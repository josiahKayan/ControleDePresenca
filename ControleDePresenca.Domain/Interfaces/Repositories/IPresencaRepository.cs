using ControleDePresenca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Domain.Interfaces.Repositories
{
    public interface IPresencaRepository : IRepositoryBase<ListaPresenca>
    {

        IEnumerable<ListaPresenca> BuscaPorMesEAno(int mes, int ano);

        IEnumerable<ListaPresenca> GetListaPresenca(int id);


    }
}
