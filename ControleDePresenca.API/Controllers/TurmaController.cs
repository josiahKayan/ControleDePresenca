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
    [RoutePrefix("turma")]
    public class TurmaController : ApiController
    {

        ITurmaRepository _turma;

        public TurmaController()
        {
            _turma = new TurmaRepository();
        }

        /// <summary>
        /// Lista de Turmas
        /// </summary>
        /// <remarks>
        /// Exibe uma lista de Turmas
        /// </remarks>
        /// <returns> Lista de Turmas</returns>
        /// <response code="200">Lista de Turmas</response>
        /// <response code="404">Turmas not founded</response>
        [HttpGet]
        [Route("turmas")]
        public HttpResponseMessage GetTurmas()
        {

            try
            {

                var listTurmas = _turma.GetAll();

                return Request.CreateResponse(HttpStatusCode.OK, listTurmas);

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }

        /// <summary>
        /// Método para adicionar uma turma
        /// </summary>
        /// <remarks>
        /// Método que adiciona
        /// </remarks>
        /// <param name="turmaVm">CursoViewModel</param>
        /// <returns></returns>
        /// <response code="200">Turma</response>
        /// <response code="404">Turma not foundd</response>
        [HttpPost]
        [Route("addturma")]
        public HttpResponseMessage NewTurma([FromBody] TurmaViewModel turmaVm)
        {
            var _professor = new ProfessorRepository();
            var _curso = new CursoRepository();

            try
            {

                Turma turma = new Turma();

                turma.NomeTurma = turmaVm.NomeTurma;
                turma.DataInicio = Convert.ToDateTime( turmaVm.DataInicio );
                turma.DataTermino = Convert.ToDateTime( turmaVm.DataTermino );

                turma.HoraFinal = Convert.ToDateTime(turmaVm.HoraFinal);
                turma.HoraInicial = Convert.ToDateTime(turmaVm.HoraInicial);

                turma.CursoId = turmaVm.CursoId;
                turma.ProfessorId = turmaVm.ProfessorId;
                
                _turma.AddTurmaNoCurso(turma);

                return Request.CreateResponse(HttpStatusCode.OK, "Ok");

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }

        /// <summary>
        /// Get turma by id
        /// </summary>
        /// <remarks>
        /// Get a Turma by id
        /// </remarks>
        /// <param name="id">Id da turma</param>
        /// <returns></returns>
        /// <response code="200">Turma found</response>
        /// <response code="404">Turma not founded</response>
        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetTurma(string id)
        {

            try
            {

                var turma = _turma.GetEntityById(int.Parse(id));

                return Request.CreateResponse(HttpStatusCode.OK, turma);

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }


        /// <summary>
        /// Get turma by id
        /// </summary>
        /// <remarks>
        /// Get a Turma by id
        /// </remarks>
        /// <param name="id">Id da turma</param>
        /// <returns></returns>
        /// <response code="200">Turma found</response>
        /// <response code="404">Turma not founded</response>
        [HttpGet]
        [Route("GetTurmasPeloCursoId/{id}")]
        public HttpResponseMessage GetTurmaPeloId(string id)
        {

            try
            {

                var turma = _turma.GetTurmasPeloCursoId(int.Parse(id));

                return Request.CreateResponse(HttpStatusCode.OK, turma);

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }

        [HttpGet]
        [Route("GetTurmasPeloUsuarioId/{id}")]
        public HttpResponseMessage GetTurmasPeloUsuarioId(string id)
        {

            try
            {
                var _usuario = new UsuarioRepository();

                List<Turma> turma = null;

                var usuario = _usuario.GetEntityById(int.Parse(id));

                if (usuario.Perfil == 1) {
                    turma = _turma.GetTurmasPeloUsuarioId(int.Parse(id));
                }
                else if (usuario.Perfil == 0)
                {
                    turma = _turma.GetTurmasPeloUsuarioAlunoId(int.Parse(id));

                }

                return Request.CreateResponse(HttpStatusCode.OK, turma);

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }

        

        /// <summary>
        /// Get turma by id
        /// </summary>
        /// <remarks>
        /// Get a Turma by id
        /// </remarks>
        /// <param name="id">Id da Turma</param>
        /// <returns></returns>
        /// <response code="200">Turma found</response>
        /// <response code="404">Turma not founded</response>
        [HttpGet]
        [Route("delete/{id}")]
        public HttpResponseMessage DeleteTurma(string id)
        {

            try
            {

                var turma = _turma.GetEntityById(int.Parse(id));

                _turma.Remove(turma);

                return Request.CreateResponse(HttpStatusCode.OK, "The turma was removed");

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }

        /// <summary>
        /// Get turma by id
        /// </summary>
        /// <remarks>
        /// Get a Turma by id
        /// </remarks>
        /// <param name="id">Id de Turma</param>
        /// <param name="turmaVm">Id of course</param>
        /// <returns></returns>
        /// <response code="200">Curso found</response>
        /// <response code="404">Curso not foundd</response>
        [HttpPost]
        [Route("update/{id}")]
        public HttpResponseMessage UpdateTurma([FromBody] TurmaViewModel turmaVm, string id)
        {

            var _professor = new ProfessorRepository();
            var _curso = new CursoRepository();

            try
            {

                Turma turma = new Turma();

                turma.TurmaId = int.Parse( id );

                turma.NomeTurma = turmaVm.NomeTurma;

                turma.DataInicio = Convert.ToDateTime(turmaVm.DataInicio);
                turma.DataTermino = Convert.ToDateTime(turmaVm.DataTermino);

                turma.HoraFinal = Convert.ToDateTime(turmaVm.HoraFinal);
                turma.HoraInicial = Convert.ToDateTime(turmaVm.HoraInicial);

                turma.CursoId = turmaVm.CursoId;
                turma.ProfessorId = turmaVm.ProfessorId;

                //_turma.UpdateTurmaNoCurso(turma);
                _turma.UpdateTurmaNoCurso(turma);


                return Request.CreateResponse(HttpStatusCode.OK, "The course was updated");

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }


    }
}
