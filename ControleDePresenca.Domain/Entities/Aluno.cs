using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Domain.Entities
{
    /// <summary>
    /// Entidade Aluno
    /// </summary>
    public class Aluno : Pessoa
    {

        public Aluno()
        {
            //Usuario = new Usuario();
        }

        public int AlunoId { get; set; }


        
        /// <summary>
        /// atributo da classe Tag
        /// </summary>
        /// 
        [ForeignKey("TagId")]
        public virtual Tag Tag { get; set; }


        public int TagId { get; set; }

        /// <summary>
        /// atributo da classe Usuario
        /// </summary>
        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }

        public int UsuarioId { get; set; }


        /// <summary>
        /// atributo da classe Turma
        /// </summary>
        public virtual ICollection<Turma> Turma { get; set; }


        public string NotificacaoId { get; set; }

    }
}
