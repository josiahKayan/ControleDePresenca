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
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {

        public void AddUsuario(Usuario user)
        {
            context.Set<Usuario>().Add(user);
            context.SaveChanges();
        }

        public Usuario Login(Usuario user)
        {
            var usuario = context.Usuario.Where(u => u.Email.Equals(user.Email) && u.Senha.Equals(user.Senha)).FirstOrDefault();

            if (usuario != null)
            {
                return usuario;
            }
            else
            {
                return null;
            }
        }

        public void UpdateIdNotification(string idRegister, int idUser)
        {
            using (var context = new ControlePresencaContext())
            {

                var u = context.Usuario.Find(idUser);

                u.NotificacaoId = idRegister;

                context.Entry(u).State = System.Data.Entity.EntityState.Modified;

                context.SaveChanges();
            }

        }

    }
    
}
