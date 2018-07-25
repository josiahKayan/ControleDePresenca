using ControleDePresenca.Domain.Entities;
using ControleDePresenca.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Infra.Data.Repositories
{
    public class CursoRepository : RepositoryBase<Curso> , ICursoRepository
    {
        public Curso GetCursoByIdIncludesTurma(int id)
        {
            return context.Set<Curso>().Include("TurmaLista").ToList().Find(x => x.CursoId == id);
        }

        //public Curso GetCursoByIdIncludesTurma(int id)
        //{
        //    return context.Set<Curso>().Include("TurmaLista").ToList().Find(x => x.CursoId == id);
        //}


        //public void RemoveCurso(Curso curso)
        //{
        //    context.Set<Curso>().Remove(curso);
        //    context.SaveChanges();
        //}

        //public void RemoveCurso(Curso curso)
        //{
        //    context.Set<Curso>().Remove(curso);



        //    context.SaveChanges();
        //}

    }


}
