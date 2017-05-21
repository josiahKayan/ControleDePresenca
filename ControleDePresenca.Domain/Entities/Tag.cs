using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Domain.Entities
{
    /// <summary>
    /// Entidade Tag
    /// </summary>
    public class Tag
    {

        public int TagId { get; set; }
        /// <summary>
        /// Código da Tag
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Status da Tag
        /// </summary>
        public int Status { get; set; }
        
    }
}
