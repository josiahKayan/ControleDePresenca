﻿using ControleDePresenca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleDePresenca.API.ViewModels
{
    public class PresencaViewModel
    {

        public int PresencaId { get; set; }

        public bool Ativo { get; set; }

        public int Dia { get; set; }

        /// <summary>
        /// Marca a Hora de Entrada
        /// </summary>
        public DateTime HoraEntrada { get; set; }
        /// <summary>
        /// Marca o mes
        /// </summary>
        /// 
        public int Mes { get; set; }
        /// <summary>
        /// Marca o ano
        /// </summary>
        public int Ano { get; set; }

        public ICollection<Aluno> Alunos { get; set; }
        public object TurmaLista { get; internal set; }
    }
}