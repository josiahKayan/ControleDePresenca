using ControleDePresenca.Domain.Entities;
using ControleDePresenca.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;

namespace ControleDePresenca.Infra.Data.Repositories
{
    public class PresencaRepository : RepositoryBase<Presenca> , IPresencaRepository
    {

        public IEnumerable<Presenca> BuscaPorMesEAno(int mes, int ano)
        {
            throw new NotImplementedException();
        }

    }
}
