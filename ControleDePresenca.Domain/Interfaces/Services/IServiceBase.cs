using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Domain.Interfaces.Services
{
    public interface IServiceBase<TEntity>  where TEntity : class
    {

        void Add(TEntity obj);
        void Remove(TEntity obj);
        TEntity GetEntityById(int i);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity obj);


    }
}
