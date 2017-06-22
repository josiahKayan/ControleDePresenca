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
                //List<Professor> listProfessores = new List<Professor>();
                //Professor professor1 = new Professor();
                //professor1.Nome = "Ismael";
                //professor1.NomeCompleto = "Ismael Ismael";
                //professor1.ProfessorId = 1;
                //professor1.DataNascimento = DateTime.Now;
                //professor1.Idade = 25;


                //Professor professor2 = new Professor();
                //professor2.Nome = "Patrícia";
                //professor2.NomeCompleto = "Patrícia PATT";
                //professor2.ProfessorId = 2;
                //professor2.DataNascimento = DateTime.Now;
                //professor2.Idade = 23;

                //listProfessores.Add(professor1);
                //listProfessores.Add(professor2);


                var listProfessores = _professor.GetAll();

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

                var professor = _professor.GetEntityById(int.Parse(id));

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

            Professor pf = new Professor();

            try
            {

                Professor professor = _professor.GetEntityById(int.Parse(id));

                _professor.Remove(professor);
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
