using ControleDePresenca.Domain.Entities;
using ControleDePresenca.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public void UpdateTurmaProfessorCurso(Turma turma, Curso curso , Professor professor)
        {
            context.Set<Turma>().Add(turma);
            context.SaveChanges();
        }

        

    }
}

