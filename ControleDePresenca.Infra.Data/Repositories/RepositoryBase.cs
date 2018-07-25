using ControleDePresenca.Domain.Interfaces.Repositories;
using ControleDePresenca.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

            using ( var context = new ControlePresencaContext()  )
            {

                context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

            }

                 
        }

        public void MarkStates(System.Data.Entity.EntityState state, params TEntity[] entity)
        {
            foreach (var item in entity)
            {
                context.Entry(item).State = state;
            }
        }

        public void MarkStates(TEntity obj)
        {
            MarkStates(System.Data.Entity.EntityState.Modified, obj );
        }
    }
}
