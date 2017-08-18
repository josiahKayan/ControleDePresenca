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

        public string DataNascimento { get; set; }

        public string Idade { get; set; }

        public int ProfessorId { get; set; }
        /// <summary>
        /// Lista de Cursos
        /// </summary>
        public virtual ICollection<Curso> CursoLista { get; set; }
        /// <summary>
        /// Lista de Turmas
        /// </summary>
        public virtual ICollection<Turma> TurmaLista { get; set; }

        public int UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }


        public ProfessorViewModel(Professor professor)
        {
            this.ProfessorId = professor.ProfessorId;
            this.DataNascimento = professor.DataNascimento.ToString("dd/MM/yyyy");
            this.Idade = "" + professor.Idade;
            this.Nome = professor.Nome;
            this.NomeCompleto = professor.NomeCompleto;
            this.TurmaLista = professor.TurmaLista;
            this.Usuario = professor.Usuario;
            this.UsuarioId = professor.UsuarioId;
            this.CursoLista = professor.CursoLista;
        }

        public ProfessorViewModel()
        {
        }

        public List<ProfessorViewModel> ParserProfessor(List<Professor> listProfessor)
        {
            return listProfessor.Select(x => new ProfessorViewModel(x)).ToList();
        }

    }
}