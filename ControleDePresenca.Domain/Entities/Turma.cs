using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Domain.Entities
{
    /// <summary>
    /// Entidade Turma
    /// </summary>
    public class Turma
    {

        public Turma()
        {
            this.AlunoLista = new List<Aluno>();
            this.PresencaLista = new List<Presenca>();
        }

        public int TurmaId { get; set; }

        public string NomeTurma { get; set; }

        /// <summary>
        /// Data de Início da Turma
        /// </summary>
        public DateTime DataInicio { get; set; }
        /// <summary>
        /// Data de Término da Turma
        /// </summary>
        public DateTime DataTermino { get; set; }

        public DateTime HoraInicial { get; set; }

        public DateTime HoraFinal { get; set; }


        /// <summary>
        /// Entidade Professor
        /// </summary>
        public virtual Professor Professor { get; set; }
        /// <summary>
        /// Entidade Curso
        /// </summary>
        public virtual Curso Curso { get; set; }
        /// <summary>
        /// Lista de Alunos
        /// </summary>
        /// 
        public virtual ICollection<Aluno> AlunoLista { get; set; }
        /// <summary>
        /// Lista de Presenças
        /// </summary>
        public virtual ICollection<Presenca> PresencaLista { get; set; }


    }
}
