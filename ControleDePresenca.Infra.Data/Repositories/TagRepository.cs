using ControleDePresenca.Domain.Entities;
using ControleDePresenca.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Infra.Data.Repositories
{
    public class TagRepository : RepositoryBase<Tag>, ITagRepository
    {

        public IEnumerable<Tag> BuscaTag(string code)
        {
            throw new NotImplementedException();
        }
    }
}


