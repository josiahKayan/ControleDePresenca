using ControleDePresenca.Application.Interfaces;
using ControleDePresenca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleDePresenca.Domain.Interfaces.Services;

namespace ControleDePresenca.Application
{
    public class TagAppService : AppServiceBase<Tag>, ITagAppService
    {

        private readonly ITagService _tagService;

        public TagAppService(ITagService tagService) : base(tagService)
        {
            _tagService = tagService;
        }
    }
}
