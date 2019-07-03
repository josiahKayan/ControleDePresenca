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
            return context.Set<Aluno>().Include("Usuario").Include("Turma").Include("Tag").Where(t => t.Turma.Any(a => a.TurmaId == id));
        }

        public List<Turma> GetAlunosByTurmaId(int id)
        {
            return context.Set<Turma>().Include("Curso").Include("Professor").Include("AlunoLista").Where(x => x.TurmaId == id).ToList();


        }

        public List<Aluno> GetAlunosComUsuarioPorIdTurma(int id)
        {
            var l =  context.Set<Aluno>().Include("Usuario").Include("Tag").Where(x => x.Turma.Select(t => t.TurmaId == id).FirstOrDefault()).ToList();

            return l;
        }


        public List<Turma> GetTurmasPeloCursoId(int id)
        {
            return context.Set<Turma>().Include("Curso").Include("Professor").Where(x => x.Curso.CursoId == id).ToList();
        }

        public List<Turma> GetTurmaPorProfessorId(int id)
        {
            return context.Set<Turma>().Include("Curso").Include("Professor").Where(x => x.Professor.ProfessorId == id).ToList();
        }

        public List<Professor> GetTurmasPeloProfessorId(int id)
        {
            return context.Set<Professor>().Include("TurmaLista").Include("Usuario").Where(x => x.ProfessorId == id).ToList();
        }

        public List<Turma> GetTurmasPeloUsuarioId(int id)
        {
            return context.Set<Turma>().Where(x => x.Professor.UsuarioId == id).ToList();
        }

        public List<Turma> GetTurmasPeloUsuarioAlunoId(int id)
        {
            var turma = context.Set<Turma>().Where( t => t.AlunoLista.Any( x => x.UsuarioId == id) ).ToList();



            //List<Turma> t = null;

            //foreach (var item in turma)
            //{

            //    var alunos = item.AlunoLista.Where(a => a.UsuarioId == id).ToList()   ;

            //    if (  alunos.Count > 0       ) {
            //        t = new List<Turma>();
            //        t.Add(item);
            //    }
            //}


            //var s = turma.Select( t => t ).ToList().Where(  t => t.AlunoLista.Select( a => a.UsuarioId == id ).FirstOrDefault() ).ToList()    ;

            //return s;
            return turma;

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


