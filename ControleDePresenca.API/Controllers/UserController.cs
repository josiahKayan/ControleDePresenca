using ControleDePresenca.API.ViewModels;
using ControleDePresenca.Domain.Entities;
using ControleDePresenca.Domain.Interfaces.Repositories;
using ControleDePresenca.Domain.Services;
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

    [EnableCors(origins:"*",headers:"*",methods:"*")]
    [RoutePrefix("user")]
    public class UserController : ApiController
    {

        IUsuarioRepository _usuario;

        public UserController()
        {
            _usuario = new UsuarioRepository();
        }

        [HttpGet]
        [Route("users")]
        public HttpResponseMessage GetUsers()
        {

            try
            {

                var listUsuarios = _usuario.GetAll();

                return Request.CreateResponse(HttpStatusCode.OK, listUsuarios);

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }

        [HttpPost]
        [Route("adduser")]
        public HttpResponseMessage NewUser([FromBody] UsuarioViewModel usuarioVm)
        {

            try
            {

                Usuario usuario = new Usuario();

                usuario.Email = usuarioVm.Email;
                usuario.Senha = usuarioVm.Senha;

                _usuario.Add(usuario);

                return Request.CreateResponse(HttpStatusCode.OK, "Ok");

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }

        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetUser( string id)
        {

            try
            {

                var usuario = _usuario.GetEntityById(int.Parse(id));

                return Request.CreateResponse(HttpStatusCode.OK, usuario);

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }

        [HttpDelete]
        [Route("delete/{id}")]
        public HttpResponseMessage DeleteUser(string id)
        {

            try
            {

                var usuario = _usuario.GetEntityById(int.Parse(id));

                _usuario.Remove(usuario);

                return Request.CreateResponse(HttpStatusCode.OK, "The user was removed");

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }

        [HttpPut]
        [Route("update/{id}")]
        public HttpResponseMessage UpdateUser([FromBody] UsuarioViewModel usuarioVm, string id)
        {

            try
            {

                var usuario = _usuario.GetEntityById(int.Parse(id));

                usuario.Email = usuarioVm.Email;
                usuario.Senha = usuarioVm.Senha;

                _usuario.Update(usuario);

                return Request.CreateResponse(HttpStatusCode.OK, "The user was updated");

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }
    }
}
