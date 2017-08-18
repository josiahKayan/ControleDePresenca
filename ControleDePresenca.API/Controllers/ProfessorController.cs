using ControleDePresenca.API.ViewModels;
using ControleDePresenca.Domain.Entities;
using ControleDePresenca.Domain.Interfaces.Repositories;
using ControleDePresenca.Infra.Data.Repositories;
using ControleDePresenca.Library.Log;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ControleDePresenca.API.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("professor")]
    public class ProfessorController : ApiController
    {

        IProfessorRepository _professor;
        Log log;

        public ProfessorController()
        {
            _professor = new ProfessorRepository();
        }

        /// <summary>
        ///  Get all professores
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [Route("professores")]
        public HttpResponseMessage GetProfessores()
        {

            try
            {

                ProfessorViewModel prof = new ProfessorViewModel();

                var listProfs = _professor.GetAllProfessors();

                var list = prof.ParserProfessor(listProfs.ToList());

                return Request.CreateResponse(HttpStatusCode.OK, list);

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }

        [HttpPost]
        [Route("addprofessor")]
        public HttpResponseMessage NewUser([FromBody] ProfessorViewModel professorVm)
        {
            Professor professor;

            try
            {
                if (professorVm.ProfessorId != 0)
                {
                    professor = _professor.GetEntityById(professorVm.ProfessorId);

                    if (professor != null)
                    {

                        professor.Nome = professorVm.Nome;
                        professor.NomeCompleto = professorVm.NomeCompleto;
                        professor.Idade = int.Parse(professorVm.Idade);
                        professor.DataNascimento = DateTime.ParseExact(professorVm.DataNascimento.Replace("/", ""), "ddMMyyyy", CultureInfo.InvariantCulture);
                        professor.CursoLista = professorVm.CursoLista;
                        professor.Usuario = new Usuario();

                        professor.Usuario.Email = professorVm.Usuario.Email;
                        professor.Usuario.Senha = professorVm.Usuario.Senha.GetHashCode().ToString();
                        professor.Usuario.UsuarioId = professorVm.Usuario.UsuarioId;
                        professor.CursoLista = professorVm.CursoLista;
                        professor.TurmaLista = professorVm.TurmaLista;
                        _professor.Update(professor);
                        log = new Log();
                        log.Message = "The object was updated";
                        log.Status = 1;
                        log.Type = "success";
                        return Request.CreateResponse(HttpStatusCode.OK, log);
                    }
                }

                professor = new Professor();
                professor.Nome = professorVm.Nome;
                professor.NomeCompleto = professorVm.NomeCompleto;
                professor.Idade = int.Parse(professorVm.Idade);
                
                professor.DataNascimento = DateTime.ParseExact(professorVm.DataNascimento.Replace("/",""), "ddMMyyyy", CultureInfo.InvariantCulture); 
                professor.Usuario = new Usuario();
                professor.Usuario.Email = professorVm.Usuario.Email;
                professor.Usuario.Senha = professorVm.Usuario.Senha.GetHashCode().ToString();
                professor.UsuarioId = professorVm.UsuarioId;
                professor.CursoLista = professorVm.CursoLista;
                professor.TurmaLista = professorVm.TurmaLista;
                _professor.Add(professor);
                log = new Log();
                log.Message = "The object was added";
                log.Status = 1;
                log.Type = "success";
                return Request.CreateResponse(HttpStatusCode.OK, log);


            }
            catch (Exception e)
            {
                log = new Log();
                log.Message = e.Message;
                log.Status = 0;
                log.Type = "error";
                return Request.CreateResponse(HttpStatusCode.OK, log);
            }

        }

        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetProfessor(string id)
        {

            try
            {

                var professor = _professor.GetProfessorByIdIncludes(int.Parse(id));
                
                ProfessorViewModel professorVm = new ProfessorViewModel();
                professorVm.Nome = professor.Nome;
                professorVm.NomeCompleto = professor.NomeCompleto;
                professorVm.Idade = ""+professor.Idade;
                professorVm.DataNascimento = professor.DataNascimento.Date.ToString("dd/MM/yyyy");
                professorVm.ProfessorId = professor.ProfessorId;
                professorVm.Usuario = new Usuario();
                professorVm.Usuario.Email = professor.Usuario.Email;
                professorVm.Usuario.Senha = null;
                professorVm.Usuario.Perfil = professor.Usuario.Perfil;
                professorVm.Usuario.UsuarioId = professor.Usuario.UsuarioId;
                professorVm.UsuarioId = professor.UsuarioId;
                professorVm.CursoLista = professor.CursoLista;
                professorVm.TurmaLista = professor.TurmaLista;
                return Request.CreateResponse(HttpStatusCode.OK, professorVm);

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }

        [HttpPost]
        [Route("delete/{id}")]
        public HttpResponseMessage DeleteProfessor(string id)
        {

            ProfessorRepository _professor = new ProfessorRepository();

            try
            {

                Professor professor = _professor.GetEntityById(int.Parse(id));

                if (professor == null)
                {
                    Exception e = new Exception("The object was not found");
                    throw e;
                }

                _professor.RemoveComUsuario(professor);
                log = new Log();
                log.Message = "The object was removed";
                log.Status = 1;
                log.Type = "success";
                return Request.CreateResponse(HttpStatusCode.OK, log);

            }
            catch (Exception e)
            {
                log = new Log();
                log.Message = e.Message;
                log.Status = 0;
                log.Type = "error";
                return Request.CreateResponse(HttpStatusCode.OK, log);
            }

        }

        [HttpPut]
        [Route("update/{id}")]
        public HttpResponseMessage UpdateProfessor([FromBody] ProfessorViewModel professorVm, string id)
        {

            Professor pf = new Professor();

            try
            {

                Professor professor = _professor.GetEntityById(int.Parse(id));

                professor.Nome = professorVm.Nome;
                professor.NomeCompleto = professorVm.NomeCompleto;
                professor.Idade = int.Parse(professorVm.Idade);
                professor.DataNascimento = DateTime.Now;


                _professor.Update(professor);

                return Request.CreateResponse(HttpStatusCode.OK, pf);

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }

    }
}
