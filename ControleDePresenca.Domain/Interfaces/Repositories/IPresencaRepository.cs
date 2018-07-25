﻿using ControleDePresenca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Domain.Interfaces.Repositories
{
    public interface IPresencaRepository : IRepositoryBase<Presenca>
    {

        IEnumerable<Presenca> BuscaPorMesEAno(int mes, int ano);

        IEnumerable<Presenca> GetListaPresenca(int id);


    }
}
