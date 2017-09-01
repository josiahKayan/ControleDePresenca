using ControleDePresenca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleDePresenca.API.ViewModels
{
    public class AlunoViewModel
    {

        public string Nome { get; set; }

        public string NomeCompleto { get; set; }

        public string DataNascimento { get; set; }

        public string Idade { get; set; }

        public int AlunoId { get; set; }

        public int TagId  { get; set; }

        /// <summary>
        /// atributo da classe Tag
        /// </summary>
        public virtual Tag Tag { get; set; }

        public int UsuarioId { get; set; }

        /// <summary>
        /// atributo da classe Usuario
        /// </summary>
        public virtual Usuario Usuario { get; set; }
        /// <summary>
        /// atributo da classe Turma
        /// </summary>
        public virtual List<Turma> Turmas { get; set; }


        public AlunoViewModel(Aluno aluno)
        {
            this.AlunoId =  aluno.AlunoId;
            this.DataNascimento = aluno.DataNascimento.ToString("dd/MM/yyyy");
            this.Idade = "" +aluno.Idade;
            this.Nome = aluno.Nome;
            this.NomeCompleto = aluno.NomeCompleto;
            this.Tag = aluno.Tag;
            this.TagId = aluno.TagId;
            this.Turmas = aluno.Turma.ToList();
            this.Usuario = aluno.Usuario;
            this.UsuarioId = aluno.UsuarioId;
        }

        public AlunoViewModel()
        {
        }

        public List<AlunoViewModel> ParserAluno( List<Aluno> listAlunos )
        {
            return listAlunos.Select(x => new AlunoViewModel(x)).ToList();
        } 

    }
}