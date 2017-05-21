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
    public class AlunoService : ServiceBase<Aluno>, IAlunoService
    {

        private readonly IAlunoRepository _alunoRepository; 

        public AlunoService(IAlunoRepository repository) : base(repository)
        {
            _alunoRepository = repository;
        }



    }
}
