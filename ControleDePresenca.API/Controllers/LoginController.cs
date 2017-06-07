using ControleDePresenca.API.Responses;
using ControleDePresenca.API.ViewModels;
using ControleDePresenca.Domain.Entities;
using ControleDePresenca.Domain.Interfaces.Repositories;
using ControleDePresenca.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ControleDePresenca.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("login")]
    public class LoginController : ApiController
    {
        IUsuarioRepository _usuario;


        public LoginController()
        {
            _usuario = new UsuarioRepository();
        }

        [HttpPost]
        [Route("logar")]
        public HttpResponseMessage Logar([FromBody] UsuarioViewModel usuarioViewModel)
        {

            try
            {

                Usuario newUsuario = new Usuario();

                newUsuario.Email = usuarioViewModel.Email;
                newUsuario.Senha = usuarioViewModel.Senha.GetHashCode().ToString();

                var usuarioLogado = _usuario.Login(newUsuario);

                return Request.CreateResponse(HttpStatusCode.OK, usuarioLogado);

            }
            catch (Exception e)
            {

                Log log = new Log();
                log.Message = e.Message;
                log.Status = "error";

                return Request.CreateResponse(HttpStatusCode.OK, log);
            }

        }


    }
}
