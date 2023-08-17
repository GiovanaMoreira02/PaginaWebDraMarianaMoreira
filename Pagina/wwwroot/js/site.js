function mostrarDiv(elemento) {
    idImg = elemento.id;
    idDiv = idImg.substr(4, idImg.length - 1)
    div = document.getElementById(idDiv)

    if (div.style.display == 'block' || div.style.display == "") {
        document.getElementById(idImg).src = "../Imagens/setaBaixo.png";
        document.getElementById(div.id).style.display = 'none'

        if (idDiv == "Contato")
            document.getElementById("selecao").style.display = 'none'
    }
    else {
        document.getElementById(div.id).style.display = 'block  '
        document.getElementById(idImg).src = "../Imagens/setaCima.png";

        if (idDiv == "Contato")
            document.getElementById("selecao").style.display = 'block'
    }
}

function OpcaoSelecionada() {
    combo = document.getElementById("selecao")

    opcaoSelecionada = combo.value;
    assunto = combo[opcaoSelecionada].text;

    return assunto;
}

document.addEventListener('DOMContentLoaded', function () {
    var campoNome = document.getElementById('nomeContato');
    campoNome.addEventListener('input', function () {
        if (campoNome.value.trim().split(' ').length < 2) {
            campoNome.setCustomValidity('Digite o nome completo');
        }
        else {
            campoNome.setCustomValidity('');
        }
    });
});

document.addEventListener('DOMContentLoaded', function () {
    var campoTelefone = document.getElementById('telefone');
    campoTelefone.addEventListener('input', function () {
        if (campoTelefone.value.trim().length < 11) {
            campoTelefone.setCustomValidity('Digite um telefone válido (com DDD)');
        }
        else {
            campoTelefone.setCustomValidity('');
        }
    });
});

document.addEventListener('DOMContentLoaded', function () {
    var campoAssunto = document.getElementById('selecao');
    campoAssunto.addEventListener('input', function () {
        if (campoAssunto.value == '0') {
            campoAssunto.setCustomValidity('Selecione o assunto desejado');
        }
        else {
            campoAssunto.setCustomValidity('');
        }
    });
});

$(document).ready(function () {
    $('#telefone').on('input', function () {
        let telefone = $(this).val();
        telefone = telefone.replace(/\D/g, '');
        let formattedTelefone = '';

        if (telefone.length >= 11) {
            formattedTelefone = '(' + telefone.substr(0, 2) + ') ' +
                telefone.substr(2, 5) + '-' +
                telefone.substr(7, 4);
        } else if (telefone.length >= 10) {
            formattedTelefone = '(' + telefone.substr(0, 2) + ') ' +
                telefone.substr(2, 4) + '-' +
                telefone.substr(6, 4);
        } else if (telefone.length >= 9) {
            formattedTelefone = telefone.substr(0, 5) + '-' +
                telefone.substr(5, 4);
        } else if (telefone.length >= 5) {
            formattedTelefone = telefone.substr(0, 5) + '-' +
                telefone.substr(5);
        } else {
            formattedTelefone = telefone;
        }

        $(this).val(formattedTelefone);
    });
});






