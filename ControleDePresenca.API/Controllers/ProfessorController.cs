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
    [RoutePrefix("professor")]
    public class ProfessorController : ApiController
    {

        ProfessorRepository _professor;

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

            try
            {

                Professor professor = new Professor();

                professor.Nome = professorVm.Nome;
                professor.NomeCompleto = professorVm.NomeCompleto;
                professor.Idade = professorVm.Idade;
                professor.DataNascimento = professorVm.DataNascimento;
                professor.Imagem = professorVm.Imagem;
                professor.Usuario = professorVm.Usuario;
                _professor.Add(professor);

                return Request.CreateResponse(HttpStatusCode.OK, "Ok");

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
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

        [HttpGet]
        [Route("delete/{id}")]
        public HttpResponseMessage DeleteProfessor(string id)
        {

            try
            {

                var professor = _professor.GetEntityById(int.Parse(id));

                _professor.Remove(professor);

                return Request.CreateResponse(HttpStatusCode.OK, "The teacher was removed");

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }

        [HttpPost]
        [Route("update/{id}")]
        public HttpResponseMessage UpdateProfessor( [FromBody] ProfessorViewModel professorVm, string id)
        {

            try
            {

                Professor professor = new Professor();

                professor.ProfessorId = professorVm.ProfessorId;
                professor.Nome = professorVm.Nome;
                professor.NomeCompleto = professorVm.NomeCompleto;
                professor.Idade = professorVm.Idade;
                professor.DataNascimento = professorVm.DataNascimento;
                professor.Imagem = professorVm.Imagem;
                professor.TurmaLista = professorVm.TurmaLista;



                _professor.UpdateTeacher(professor);

                return Request.CreateResponse(HttpStatusCode.OK, "Ok");

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.InnerException.Message);
            }

        }


        [HttpGet]
        [Route("usuario/{id}")]
        public HttpResponseMessage GetAlunoByUsuario(string id)
        {

            try
            {

                var professor = _professor.GetProfessorByIdIncludesUserId(int.Parse(id));

                return Request.CreateResponse(HttpStatusCode.OK, professor);

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }

    }
}
