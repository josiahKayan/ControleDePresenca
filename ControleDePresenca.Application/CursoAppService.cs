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
    public class CursoAppService : AppServiceBase<Curso>, ICursoAppService
    {

        private readonly ICursoService _cursoService;


        public CursoAppService(ICursoService service) : base(service)
        {
            _cursoService = service;


        }
    }
}
