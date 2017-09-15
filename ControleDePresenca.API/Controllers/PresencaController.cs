using ControleDePresenca.API.ViewModels;
using ControleDePresenca.Domain.Entities;
using ControleDePresenca.Domain.Interfaces.Repositories;
using ControleDePresenca.Infra.Data.Repositories;
using Newtonsoft.Json;
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
    [RoutePrefix("presenca")]
    public class PresencaController : ApiController
    {

        IPresencaRepository _presenca;

        public PresencaController()
        {
            _presenca = new PresencaRepository();
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
        [Route("presencas")]
        public HttpResponseMessage GetPresencas()
        {

            try
            {

                var listPresenca = _presenca.GetAll();

                return Request.CreateResponse(HttpStatusCode.OK, listPresenca);

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

                Presenca presenca = new Presenca();


                // Compose a string that consists of three lines.
                string lines = "Escrevendo no arquivo txt o que foi recebido";

                lines = lines + "\n" + presencaVm.HoraEntrada;

                lines = lines + "\n" + presencaVm.Mes;

                lines = lines + "\n" + presencaVm.Ano;

                //lines = lines + "\n" + presencaVm.TurmaId;

                lines = lines + "\n" + presencaVm.Ativo;

                lines = lines + "\n" + presencaVm.Dia;


                // Write the string to a file.
                System.IO.StreamWriter file = new System.IO.StreamWriter("C:\\Temp\\test.txt");
                file.WriteLine(lines);

                file.Close();

                presenca.HoraEntrada = presencaVm.HoraEntrada;
                presenca.Mes = presencaVm.Mes;
                presenca.Ano = presencaVm.Ano;
                //presenca.TurmaId = presencaVm.TurmaId;
                //presenca.Turma = presencaVm.Turma;
                presenca.Ativo = presencaVm.Ativo;
                presenca.Dia = presencaVm.Dia;
                _presenca.Add(presenca);

                return Request.CreateResponse(HttpStatusCode.OK, presenca);

            }
            catch (Exception e)
            {
                // Compose a string that consists of three lines.
                string lines = "Escrevendo no arquivo txt o que foi recebido";

                

             


                // Write the string to a file.
                System.IO.StreamWriter file = new System.IO.StreamWriter("C:\\Temp\\logg.txt");
                file.WriteLine(lines);

                file.Close();
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
        [Route("turma/{idTurma}")]
        public HttpResponseMessage GetPresencaPorTurma(string idTurma)
        {

            PresencaViewModel presencaVm = null;

            try
            {
                int id = int.Parse(idTurma);
                var presenca = _presenca.GetListaPresenca(id);

                List<PresencaViewModel> list = new List<PresencaViewModel>();

                foreach (var item in presenca)
                {
                    presencaVm = new PresencaViewModel();
                    presencaVm.Alunos = item.Alunos;
                    presencaVm.Ano = item.Ano;
                    presencaVm.Ativo = item.Ativo;
                    presencaVm.Dia = item.Dia;
                    presencaVm.HoraEntrada = item.HoraEntrada;
                    presencaVm.Mes = item.Mes;
                    presencaVm.PresencaId = item.PresencaId;
                    list.Add(presencaVm);
                }

                return Request.CreateResponse(HttpStatusCode.OK, list);

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
        [HttpDelete]
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

        ///// <summary>
        ///// Get curso by id
        ///// </summary>
        ///// <remarks>
        ///// Get a Curso by id
        ///// </remarks>
        ///// <param name="id">Id of course</param>
        ///// <param name="presencaVm">Id of course</param>
        ///// <returns></returns>
        ///// <response code="200">Curso found</response>
        ///// <response code="404">Curso not foundd</response>
        //[HttpPut]
        //[Route("update/{id}")]
        //public HttpResponseMessage UpdateCurso([FromBody] PresencaViewModel presencaVm, string id)
        //{

        //    try
        //    {

        //        Presenca presenca = _presenca.GetEntityById(int.Parse(id));

        //        presenca.HoraEntrada = presencaVm.HoraEntrada;
        //        presenca.Mes = presencaVm.Mes;
        //        presenca.Ano = presencaVm.Ano;
        //        presenca.TurmaLista = presencaVm.TurmaLista;
        //        presenca.Ativo = presencaVm.Ativo;
        //        presenca.Dia = presencaVm.Dia;
        //        _presenca.Update(presenca);

        //        return Request.CreateResponse(HttpStatusCode.OK, "The object was updated");

        //    }
        //    catch (Exception e)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.OK, e.Message);
        //    }

        //}


    }
}
