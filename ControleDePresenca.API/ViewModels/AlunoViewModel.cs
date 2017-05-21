﻿using ControleDePresenca.Domain.Entities;
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

        public DateTime DataNascimento { get; set; }

        public int Idade { get; set; }

        public int AlunoId { get; set; }
        /// <summary>
        /// atributo da classe Tag
        /// </summary>
        public virtual Tag Tag { get; set; }
        /// <summary>
        /// atributo da classe Usuario
        /// </summary>
        public virtual Usuario Usuario { get; set; }
        /// <summary>
        /// atributo da classe Turma
        /// </summary>
        public virtual Turma Turma { get; set; }

    }
}