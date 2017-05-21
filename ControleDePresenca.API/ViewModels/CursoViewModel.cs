using ControleDePresenca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleDePresenca.API.ViewModels
{
    public class CursoViewModel
    {

        public CursoViewModel()
        {
            this.ProfessorLista = new List<Professor>();
            this.Ativo = true;
        }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public virtual ICollection<Professor> ProfessorLista { get; set; }

    }
}