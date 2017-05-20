using ControleDePresenca.Domain.Entities;
using ControleDePresenca.Domain.Interfaces.Repositories;
using ControleDePresenca.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Domain.Services
{
    public class ProfessorService : ServiceBase<Professor>, IProfessorService
    {

        private readonly IProfessorRepository _professorRepository;

        public ProfessorService(IProfessorRepository repository) : base(repository)
        {

            _professorRepository = repository;

        }
    }
}
