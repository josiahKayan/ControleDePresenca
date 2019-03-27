using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleDePresenca.Domain.Entities
{
    public class Geral
    {

        public int TotalDias { get; set; }

        public List<FrequenciaAlunos> FrequenciaAlunos { get; set; }

    }
}