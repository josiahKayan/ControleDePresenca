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
        public UsuarioViewModel Logar([FromBody] UsuarioViewModel usuarioViewModel)
        {

            try
            {

                //// Compose a string that consists of three lines.
                //string lines = "Escrevendo no arquivo txt o que foi recebido";

                //lines = lines + "\n" + usuarioViewModel.Email;

                //lines = lines + "\n" + usuarioViewModel.Perfil;

                //lines = lines + "\n" + usuarioViewModel.Senha;

                //lines = lines + "\n" + usuarioViewModel.UsuarioId;

                //// Write the string to a file.
                //System.IO.StreamWriter file = new System.IO.StreamWriter("C:\\Temp\\test.txt");
                //file.WriteLine(lines);

                //file.Close();

                Usuario newUsuario = new Usuario();

                newUsuario.Email = usuarioViewModel.Email;
                newUsuario.Senha = usuarioViewModel.Senha.GetHashCode().ToString();

                var usuarioLogado = _usuario.Login(newUsuario);

                usuarioViewModel.Email = usuarioLogado.Email;
                usuarioViewModel.Senha = usuarioLogado.Senha;
                usuarioViewModel.UsuarioId = usuarioLogado.UsuarioId;
                usuarioViewModel.Perfil = usuarioLogado.Perfil;

                // return Request.CreateResponse(HttpStatusCode.OK, usuarioViewModel);
                return usuarioViewModel;
            }
            catch (Exception e)
            {

                Log log = new Log();
                log.Message = e.Message;
                log.Status = "error";

                return usuarioViewModel;
            }

        }


    }
}
