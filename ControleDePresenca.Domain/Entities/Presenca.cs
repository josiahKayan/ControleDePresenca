using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Domain.Entities
{

    /// <summary>
    /// Entidade Presença
    /// </summary>
    public class Presenca
    {

        public Presenca()
        {
            this.TurmaLista = new List<Turma>();
        }

        public int PresencaId { get; set; }
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

        //public virtual Turma Turma { get; set; }
        /// <summary>
        /// Lista de Turmas
        /// </summary>
        public virtual ICollection<Turma> TurmaLista { get; set; }
    }
}
