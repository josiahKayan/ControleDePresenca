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
            return context.Set<Aluno>().Include("Usuario").Include("Tag").Where(x => x.UsuarioId == id).FirstOrDefault();
        }


        public IEnumerable<Aluno> GetAllAlunos()
        {
            return context.Set<Aluno>().Include("Usuario").Include("Turma").Include("Tag").ToList();
        }


        public Aluno GetAlunoByTag( string code)
        {
            code = code.ToUpper();
            return context.Set<Aluno>().Include("Usuario").Include("Turma").Include("Tag").ToList().Where(a => a.Tag.Code.Equals(code)).FirstOrDefault() ;
        }

        public IEnumerable<Aluno> GetAlunosNessaTurma( int id)
        {
            var al = context.Set<Aluno>().Include("Usuario").Include("Turma").Include("Tag").Where( a => a.Turma.Any( t => t.TurmaId == id)).ToList() ;
            return al;
        }

        public IEnumerable<Aluno> GetAlunosNaoPertencentesNessaTurma(int id)
        {
            var al = context.Set<Aluno>().Include("Turma").ToList();


            al = al.Where(a => a.Turma.Count == 0 || a.Turma.All( t => t.TurmaId != id)   ).ToList();

            return al;
        }


        public void AddOuRemoveAlunoNaTurma(int id , List<int>  listaIdSelecionados)
        {

            List<int> listaParaRemover = new List<int>();

            //1.Verifico se ListFull esta em ListNew os que nao estiverem são selecionados para remoção.

           //Todos os  aluno dessa turma
           var todosAlunosDessaTurma = GetAlunosNessaTurma(id);

            //Alunos Selecionados
            var alunosSelecionados = context.Set<Aluno>().Include("Turma").Where(r => listaIdSelecionados.Contains(r.AlunoId)).ToList();

            //Lista para Exclusão
            var lsitaParaExclusão = todosAlunosDessaTurma.Distinct().Where(x => !alunosSelecionados.Any(e => x.AlunoId == e.AlunoId)).ToList();

            //2.Vou andar contrário na lista e Vou checar se ListNew esta em ListFull, os que não estiverem serão adicionados.

            //Lista para adicionar
            var alunosParaAdicionar = alunosSelecionados.Distinct().Where(x => !todosAlunosDessaTurma.Any(e => x.AlunoId == e.AlunoId)).ToList();

            Turma turma = null;

           
            turma = context.Turmas.Where(x => x.TurmaId == id).FirstOrDefault();

            //Adição dos alunos na turma
            foreach (var item in alunosParaAdicionar)
            {

                    turma.AlunoLista.Add(item);

            }

            foreach (var aluno in lsitaParaExclusão)
            {
                var contains = turma.AlunoLista.Contains( aluno);

                if (contains)
                {
                    turma.AlunoLista.Remove(aluno);
                }
            }

            context.Entry(turma).State = System.Data.Entity.EntityState.Modified;

            context.SaveChanges();
            

        }

        public Aluno GetAlunoByIdAllIncludes(int id)
        {
            return context.Set<Aluno>().Include("Usuario").Include("Turma").Include("Tag").Include("Presenca").ToList().Find(x => x.AlunoId == id);
        }

        public List<Usuario> GetAlunosComUsuarioPorIdTurma(int id)
        {
            //var l = context.Set<Aluno>().Include("Usuario").Include("Tag").ToList().Where(x => x.Turma.Select(t => t.TurmaId == id).FirstOrDefault()).ToList();
            
            

            var l = context.Aluno.Where(x => x.Turma.Select(t => t.TurmaId == id).FirstOrDefault()).ToList();

            var g = l.Select(x => x.UsuarioId).ToList();

            var h = context.Usuario.Where(r => g.Contains(r.UsuarioId)).ToList();

            //var listOfRoleId = user.Roles.Select(r => r.RoleId);
            //var roles = db.Roles.Where(r => listOfRoleId.Contains(r.RoleId));

            return h;
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
                //aluno.Tag = a.Tag;
                //aluno.Turma = alunoVm.Turma;
                //aluno.Usuario = a.Usuario;
                aluno.Imagem = a.Imagem;
                
                context.SaveChanges();
            }

        }

        public void AtualizaAluno(Aluno a, Usuario u  )
        {

            Aluno alunoUpdate = null;
            

            using (var context = new ControlePresencaContext())
            {

                alunoUpdate = context.Set<Aluno>().Include("Turma").Include("Usuario").Include("Tag").ToList().Where(x => x.UsuarioId == a.UsuarioId   ).FirstOrDefault();
                
            }

            using (var context = new ControlePresencaContext())
            {

                //alunoUpdate.AlunoId = a.AlunoId;
                alunoUpdate.Nome = a.Nome;
                alunoUpdate.NomeCompleto = a.NomeCompleto;
                alunoUpdate.DataNascimento = a.DataNascimento;
                alunoUpdate.Imagem = a.Imagem;
                alunoUpdate.Usuario = u;
                alunoUpdate.UsuarioId = alunoUpdate.UsuarioId;
                alunoUpdate.Tag = alunoUpdate.Tag;
                alunoUpdate.TagId = alunoUpdate.TagId;
                alunoUpdate.Turma = alunoUpdate.Turma;
                

                context.Entry(alunoUpdate).State = System.Data.Entity.EntityState.Modified;

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

            context.Usuario.Remove(obj.Usuario);

            context.Set<Aluno>().Remove(obj);
            context.SaveChanges();
        }

    }
}


