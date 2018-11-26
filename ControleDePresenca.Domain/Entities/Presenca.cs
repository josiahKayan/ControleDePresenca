using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Domain.Entities
{
    public class Presenca
    {
        

        public int PresencaId { get; set; }

        public string NomeDia { get; set; }

        public string NomeMes { get; set; }
        

        public DateTime HoraChegada { get; set; }

       
        [ForeignKey("AlunoId")]
        public virtual Aluno Aluno{ get; set; }

        public int AlunoId { get; set; }

        public int ListaPresencaId { get; set; }

        /// <summary>
        /// Lista de Turmas
        /// </summary>
        [ForeignKey("ListaPresencaId")]
        public virtual ListaPresenca ListaPresenca { get; set; }

    }
}
