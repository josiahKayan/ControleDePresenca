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
    [RoutePrefix("resumo-presenca")]
    public class PresencaController : ApiController
    {

        IPresencaRepository _presenca;
        ITurmaRepository _turma;


        public PresencaController()
        {
            _presenca = new PresencaRepository();
            _turma = new TurmaRepository();

        }

        /// <summary>
        /// Lista de Presenças
        /// </summary>
        /// <remarks>
        /// Exibe uma lista de presenças
        /// </remarks>
        /// <returns> Lista de Presenças</returns>
        /// <response code="200">Lista de Presenças</response>
        /// <response code="404">Presença not foundd</response>
        [HttpGet]
        [Route("GetResumoListaPresencaByIdPresencalista/{id}")]
        public HttpResponseMessage GetResumoListaPresencaByIdPresencalista(int id)
        {

            try
            {

                var listPresenca = _presenca.GetResumoListaPresencaByIdPresencalista(id);

                return Request.CreateResponse(HttpStatusCode.OK, listPresenca);

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }


        /// <summary>
        /// Lista de Presenças
        /// </summary>
        /// <remarks>
        /// Exibe uma lista de presenças
        /// </remarks>
        /// <returns> Lista de Presenças</returns>
        /// <response code="200">Lista de Presenças</response>
        /// <response code="404">Presença not foundd</response>
        [HttpGet]
        [Route("GetResumoListaFaltosos/{id}/{idTurma}")]
        public HttpResponseMessage GetResumoListaFaltosos(int id, int idTurma)
        {

            try
            {
                //Lista dos alunos presentes
                List<Aluno> listPresencaAluno = _presenca.GetResumoListaPresencaByIdPresencalista(id).Select( x => x.Aluno).ToList();

                var t = new TurmaRepository();

                var alunosTurma = t.GetAlunosByTurmaId(idTurma);

                //Lista dos alunos da turma
                var listaAlunoTurma = alunosTurma.Select(x => x.AlunoLista.ToList()).FirstOrDefault().ToList();

                //Lista dos faltosos
                var listFaltosos = listaAlunoTurma.Where(  x => listPresencaAluno.Select( a => a.AlunoId != x.AlunoId ).FirstOrDefault()     ) ;

                listFaltosos = listFaltosos.ToList();

                if (listPresencaAluno.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, listFaltosos);

                }

                return Request.CreateResponse(HttpStatusCode.OK, listaAlunoTurma);

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
        /// <param name="presencaVm">CursoViewModel</param>
        /// <returns></returns>
        /// <response code="200">Curso</response>
        /// <response code="404">Curso not foundd</response>
        [HttpPost]
        [Route("addpresenca")]
        public HttpResponseMessage NewCurso([FromBody] PresencaViewModel presencaVm)
        {

            try
            {

                Domain.Entities.ListaPresenca presenca = new Domain.Entities.ListaPresenca();

                presenca.HoraEntrada = presencaVm.HoraEntrada;
                presenca.Mes = presencaVm.Mes;
                presenca.Ano = presencaVm.Ano;
                _presenca.Add(presenca);

                return Request.CreateResponse(HttpStatusCode.OK, "Ok");

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }

        /// <summary>
        /// Get presenca by id
        /// </summary>
        /// <remarks>
        /// Get a Presenças by id
        /// </remarks>
        /// <param name="id">Id de Presenças</param>
        /// <returns></returns>
        /// <response code="200">Presença found</response>
        /// <response code="404">Presença not foundd</response>
        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetPresenca(string id)
        {

            try
            {

                var presenca = _presenca.GetEntityById(int.Parse(id));

                return Request.CreateResponse(HttpStatusCode.OK, presenca);

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }

        /// <summary>
        /// Deleta Presença by id
        /// </summary>
        /// <remarks>
        /// Deleta uma Presença by id
        /// </remarks>
        /// <param name="id">Id d Presenças</param>
        /// <returns></returns>
        /// <response code="200">Presença found</response>
        /// <response code="404">Presença not foundd</response>
        [HttpGet]
        [Route("delete/{id}")]
        public HttpResponseMessage DeletePresenca(string id)
        {

            try
            {

                var presenca = _presenca.GetEntityById(int.Parse(id));

                _presenca.Remove(presenca);

                return Request.CreateResponse(HttpStatusCode.OK, "The course was removed");

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
        /// <param name="presencaVm">Id of course</param>
        /// <returns></returns>
        /// <response code="200">Curso found</response>
        /// <response code="404">Curso not foundd</response>
        [HttpPost]
        [Route("update/{id}")]
        public HttpResponseMessage UpdateCurso([FromBody] PresencaViewModel presencaVm, string id)
        {

            try
            {

                Domain.Entities.ListaPresenca presenca = _presenca.GetEntityById(int.Parse(id));

                presenca.HoraEntrada = presencaVm.HoraEntrada;
                presenca.Mes = presencaVm.Mes;
                presenca.Ano = presencaVm.Ano;

                _presenca.Update(presenca);

                return Request.CreateResponse(HttpStatusCode.OK, "The object was updated");

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }


    }
}
