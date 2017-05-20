using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Domain.Entities
{
    public class Presenca
    {

        public Presenca()
        {
            this.TurmaLista = new List<Turma>();
        }

        public int PresencaId { get; set; }
        public DateTime HoraEntrada { get; set;}
        public int Mes { get; set; }
        public int Ano { get; set; } 
        //public virtual Turma Turma { get; set; }
        public virtual ICollection<Turma> TurmaLista { get; set; }
    }
}
