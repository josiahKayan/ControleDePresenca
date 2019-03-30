using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Domain.Entities
{
    /// <summary>
    /// Entidade Usuario
    /// </summary>
    public class Usuario
    {
        //[Key]
        public int UsuarioId { get; set; }
        /// <summary>
        /// Email do usuário
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Senha do usuário
        /// </summary>
        public string Senha { get; set; }

        public int Perfil { get; set; }

        public string NotificacaoId { get; set; }


    }
}
