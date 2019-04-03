using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Domain.Entities
{
    public class MensagemPublicacao
    {

        public int MensagemPublicacaoId { get; set; }

        public int Responsavel { get; set; }


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
