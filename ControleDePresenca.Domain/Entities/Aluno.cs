using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        //[ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }
        
        //public virtual int UsuarioId { get; set; }
        /// <summary>
        /// atributo da classe Turma
        /// </summary>
        public virtual Turma Turma { get; set; }

        //[ForeignKey("UsuarioId")]
        //public int UsuarioId { get; set; }
    }
}
