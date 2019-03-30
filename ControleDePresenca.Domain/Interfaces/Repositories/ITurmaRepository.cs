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

        List<Turma> GetTurmaByProfessorId(int id);

        void UpdateTurmaProfessorCurso(Turma turma, Curso curso, Professor professor);


        List<Professor> GetTurmasPeloProfessorId(int id);

        List<Turma> GetTurmasPeloUsuarioId(int id);

        IEnumerable<Aluno> GetAlunoByTurmaId(int id);

        void AddTurmaNoCurso(Turma turma);

        void UpdateTurmaNoCurso(Turma turma);

        List<Turma> GetTurmasPeloUsuarioAlunoId(int id);

        List<Aluno> GetAlunosComUsuarioPorIdTurma( int id);

    }
}
