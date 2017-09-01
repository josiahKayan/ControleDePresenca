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

    }
}

