using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Domain.Entities
{

    /// <summary>
    /// Entidade Presença
    /// </summary>
    public class ListaPresenca
    {

        public ListaPresenca()
        {}

        public int ListaPresencaId { get; set; }
        /// <summary>
        /// Marca a Hora de Entrada
        /// </summary>
        public DateTime HoraEntrada { get; set;}
        /// <summary>
        /// Marca o mes
        /// </summary>
        public int Mes { get; set; }
        /// <summary>
        /// Marca o ano
        /// </summary>
        public int Ano { get; set; }

        public int Dia { get; set; }

        public bool Ativo { get; set; }

        public virtual ICollection<Presenca> Presenca { get; set; }

        public int TurmaId { get; set; }

        /// <summary>
        /// Lista de Turmas
        /// </summary>
        [ForeignKey("TurmaId")]
        public virtual Turma Turma { get; set; }
    }
}
