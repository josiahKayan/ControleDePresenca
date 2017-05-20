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
    public class CursoService : ServiceBase<Curso>, ICursoService
    {

        private readonly Interfaces.Repositories.ICursoRepository _cursoRepository;

        public CursoService(ICursoRepository repository) : base(repository)
        {

            _cursoRepository = repository;

        }

    }
}
