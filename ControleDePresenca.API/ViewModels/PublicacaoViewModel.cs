using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleDePresenca.API.ViewModels
{
    public class PublicacaoViewModel
    {

        public int PublicacaoId { get; set; }

        public int UsuarioId { get; set; }
        
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


        /// <summary>
        /// Usuarios
        /// </summary>
        public string  TurmasId { get; set; }

    }
}