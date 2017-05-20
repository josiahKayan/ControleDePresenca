using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Domain.Entities
{
    public abstract class Pessoa
    {

        public string Nome { get; set; }

        public string NomeCompleto { get; set; }

        public DateTime DataNascimento { get; set; }

        public int Idade { get; set; }

    }
}
