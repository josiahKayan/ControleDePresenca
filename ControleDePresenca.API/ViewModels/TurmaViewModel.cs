using ControleDePresenca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleDePresenca.API.ViewModels
{
    public class TurmaViewModel
    {

        //public Turma()
        //{
        //    this.AlunoLista = new List<Aluno>();
        //    this.PresencaLista = new List<Presenca>();
        //}

        public string NomeTurma { get; set; }


        /// <summary>
        /// Data de Início da Turma
        /// </summary>
        public string DataInicio { get; set; }
        /// <summary>
        /// Data de Término da Turma
        /// </summary>
        public string DataTermino { get; set; }

        public int ProfessorId { get; set; }

        /// <summary>
        /// Data de Início da Turma
        /// </summary>
        public string HoraInicial { get; set; }
        /// <summary>
        /// Data de Término da Turma
        /// </summary>
        public string HoraFinal { get; set; }

        /// <summary>
        /// Entidade Professor
        /// </summary>
        public virtual Professor Professor { get; set; }

        public int CursoId { get; set; }


        /// <summary>
        /// Entidade Curso
        /// </summary>

        public virtual Curso Curso { get; set; }
        /// <summary>
        /// Lista de Alunos
        /// </summary>
        public virtual ICollection<Aluno> AlunoLista { get; set; }
        /// <summary>
        /// Lista de Presenças
        /// </summary>
        public virtual ICollection<Presenca> PresencaLista { get; set; }


    }
}