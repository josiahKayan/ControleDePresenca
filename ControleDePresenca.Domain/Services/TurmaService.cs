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
    public class TurmaService : ServiceBase<Turma>, ITurmaService
    {

        private readonly ITurmaRepository _turmaRepository; 

        public TurmaService(ITurmaRepository repository) : base(repository)
        {
            _turmaRepository = repository;
        }



    }
}
