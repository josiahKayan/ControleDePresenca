using ControleDePresenca.API.Responses;
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
    [RoutePrefix("usuario")]
    public class UserController : ApiController
    {

        IUsuarioRepository _usuario;

        public UserController()
        {
            _usuario = new UsuarioRepository();
        }

        [HttpGet]
        [Route("usuarios")]
        public HttpResponseMessage GetUsers()
        {

            try
            {

                var listUsuarios = _usuario.GetAll();

                return Request.CreateResponse(HttpStatusCode.OK, listUsuarios);

            }
            catch (Exception e)
            {
                Log log = new Log();
                log.Message = e.Message;
                log.Status = "error";

                return Request.CreateResponse(HttpStatusCode.OK, log);
            }

        }

        [HttpPost]
        [Route("addusuario")]
        public HttpResponseMessage NewUser([FromBody] UsuarioViewModel usuarioVm)
        {

            try
            {

                Usuario usuario = new Usuario();

                usuario.Email = usuarioVm.Email;
                usuario.Senha = usuarioVm.Senha.GetHashCode().ToString();

                _usuario.Add(usuario);

                Log log = new Log();
                log.Message = "The object was added";
                log.Status = "success";

                return Request.CreateResponse(HttpStatusCode.OK, log);

            }
            catch (Exception e)
            {
                Log log = new Log();
                log.Message = e.Message;
                log.Status = "error";
                return Request.CreateResponse(HttpStatusCode.OK, log);
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

                Log log = new Log();
                log.Message = e.Message;
                log.Status = "error";
                return Request.CreateResponse(HttpStatusCode.OK, log);
            }

        }

        [HttpGet]
        [Route("delete/{id}")]
        public HttpResponseMessage DeleteUser(string id)
        {

            try
            {

                var usuario = _usuario.GetEntityById(int.Parse(id));

                _usuario.Remove(usuario);

                Log log = new Log();
                log.Message = "The user was removed";
                log.Status = "success";

                return Request.CreateResponse(HttpStatusCode.OK, log);

            }
            catch (Exception e)
            {
                Log log = new Log();
                log.Message = e.Message;
                log.Status = "error";

                return Request.CreateResponse(HttpStatusCode.OK, log);
            }

        }

        [HttpPost]
        [Route("update/{id}")]
        public HttpResponseMessage UpdateUser([FromBody] UsuarioViewModel usuarioVm, string id)
        {

            try
            {

                var usuario = _usuario.GetEntityById(int.Parse(id));

                usuario.Email = usuarioVm.Email;
                usuario.Senha = usuarioVm.Senha.GetHashCode().ToString();

                _usuario.Update(usuario);

                Log log = new Log();
                log.Message = "The user was updated";
                log.Status = "success";
                return Request.CreateResponse(HttpStatusCode.OK, log);

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
