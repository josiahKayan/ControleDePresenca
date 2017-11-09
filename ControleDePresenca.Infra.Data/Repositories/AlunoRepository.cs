using ControleDePresenca.Domain.Entities;
using ControleDePresenca.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Infra.Data.Repositories
{
    public class AlunoRepository : RepositoryBase<Aluno>, IAlunoRepository
    {

        public IEnumerable<Aluno> GetAllAlunos()
        {
            return context.Set<Aluno>().Include("Usuario").Include("Turma").Include("Tag").ToList();
        }

        public Aluno GetAlunoByIdIncludes(int id)
        {
            return context.Set<Aluno>().Include("Usuario").Include("Turma").Include("Tag").Include("Presenca").ToList().Find(x => x.AlunoId == id);
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


