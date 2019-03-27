using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleDePresenca.Domain.Entities
{
    public class FrequenciaAlunos
    {
        public int IdAluno { get; set; }
        public string Nome { get; set; }
        public string NomeCompleto { get; set; }
        //public string Imagem { get; set; }
        //public DateTime DataNascimento { get; set; }
        //public int Idade { get; set; }

        public int PresencasTotal { get; set; }

        public int FaltasTotal { get; set; }

        public List<PresencaDia> ListaPresencaDia  {get;set;}



    }
}