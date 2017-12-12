using ControleDePresenca.Domain.Entities;
using Newtonsoft.Json;
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
            this.TurmaLista = new List<Turma>();
        }

        public int CursoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }


        [JsonIgnore]
        public virtual ICollection<Professor> ProfessorLista { get; set; }
        public virtual ICollection<Turma> TurmaLista { get; set; }

    }
}