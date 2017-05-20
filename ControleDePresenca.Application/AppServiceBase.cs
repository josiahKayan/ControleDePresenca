using ControleDePresenca.Application.Interfaces;
using ControleDePresenca.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Application
{
    public class AppServiceBase<TEntity> : IDisposable, IAppServiceBase<TEntity> where TEntity : class
    {
        private readonly IServiceBase<TEntity> _service;

        public AppServiceBase(IServiceBase<TEntity> service)
        {
            this._service = service;
        }

        public void Add(TEntity obj)
        {
            _service.Add(obj);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _service.GetAll();

        }

        public TEntity GetEntityById(int id)
        {
            return _service.GetEntityById(id);
        }

        public void Remove(TEntity obj)
        {
            _service.Remove(obj);
        }

        public void Update(TEntity obj)
        {
            _service.Update(obj);
        }


    }
}
