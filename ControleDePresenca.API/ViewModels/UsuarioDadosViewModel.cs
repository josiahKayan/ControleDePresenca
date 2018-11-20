using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleDePresenca.API.ViewModels
{
    public class UsuarioDadosViewModel
    {

        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string NomeCompleto { get; set; }
        public int DataNascimento { get; set; }

    }
}