using ControleDePresenca.API.ViewModels;
using ControleDePresenca.Domain.Entities;
using ControleDePresenca.Domain.Interfaces.Repositories;
using ControleDePresenca.Infra.Data.Repositories;
using ControleDePresenca.Library.Log;
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

                var listProfessores = _professor.GetAllProfessors();

                return Request.CreateResponse(HttpStatusCode.OK, listProfessores);

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
                        professor.DataNascimento = DateTime.Now;
                        professor.Usuario.Email = professorVm.Usuario.Email;
                        professor.Usuario.Senha = professorVm.Usuario.Senha.GetHashCode().ToString();
                        professor.Usuario.UsuarioId = professorVm.Usuario.UsuarioId;
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
                professor.DataNascimento = DateTime.Now;
                professor.Usuario.Email = professorVm.Usuario.Email;
                professor.Usuario.Senha = professorVm.Usuario.Senha.GetHashCode().ToString();
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

                return Request.CreateResponse(HttpStatusCode.OK, professor);

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
