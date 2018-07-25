using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Domain.Entities
{
    /// <summary>
    /// Pessoa
    /// </summary>
    public abstract class Pessoa
    {
        /// <summary>
        /// Nome de uma Pessoa
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Nome Completo de uma Pessoa
        /// </summary>
        public string NomeCompleto { get; set; }

        /// <summary>
        /// Foto
        /// </summary>
        public string Imagem { get; set; }

        /// <summary>
        /// Data de Nascimento de uma Pessoa
        /// </summary>
        public DateTime DataNascimento { get; set; }

        /// <summary>
        /// Idade de uma pessoas
        /// </summary>
        public int Idade { get; set; }

    }
}
