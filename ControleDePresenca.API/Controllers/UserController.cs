using ControleDePresenca.API.ViewModels;
using ControleDePresenca.Domain.Entities;
using ControleDePresenca.Domain.Interfaces.Repositories;
using ControleDePresenca.Domain.Services;
using ControleDePresenca.Infra.Data.Repositories;
using ControleDePresenca.LayerLog;
using ControleDePresenca.Notifications;
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

        UsuarioRepository _usuario;
        ITurmaRepository _turma;
        AlunoRepository _aluno;
        ProfessorRepository _professor;



        public UserController()
        {
            _usuario = new UsuarioRepository();
            _turma = new TurmaRepository();
            _aluno = new AlunoRepository();
            _professor = new ProfessorRepository();

        }


        [HttpPost]
        [Route("upload")]
        public HttpResponseMessage UploadFile()
        {
            try
            {

                var listUsuarios = _usuario.GetAll();

                return Request.CreateResponse(HttpStatusCode.OK, listUsuarios);

            }
            catch (Exception e)
            {
                Log log = new Log
                {
                    Message = e.Message,
                    Status = "error"
                };

                return Request.CreateResponse(HttpStatusCode.OK, log);
            }
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
                Log log = new Log
                {
                    Message = e.Message,
                    Status = "error"
                };

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
        [Route("GetPerfil/{id}")]
        public HttpResponseMessage GetUserPerfil(string id)
        {

            try
            {

                var _professor = new ProfessorRepository();
                var _aluno = new AlunoRepository();



                var usuario = _usuario.GetEntityById(int.Parse(id));

                if(  usuario.Perfil == 0 )
                {
                    var aluno = _aluno.GetAlunoByUsuarioId( usuario.UsuarioId  );

                    var perfil = new PerfilViewModel();
                    perfil.UserId = aluno.UsuarioId;
                    perfil.Nome = aluno.Nome;
                    perfil.NomeCompleto = aluno.NomeCompleto;
                    perfil.DataNascimento = aluno.DataNascimento;
                    perfil.Imagem = aluno.Imagem;

                    return Request.CreateResponse(HttpStatusCode.OK, perfil);

                }
                else if (  usuario.Perfil == 1 )
                {
                    var professor = _professor.GetProfessorByIdIncludesUserId(usuario.UsuarioId);

                    var perfil = new PerfilViewModel();
                    perfil.UserId = professor.UsuarioId;
                    perfil.Nome = professor.Nome;
                    perfil.NomeCompleto = professor.NomeCompleto;
                    perfil.DataNascimento = professor.DataNascimento;
                    perfil.Imagem = professor.Imagem;

                    return Request.CreateResponse(HttpStatusCode.OK, perfil);


                }

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


        [HttpPost]
        //[HttpGet]
        [Route("UpdatePerfil/{id}")]
        public HttpResponseMessage UpdatePerfil([FromBody] PerfilViewModel usuarioVm, string id)
        {

            try
            {

                var _professor = new ProfessorRepository();
                var _aluno = new AlunoRepository();



                var usuario = _usuario.GetEntityById(int.Parse(id));

                if (usuario.Perfil == 0)
                {
                    var aluno = new Aluno();

                    aluno.UsuarioId = usuario.UsuarioId;
                    aluno.Nome = usuarioVm.Nome ;
                    aluno.NomeCompleto = usuarioVm.NomeCompleto;
                    aluno.DataNascimento = usuarioVm.DataNascimento;
                    aluno.Imagem = usuarioVm.Imagem;

                    _aluno.AtualizaAluno(aluno, usuario);

                    return Request.CreateResponse(HttpStatusCode.OK, "Ok");

                }
                else if (usuario.Perfil == 1)
                {

                    var perfil = new Professor();
                    perfil.UsuarioId = usuario.UsuarioId;
                    perfil.Nome = usuarioVm.Nome;
                    perfil.NomeCompleto = usuarioVm.NomeCompleto;
                    perfil.DataNascimento = usuarioVm.DataNascimento;
                    perfil.Imagem = usuarioVm.Imagem;

                    _professor.AtualizaProfessor(perfil, usuario);

                    return Request.CreateResponse(HttpStatusCode.OK, "Ok");


                }

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

        //[HttpPost]
        //[Route("updateDados/{id}")]
        //public HttpResponseMessage UpdateDados([FromBody] UsuarioDadosViewModel usuarioVm, string id)
        //{

        //    try
        //    {

        //        var pr ofes= _usuario.GetEntityById(int.Parse(id));

        //        usuario.Email = usuarioVm.Email;
        //        usuario.Senha = usuarioVm.Senha.GetHashCode().ToString();

        //        _usuario.Update(usuario);

        //        Log log = new Log();
        //        log.Message = "The user was updated";
        //        log.Status = "success";
        //        return Request.CreateResponse(HttpStatusCode.OK, log);

        //    }
        //    catch (Exception e)
        //    {
        //        Log log = new Log();
        //        log.Message = e.Message;
        //        log.Status = "error";

        //        return Request.CreateResponse(HttpStatusCode.OK, log);
        //    }

        //}


        [HttpGet]
        [Route("ping")]

        public HttpResponseMessage Ping()
        {

            
               

                return Request.CreateResponse(HttpStatusCode.OK,"Funcionou");

           




        }

        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Login([FromBody] UsuarioViewModel usuarioVm)
        {

            try
            {

                Usuario usuario = new Usuario();

                usuario.Email = usuarioVm.Email;
                usuario.Senha = usuarioVm.Senha;

                Usuario user = _usuario.Login(usuario);

                if (usuarioVm.NotificacaoId != null || usuarioVm.NotificacaoId != string.Empty ) {
                    _usuario.UpdateIdNotification(usuarioVm.NotificacaoId, user.UsuarioId);
                }

                if (user != null) {
                    

                       

                    return Request.CreateResponse(HttpStatusCode.OK, user);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Error");

                }

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
