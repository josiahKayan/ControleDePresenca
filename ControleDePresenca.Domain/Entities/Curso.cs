﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Domain.Entities
{
    public class Curso
    {

        public Curso()
        {
            this.ProfessorLista = new List<Professor>();
            this.Ativo = true;
        }

        public int CursoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public virtual ICollection<Professor> ProfessorLista { get; set; }
    
    }
}