﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Domain.Entities
{
    public class Usuario
    {

        public int UsuarioId { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

    }
}
