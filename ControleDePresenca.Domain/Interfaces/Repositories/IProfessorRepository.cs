using ControleDePresenca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Domain.Interfaces.Repositories
{
    public interface IProfessorRepository : IRepositoryBase<Professor>
    {

        IEnumerable<Professor> GetAllProfessors();

        Professor GetProfessorByIdIncludes(int id);

        void RemoveComUsuario(Professor obj);

        List<Professor> GetProfessorBy(int id);
        Professor UpdateTeacher(Professor professor);
    }
}
