using ControleDePresenca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleDePresenca.Domain.Interfaces.Services;

namespace ControleDePresenca.Application.Interfaces
{
    public class UsuarioAppService : AppServiceBase<Usuario>, IUsuarioAppService
    {

        private readonly IUsuarioService _usuariService;
        private readonly ITagService _tagService;


        public UsuarioAppService(IUsuarioService service, ITagService tagservice) : base(service)
        {

            _usuariService = service ;
            _tagService = tagservice;
        }

        public void AddUsuario(Usuario user)
        {
            _usuariService.AddUsuario(user);
        }
    }
}
