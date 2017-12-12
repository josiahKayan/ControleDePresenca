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
    [RoutePrefix("curso")]
    public class CursoController : ApiController
    {

        ICursoRepository _curso;
        Log log;

        public CursoController()
        {
            _curso = new CursoRepository();
        }

        /// <summary>
        /// Lista de Cursos
        /// </summary>
        /// <remarks>
        /// Exibe uma lista de cursos
        /// </remarks>
        /// <returns> Lista de Cursos</returns>
        /// <response code="200">Lista de Cursos</response>
        /// <response code="404">Superhero not foundd</response>
        [HttpGet]
        [Route("cursos")]
        public HttpResponseMessage GetCursos()
        {

            try
            {



                var listCursos = _curso.GetAll();

                List<CursoViewModel> curso = new List<CursoViewModel>();

                foreach (var item in listCursos)
                {
                    CursoViewModel c = new CursoViewModel();

                    c.Ativo = item.Ativo;
                    c.CursoId = item.CursoId;
                    c.Descricao = item.Descricao;
                    c.Nome = item.Nome;
                    //c.PeriodoFinal = item.PeriodoFinal.ToString("dd/MM/yyyy"); ;
                    //c.PeriodoInicial = item.PeriodoInicial.ToString("dd/MM/yyyy"); ;
                    //c.ProfessorLista = item.ProfessorLista;
                    c.TurmaLista = item.TurmaLista;

                    curso.Add(c);
                }

                return Request.CreateResponse(HttpStatusCode.OK, curso);

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }

        /// <summary>
        /// Método para adicionar um curso
        /// </summary>
        /// <remarks>
        /// Método que adiciona
        /// </remarks>
        /// <param name="cursoVm">CursoViewModel</param>
        /// <returns></returns>
        /// <response code="200">Curso</response>
        /// <response code="404">Curso not foundd</response>
        [HttpPost]
        [Route("addcurso")]
        public HttpResponseMessage NewCurso([FromBody] CursoViewModel cursoVm)
        {

            try
            {
                Curso cursoFound;
                if (cursoVm.CursoId != 0)
                {
                    cursoFound = _curso.GetEntityById(cursoVm.CursoId);

                    if (cursoFound != null)
                    {

                        cursoFound.Nome = cursoVm.Nome;
                        cursoFound.Descricao = cursoVm.Descricao;
                        cursoFound.Ativo = cursoVm.Ativo;
                        //cursoFound.ProfessorLista = cursoVm.ProfessorLista;
                        cursoFound.TurmaLista = cursoVm.TurmaLista;
                        //cursoFound.PeriodoInicial = DateTime.ParseExact(cursoVm.PeriodoInicial.Replace("/", ""), "ddMMyyyy", CultureInfo.InvariantCulture);
                        //cursoFound.PeriodoFinal = DateTime.ParseExact(cursoVm.PeriodoFinal.Replace("/", ""), "ddMMyyyy", CultureInfo.InvariantCulture);


                        _curso.Update(cursoFound);
                        log = new Log();
                        log.Message = "The object was updated";
                        log.Status = 1;
                        log.Type = "success";
                        return Request.CreateResponse(HttpStatusCode.OK, log);
                    }
                }

                cursoFound = new Curso();
                cursoFound.Nome = cursoVm.Nome;
                cursoFound.Descricao = cursoVm.Descricao;
                cursoFound.Ativo = cursoVm.Ativo;
                //cursoFound.ProfessorLista = cursoVm.ProfessorLista;

                //cursoFound.PeriodoInicial = DateTime.ParseExact(cursoVm.PeriodoInicial.Replace("/", ""), "ddMMyyyy", CultureInfo.InvariantCulture);
                //cursoFound.PeriodoFinal = DateTime.ParseExact(cursoVm.PeriodoFinal.Replace("/", ""), "ddMMyyyy", CultureInfo.InvariantCulture);


                _curso.Add(cursoFound);
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

        /// <summary>
        /// Get curso by id
        /// </summary>
        /// <remarks>
        /// Get a Curso by id
        /// </remarks>
        /// <param name="id">Id of course</param>
        /// <returns></returns>
        /// <response code="200">Curso found</response>
        /// <response code="404">Curso not foundd</response>
        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetCurso(string id)
        {

            try
            {

                var curso = _curso.GetEntityById(int.Parse(id));

                return Request.CreateResponse(HttpStatusCode.OK, curso);

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }




        /// <summary>
        /// Get curso by id
        /// </summary>
        /// <remarks>
        /// Get a Curso by id
        /// </remarks>
        /// <param name="id">Id of course</param>
        /// <returns></returns>
        /// <response code="200">Curso found</response>
        /// <response code="404">Curso not foundd</response>
        [HttpPost]
        [Route("delete/{id}")]
        public HttpResponseMessage DeleteCurso(string id)
        {
            
            try
            {

                Curso cursoFounded = _curso.GetCursoByIdIncludesTurma(int.Parse(id));

                if (cursoFounded.TurmaLista.Count < 1)
                {
                    _curso.Remove(cursoFounded);
                    log = new Log();
                    log.Message = "O curso foi removido com sucesso";
                    log.Status = 1;
                    log.Type = "success";
                    return Request.CreateResponse(HttpStatusCode.OK, log);
                }

                log = new Log();
                log.Message = "Exclua primeiro a turma";
                log.Status = 0;
                log.Type = "error";
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

        /// <summary>
        /// Get curso by id
        /// </summary>
        /// <remarks>
        /// Get a Curso by id
        /// </remarks>
        /// <param name="id">Id of course</param>
        /// <param name="cursoVm">Id of course</param>
        /// <returns></returns>
        /// <response code="200">Curso found</response>
        /// <response code="404">Curso not foundd</response>
        [HttpPut]
        [Route("update/{id}")]
        public HttpResponseMessage UpdateCurso([FromBody] CursoViewModel cursoVm, string id)
        {

            try
            {

                Curso curso = _curso.GetEntityById(int.Parse(id));

                curso.Nome = cursoVm.Nome;
                curso.Descricao = cursoVm.Descricao;
                curso.Ativo = cursoVm.Ativo;
                //curso.ProfessorLista = cursoVm.ProfessorLista;
                curso.TurmaLista = cursoVm.TurmaLista;
                //curso.PeriodoInicial = DateTime.ParseExact(cursoVm.PeriodoInicial.Replace("/", ""), "ddMMyyyy", CultureInfo.InvariantCulture);
                //curso.PeriodoFinal = DateTime.ParseExact(cursoVm.PeriodoFinal.Replace("/", ""), "ddMMyyyy", CultureInfo.InvariantCulture);


                _curso.Update(curso);
                log = new Log();
                log.Message = "The object was updated";
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


    }
}
