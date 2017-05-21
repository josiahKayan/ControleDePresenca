using ControleDePresenca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControleDePresenca.API.ViewModels
{
    /// <summary>
    /// Entidade Tag
    /// </summary>
    public class TagViewModel
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