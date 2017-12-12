using ControleDePresenca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Domain.Interfaces.Repositories
{
    public interface ITurmaRepository : IRepositoryBase<Turma>
    {
        List<Turma> GetTurmasPeloCursoId(int id);

        void UpdateTurmaProfessorCurso(Turma turma, Curso curso, Professor professor);

        List<Turma> GetTurmaPorProfessorId(int id);

        List<Professor> GetTurmasPeloProfessorId(int id);

    }
}
