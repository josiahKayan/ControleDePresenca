using ControleDePresenca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Domain.Interfaces.Repositories
{
    public interface ITagRepository : IRepositoryBase<Tag>
    {

        IEnumerable<Tag> BuscaTag(string code);


    }
}
