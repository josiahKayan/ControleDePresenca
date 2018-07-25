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

        List<Tag> BuscaTag(int id);

        List<Tag> ListaTagNaoArmazenada();


        void EditarStatusTag(Tag tag);

    }
}
