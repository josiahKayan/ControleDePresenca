using ControleDePresenca.Domain.Entities;
using ControleDePresenca.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleDePresenca.Domain.Interfaces.Repositories;

namespace ControleDePresenca.Domain.Services
{
    public class UsuarioService : ServiceBase<Usuario>, IUsuarioService
    {

        private readonly IUsuarioRepository _userRepository;

        public UsuarioService(IUsuarioRepository repository) : base(repository)
        {

            _userRepository = repository;
        }

        public void AddUsuario(Usuario user)
        {
            _userRepository.AddUsuario(user);
        }
    }
}
