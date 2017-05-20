using ControleDePresenca.Domain.Entities;
using ControleDePresenca.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDePresenca.Infra.Data.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario> , IUsuarioRepository
    {

        public void AddUsuario(Usuario user)
        {
            context.Set<Usuario>().Add(user);
            context.SaveChanges();
        }

    }


    
}
