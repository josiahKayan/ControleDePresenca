using ControleDePresenca.Domain.Entities;
using ControleDePresenca.Domain.Interfaces.Repositories;
using ControleDePresenca.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Infra.Data.Repositories
{
    public class TurmaRepository : RepositoryBase<Turma>, ITurmaRepository
    {
        public void AddAlunoAturma(Turma turma)
        {
            context.Entry(turma).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public Aluno GetAlunoByIdIncludesTurma(int id)
        {
            return context.Set<Aluno>().Include("Usuario").Include("Turma").Include("Tag").ToList().Find(x => x.AlunoId == id);
        }

        public void AddTurmaNoCurso( Turma turma)
        {

            using (var context = new ControlePresencaContext())
            {

                var professor = context.Professor.Find(turma.ProfessorId);
                var curso = context.Cursos.Find(turma.CursoId);

                turma.Professor = professor;
                turma.Curso = curso;

                context.Professor.Attach(turma.Professor);
                context.Cursos.Attach(turma.Curso);

                context.Turmas.Add(turma);
                
                context.SaveChanges();
            }

        }


        public void UpdateTurmaNoCurso(Turma turma)
        {

            using (var context = new ControlePresencaContext())
            {

                var t = context.Turmas.Find(turma.TurmaId);

                t.DataInicio = turma.DataInicio;
                t.DataTermino = turma.DataTermino;
                t.HoraFinal = turma.HoraFinal;
                t.HoraInicial = turma.HoraInicial;
                t.NomeTurma = turma.NomeTurma;

                var p = context.Professor.Find(turma.ProfessorId);

                t.Professor = p;

                //var professor = context.Professor.Find(turma.ProfessorId);
                //var curso = context.Cursos.Find(turma.CursoId);

                //turma.Professor = professor;
                //turma.Curso = curso;

                //context.Entry(turma.Professor).State = System.Data.Entity.EntityState.Unchanged;

                context.Entry(t).State = System.Data.Entity.EntityState.Modified;

                context.SaveChanges();
            }

        }

        public IEnumerable<Aluno> GetAlunoByTurmaId(int id)
        {
            return context.Set<Aluno>().Include("Usuario").Include("Turma").Include("Tag").Include("ListaPresenca").ToList().Where(t => t.Turma.Any(a => a.TurmaId == id));
        }

        public List<Turma> GetTurmasPeloCursoId(int id)
        {
            return context.Set<Turma>().Include("Curso").Include("Professor").ToList().Where(x => x.Curso.CursoId == id).ToList();
        }

        public List<Turma> GetTurmaPorProfessorId(int id)
        {
            return context.Set<Turma>().Include("Curso").Include("Professor").ToList().Where(x => x.Professor.ProfessorId == id).ToList();
        }

        public List<Professor> GetTurmasPeloProfessorId(int id)
        {
            return context.Set<Professor>().Include("TurmaLista").Include("Usuario").ToList().Where(x => x.ProfessorId == id).ToList();
        }

        public List<Turma> GetTurmasPeloUsuarioId(int id)
        {
            return context.Set<Turma>().Include("Curso").Include("Professor").ToList().Where(x => x.Professor.UsuarioId == id).ToList();
        }

        public void UpdateTurmaProfessorCurso(Turma turma, Curso curso, Professor professor)
        {
            context.Set<Turma>().Add(turma);
            context.SaveChanges();
        }

        public List<Turma> GetTurmaByProfessorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}


