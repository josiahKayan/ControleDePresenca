using ControleDePresenca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleDePresenca.API.ViewModels
{
    public class ProfessorViewModel
    {

        public string Nome { get; set; }

        public string NomeCompleto { get; set; }

        public DateTime DataNascimento { get; set; }

        public int Idade { get; set; }

        public int ProfessorId { get; set; }
        /// <summary>
        /// Lista de Cursos
        /// </summary>
        public virtual ICollection<Curso> CursoLista { get; set; }
        /// <summary>
        /// Lista de Turmas
        /// </summary>
        public virtual ICollection<Turma> TurmaLista { get; set; }

        public virtual Usuario Usuario { get; set; }


        public string Imagem { get; set; }

    }
}