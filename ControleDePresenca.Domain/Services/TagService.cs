using ControleDePresenca.Domain.Entities;
using ControleDePresenca.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleDePresenca.Domain.Interfaces.Repositories;

namespace ControleDePresenca.Domain.Services
{
    public class TagService : ServiceBase<Tag>, ITagService
    {

        private readonly ITagRepository _tagRepository; 

        public TagService(ITagRepository repository) : base(repository)
        {
            _tagRepository = repository;
        }



    }
}
