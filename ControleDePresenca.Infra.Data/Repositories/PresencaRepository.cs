using ControleDePresenca.Domain.Entities;
using ControleDePresenca.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleDePresenca.Infra.Data.Repositories
{
    public class PresencaRepository : RepositoryBase<ListaPresenca> , IPresencaRepository
    {


        public List<Presenca> GetResumoListaPresencaByIdPresencalista(int id)
        {
            return context.Set<Presenca>().Include("Aluno").Where(  p => p.PresencaId == id  ).ToList();
        }

        public IEnumerable<ListaPresenca> BuscaPorMesEAno(int mes, int ano)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ListaPresenca> GetListaPresenca(int id)
        {
            throw new NotImplementedException();
        }

        //public IEnumerable<Presenca> GetListaPresenca(int id)
        //{
        //    //return context.Set<Presenca>().Include("Alunos").Where(x => x. == id).ToList();

        //}


    }
}
