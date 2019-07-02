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
    public class CursoRepository : RepositoryBase<Curso> , ICursoRepository
    {
        public Curso GetCursoByIdIncludesTurma(int id)
        {
            return context.Set<Curso>().Include("TurmaLista").ToList().Find(x => x.CursoId == id);
        }




        public void UpdateCurso(Curso curso, int id)
        {
            using (var context = new ControlePresencaContext())
            {

                var c = context.Cursos.Where( x => x.CursoId ==  id ).FirstOrDefault();

                c.Ativo = curso.Ativo;
                c.Descricao = curso.Descricao;
                c.Nome = curso.Nome;

                //context.Entry(curso).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

            }

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
