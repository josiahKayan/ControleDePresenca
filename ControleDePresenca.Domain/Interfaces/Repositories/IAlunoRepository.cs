using ControleDePresenca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Domain.Interfaces.Repositories
{
    public interface IAlunoRepository : IRepositoryBase<Aluno>
    {


         IEnumerable<Aluno> GetAllAlunos();

         Aluno GetAlunoByIdIncludes(int id);

        void RemoveComUsuario(Aluno obj);

    }
}
