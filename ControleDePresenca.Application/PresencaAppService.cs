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
    public class PresencaAppService : AppServiceBase<Presenca>, IPresencaAppService
    {

        private readonly IPresencaService _presencaService;

        public PresencaAppService(IPresencaService service) : base(service)
        {

            _presencaService = service;

        }
    }
}
