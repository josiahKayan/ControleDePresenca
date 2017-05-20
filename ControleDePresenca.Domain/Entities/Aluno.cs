using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Domain.Entities
{
    public class Aluno : Pessoa
    {

        public Aluno()
        {
            Usuario = new Usuario();
        }

        public int AlunoId { get; set; }
        public virtual Tag Tag { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Turma Turma { get; set; }
    }
}
