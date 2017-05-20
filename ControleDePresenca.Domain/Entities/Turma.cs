using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Domain.Entities
{
    public class Turma
    {

        public Turma()
        {
            this.AlunoLista = new List<Aluno>();
            this.PresencaLista = new List<Presenca>();
        }

        public int TurmaId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public virtual Professor Professor { get; set; }
        public virtual Curso Curso { get; set; }
        public virtual ICollection<Aluno> AlunoLista { get; set; }
        public virtual ICollection<Presenca> PresencaLista { get; set; }


    }
}
