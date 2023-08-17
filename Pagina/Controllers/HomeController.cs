using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Pagina.Models;
using System.Configuration;
using System.Diagnostics;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using ConfigurationManager = System.Configuration.ConfigurationManager;
using System.ComponentModel;
using Microsoft.AspNetCore.Hosting.Server;
using System.Net;
using System.DirectoryServices.Protocols;
using System.Text;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace Pagina.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new ContatoViewModel();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult EnviarEmail(ContatoViewModel ctt)
        {
            ValidaDados(ctt);
            if (ModelState.IsValid)
            {
                SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                client.UseDefaultCredentials = false;
                System.Net.NetworkCredential credentials =
                    new System.Net.NetworkCredential(ctt.user, ctt.password);
                client.EnableSsl = true;
                client.Credentials = credentials;
                client.TargetName = "smtp-mail.outlook.com";
                client.Host = "smtp.mailgun.org";

                try
                {
                    MailMessage mail = new MailMessage();
                    mail.Subject = $"Solicitação de Contato - {ctt.nomeContato}";
                    mail.From = new MailAddress(ctt.email, string.Empty, System.Text.Encoding.UTF8);
                    mail.To.Add(new MailAddress("giovana.moreira30@gmail.com"));
                    mail.Subject = "";

                    mail.Body = MontaCorpoEmail(ctt.nomeContato, ctt.telefone, ctt.email, ctt.assunto);
                    client.Send(mail);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return View("Index", ctt);
            }
            else
            {
                //ViewBag.Teste = "false";
                return View("Index", ctt);
            }
        }

        private void ValidaDados(ContatoViewModel ctt)
        {
            if (!string.IsNullOrEmpty(ctt.nomeContato))
            {
                if (ctt.nomeContato.Substring(ctt.nomeContato.Trim().Length - 1) != "")
                {
                    ModelState.AddModelError("nomeContato", "Preencha seu nome completo.");
                }
            }
            else
                ModelState.AddModelError("nomeContato", "Preencha seu nome.");

            if (!string.IsNullOrEmpty(ctt.telefone))
            {
                if (ctt.telefone.Trim().Length < 11)
                {
                    ModelState.AddModelError("telefone", "Digite um telefone válido (com DDD)");
                }
            }
            else
                ModelState.AddModelError("telefone", "Preencha o número do telefone.");

            if (!string.IsNullOrEmpty(ctt.email))
            {
                //string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";                

                if (!new EmailAddressAttribute().IsValid(ctt.email))
                {
                    ModelState.AddModelError("email", "Digite um e-mail válido.");
                }
            }
            else
                ModelState.AddModelError("email", "Preencha seu e-mail");

            if (ctt.assunto == "0")
            {
                ModelState.AddModelError("assunto", "Selecione o assunto desejado.");
            }
        }

        private static string MontaCorpoEmail(string nome, string telefone, string email, string assunto)
        {
            var ddd = telefone.Substring(0, 2);
            telefone = telefone.Substring(2);

            string msg = $"Dados do cliente solicitante: \n \n" +
                         $"Nome: {nome} \n" +
                         $"Telefone: ({ddd}){telefone} \n" +
                         $"Email: {email} \n" +
                         $"Assunto desejado: {assunto}";

            return msg;
        }


    }
}

