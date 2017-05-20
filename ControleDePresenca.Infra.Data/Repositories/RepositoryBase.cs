using ControleDePresenca.Domain.Interfaces.Repositories;
using ControleDePresenca.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControleDePresenca.Infra.Data.Repositories
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {

        protected ControlePresencaContext context = new ControlePresencaContext();

        public void Add(TEntity obj)
        {
            context.Set<TEntity>().Add(obj);
            context.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return context.Set<TEntity>().ToList();

        }

        public TEntity GetEntityById(int id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public void Remove(TEntity obj)
        {
            context.Set<TEntity>().Remove(obj);
            context.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges(); 
        }
    }
}
