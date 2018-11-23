using ControleDePresenca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Infra.Data.Repositories
{

    //: RepositoryBase<Tag>, ITagRepository
    public class ListaPresencaRepository : RepositoryBase<ListaPresenca>
    {

        public List<ListaPresenca> GetListaPresencaByIdTurma(int id)
        {
            return context.Set<ListaPresenca>().Include("Turma").Where( t => t.Turma.TurmaId == id    ).OrderByDescending(a => a.Ano).OrderByDescending( o => o.Mes  ).OrderByDescending( p => p.Dia ).ToList();
        }




    }
}
