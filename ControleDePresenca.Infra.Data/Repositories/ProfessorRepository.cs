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
    public class ProfessorRepository : RepositoryBase<Professor>, IProfessorRepository
    {
        public IEnumerable<Professor> GetAllProfessors()
        {
            return context.Set<Professor>().Include("Usuario").Include("TurmaLista").ToList();
        }

        public Professor GetProfessorByIdIncludes(int id)
        {
            return context.Set<Professor>().Include("Usuario").Include("TurmaLista").ToList().Find(x => x.ProfessorId == id);
        }

        public Professor GetProfessorByIdIncludesUserId(int id)
        {
            return context.Set<Professor>().Include("Usuario").Include("TurmaLista").ToList().Find(x => x.UsuarioId == id);
        }

        public List<Professor> GetProfessorBy(int id)
        {
            return context.Set<Professor>().Include("Usuario").Include("TurmaLista").OrderBy(x => x.ProfessorId == id).ToList();
        }

        public void UpdateTeacher(Professor p)
        {

            using (var context = new ControlePresencaContext())

            {

                int id = p.ProfessorId;

                Professor professor = context.Professor.Where(x => x.ProfessorId == p.ProfessorId).FirstOrDefault();

                //Professor professor = context.Professor.Where(pr => pr.ProfessorId == p.ProfessorId).FirstOrDefault();

                //Professor professor = context.Professor.Where(pf => pf.ProfessorId == p.ProfessorId).FirstOrDefault();


                professor.Nome = p.Nome;
                professor.NomeCompleto = p.NomeCompleto;
                professor.Idade = p.Idade;
                professor.DataNascimento = p.DataNascimento;
                professor.Imagem = p.Imagem;
                professor.TurmaLista = p.TurmaLista;

                context.SaveChanges();

                //return professor;



            }
        }


        public void AtualizaProfessor(Professor p, Usuario u)
        {
            Professor professor = null;

            using (var context = new ControlePresencaContext())
            {
                professor = context.Professor.Where(x => x.UsuarioId == p.UsuarioId).FirstOrDefault();
            }

            using (var context = new ControlePresencaContext())
            {
                
                professor.Nome = p.Nome;
                professor.NomeCompleto = p.NomeCompleto;
                professor.DataNascimento = p.DataNascimento;
                professor.Imagem = p.Imagem;
                professor.Usuario = u;

                context.Entry(professor).State = System.Data.Entity.EntityState.Modified;

                context.SaveChanges();
            }
        }

        public void RemoveComUsuario(Professor obj)
        {

            //foreach (var item in obj.CursoLista)
            //{
            //    if (item.ProfessorLista.Contains(obj))
            //    {
            //        item.ProfessorLista.Remove(obj);
            //    }
            //}

            //foreach (var item in obj.TurmaLista)
            //{
            //    if (item.Professor.UsuarioId == obj.UsuarioId)
            //    {
            //        context.Turmas.Remove(item);
            //    }

            //}

            //context.Usuarios.Remove(obj.Usuario);
            context.Set<Professor>().Remove(obj);
            context.SaveChanges();
        }

        Professor IProfessorRepository.UpdateTeacher(Professor professor)
        {
            throw new NotImplementedException();
        }
    }
}
