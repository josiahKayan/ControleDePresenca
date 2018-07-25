using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Domain.Entities
{
    /// <summary>
    /// Entidade Curso
    /// </summary>
    public class Curso
    {

        public Curso()
        {
            //this.ProfessorLista = new List<Professor>();
            this.Ativo = true;
            this.TurmaLista = new List<Turma>();
        }

        public int CursoId { get; set; }
        /// <summary>
        /// Nome do Curso
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Descrição
        /// </summary>
        public string Descricao { get; set; }
        /// <summary>
        /// Ativo
        /// </summary>
        public bool Ativo { get; set; }

        /// <summary>
        /// Lista de Professores
        /// </summary>
        //public virtual ICollection<Professor> ProfessorLista { get; set; }

        public virtual ICollection<Turma> TurmaLista { get; set; }
        public ICollection<Professor> ProfessorLista { get; set; }
    }
}
