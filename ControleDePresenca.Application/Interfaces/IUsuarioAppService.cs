﻿using ControleDePresenca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Application.Interfaces
{
    public interface IUsuarioAppService : IAppServiceBase<Usuario>
    {

        void AddUsuario( Usuario user);

    }
}