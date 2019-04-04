using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleDePresenca.API.ViewModels
{
    public class PerfilViewModel
    {

        public int UserId { get; set; }

        public string Nome { get; set; }

        public string NomeCompleto { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Imagem { get; set; }


    }
}