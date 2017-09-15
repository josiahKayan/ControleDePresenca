using ControleDePresenca.Domain.Entities;
using ControleDePresenca.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleDePresenca.Infra.Data.Repositories
{
    public class PresencaRepository : RepositoryBase<Presenca>, IPresencaRepository
    {

        public IEnumerable<Presenca> BuscaPorMesEAno(int mes, int ano)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Presenca> GetListaPresenca(int id)
        {
            context.SetLazyLoading(false);

            return context.Set<Presenca>().Include("Alunos").Where(x => x.TurmaId == id).ToList();
            //return context.Set<Presenca>().Where(x => x.TurmaId == id).ToList();

        }


    }



}
