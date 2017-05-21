using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Domain.Entities
{
    /// <summary>
    /// Entidade Aluno
    /// </summary>
    public class Aluno : Pessoa
    {

        public Aluno()
        {
            Usuario = new Usuario();
        }

        public int AlunoId { get; set; }
        /// <summary>
        /// atributo da classe Tag
        /// </summary>
        public virtual Tag Tag { get; set; }
        /// <summary>
        /// atributo da classe Usuario
        /// </summary>
        public virtual Usuario Usuario { get; set; }
        /// <summary>
        /// atributo da classe Turma
        /// </summary>
        public virtual Turma Turma { get; set; }
    }
}
