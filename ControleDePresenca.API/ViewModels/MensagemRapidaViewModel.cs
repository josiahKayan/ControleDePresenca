using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleDePresenca.API.ViewModels
{
    public class MensagemRapidaViewModel
    {

        public int Responsavel { get; set; }

        public string IdUnico { get; set; }

        /// <summary>
        /// Marca a Hora de Publicacao
        /// </summary>
        public DateTime HoraPublicacao { get; set; }
        /// <summary>
        /// Marca o mes
        /// </summary>
        public int Mes { get; set; }
        /// <summary>
        /// Marca o ano
        /// </summary>
        public int Ano { get; set; }

        public int Dia { get; set; }

        public string Mensagem { get; set; }
        public string Titulo { get; set; }

        public string Imagem { get; set; }
        public string Nome { get; set; }
        public string NomeCompleto { get; set; }



        /// <summary>
        /// Usuarios
        /// </summary>
        public int PessoaDestino { get; set; }

    }
}