using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Pagina.Models
{
    public class ContatoViewModel
    {        
        public string? nomeContato { get; set; }

        public string? telefone { get; set; }

        public string? email { get; set; }

        public string? assunto { get; set; }

        public string emailDestinatario { get; set; } = "advocaciamarianamoreira@gmail.com";

        public string user { get; set; } = "postmaster@sandbox58dbd7cbd45e4a79b058302737a63068.mailgun.org";

        public string password { get; set; } = "7ab947092242c9fb146e3d48f91b4787-135a8d32-8b8f4f7d";


    }
}
