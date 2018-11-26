using ControleDePresenca.Domain.Entities;
using ControleDePresenca.Domain.Interfaces.Repositories;
using ControleDePresenca.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Infra.Data.Repositories
{
    public class AlunoRepository : RepositoryBase<Aluno>, IAlunoRepository
    {

        public void addAluno(Aluno aluno)
        {
            //MarkStates(aluno);

            using ( var context = new ControlePresencaContext()  )
            {

                //context.Entry(aluno).State = System.Data.Entity.EntityState.Modified;


                //context.Entry(aluno.Tag).State = System.Data.Entity.EntityState.Unchanged;

                //context.Entry(aluno).State = System.Data.Entity.EntityState.Added;

                aluno.Tag.Status = 1;
                context.Tags.Attach(aluno.Tag);
                context.Aluno.Add(aluno);

                //context.Usuarios.Attach(aluno.Usuario);
                //context.Aluno.Add(aluno);

                context.SaveChanges();
            }

           

            //context.Aluno.Add(aluno);
            ////context.Tags.Attach(aluno.Tag);

            //context.SaveChanges();


        }

        public void MarkStates(System.Data.Entity.EntityState state,object entity)
        {
            

            PropertyInfo[] t = entity.GetType().GetProperties();

            foreach (var item in t)
            {

                if ( item.Name.Equals("Tag")  ) {
                    context.Entry(item).State = state;
                }
            }
            
            context.SaveChanges();
        }

        public void MarkStates(object obj)
        {
            MarkStates(System.Data.Entity.EntityState.Unchanged, obj);
        }


        public Aluno GetAlunoByIdIncludes(int id)
        {
            return context.Set<Aluno>().Include("Usuario").Include("Tag").ToList().Find(x => x.AlunoId == id);
        }

        public Aluno GetAlunoByUsuarioId(int id)
        {
            return context.Set<Aluno>().Include("Usuario").Include("Tag").ToList().Find(x => x.UsuarioId == id);
        }

        public IEnumerable<Aluno> GetAllAlunos()
        {
            return context.Set<Aluno>().Include("Usuario").Include("Turma").Include("Tag").ToList();
        }

        public Aluno GetAlunoByIdAllIncludes(int id)
        {
            return context.Set<Aluno>().Include("Usuario").Include("Turma").Include("Tag").Include("Presenca").ToList().Find(x => x.AlunoId == id);
        }

        //public TEntity GetEntityById(int id)
        //{
        //    return context.Set<TEntity>().Find(id);
        //}

        //public void Remove(TEntity obj)
        //{
        //    context.Set<TEntity>().Remove(obj);
        //    context.SaveChanges();
        //}

        //public void Update(TEntity obj)
        //{
        //    context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
        //    context.SaveChanges();
        //}


        public void UpdateAluno(Aluno a)
        {

         using ( var context = new ControlePresencaContext()  )
            {

                
                Aluno aluno = context.Aluno.Where(x => x.AlunoId == a.AlunoId ).FirstOrDefault();
                

                aluno.Nome = a.Nome;
                aluno.NomeCompleto = a.NomeCompleto;
                aluno.Idade = a.Idade;
                aluno.DataNascimento = a.DataNascimento;
                aluno.Tag = a.Tag;
                //aluno.Turma = alunoVm.Turma;
                aluno.Usuario = a.Usuario;
                aluno.Imagem = a.Imagem;
                
                context.SaveChanges();
            }

        }

        

        public void RemoveComUsuario(Aluno obj)
        {

            //context.Usuarios.Remove(obj.Usuario);

            //foreach (var item in obj.Presenca)
            //{
            //    context.Presencas.Remove(item);
            //}

            foreach (var item in obj.Turma)
            {
                if (item.AlunoLista.Contains(obj))
                {
                    item.AlunoLista.Remove(obj);
                }
            }

            context.Usuarios.Remove(obj.Usuario);

            context.Set<Aluno>().Remove(obj);
            context.SaveChanges();
        }

    }
}


