﻿using ControleDePresenca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Domain.Interfaces.Services
{
    public interface IUsuarioService : IServiceBase<Usuario>
    {

        void AddUsuario(Usuario user); 

    }
}
