using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Domain.Entities
{
    /// <summary>
    /// Entidade Professor
    /// </summary>
    public class Professor : Pessoa
    {

        public Professor()
        {
            //this.CursoLista = new List<Curso>();
            this.TurmaLista = new List<Turma>();
        }

        public int ProfessorId { get; set; }
        /// <summary>
        /// Lista de Cursos
        /// </summary>
        //public virtual ICollection<Curso> CursoLista { get; set; }
        /// <summary>
        /// Lista de Turmas
        /// </summary>
        public virtual ICollection<Turma> TurmaLista { get; set; }

        

        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }

        public int UsuarioId { get; set; }

    }
}
