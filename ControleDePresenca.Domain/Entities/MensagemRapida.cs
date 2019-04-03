using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Domain.Entities
{
    public class MensagemRapida
    {

        public int MensagemRapidaId { get; set; }

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


        /// <summary>
        /// Usuarios
        /// </summary>
        public int PessoaDestino { get; set; }

    }
}
