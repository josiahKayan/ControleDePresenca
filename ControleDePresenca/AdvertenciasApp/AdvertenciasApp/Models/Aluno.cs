using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvertenciasApp.Models
{
    public class Aluno
    {

        public long Matricula { get; set; }
        public string Nome { get; set; }
        public Advertencia Advertencia { get; set; }

    }
}