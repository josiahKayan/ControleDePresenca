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
        TagRepository _tag;


        public PresencaController()
        {
            _presenca = new PresencaRepository();
            _turma = new TurmaRepository();
            _tag = new TagRepository();

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
        [Route("GetResumoListaPresencaByIdPresencalista/{id}/{aluno}")]
        public HttpResponseMessage GetResumoListaPresencaByIdPresencalista(int id, int aluno)
        {

            try
            {

                if ( aluno > 0)
                {
                    var _aluno = new AlunoRepository();

                    aluno = _aluno.GetAlunoByUsuarioId(aluno).AlunoId;

                }

                var listPresenca = _presenca.GetResumoListaPresencaByIdPresencalista(id,aluno);

                return Request.CreateResponse(HttpStatusCode.OK, listPresenca);

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }


        [HttpGet]
        [Route("InsertPresenca/{idPresenca}/{idTurma}/{idUser}")]
        public HttpResponseMessage InsertPresenca(int idPresenca, int idTurma, int idUser)
        {
            

            try
            {

                var _aluno = new AlunoRepository();

                var aluno = _aluno.GetAlunoByUsuarioId(idUser);

                _presenca.InsertPresencaQrCode( idPresenca,  idTurma, aluno.AlunoId);

                return Request.CreateResponse(HttpStatusCode.OK, "OK");

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }


        [HttpGet]
        [Route("InsertPresencaRFID/{tagCode}")]
        public HttpResponseMessage InsertPresenca(string tagCode)
        {
        
            try
            {

                tagCode = tagCode.Replace(" ", "");


                var _tag = new TagRepository();

                var _aluno = new AlunoRepository();

                var _listaPresenca = new ListaPresencaRepository();

                var aluno = _aluno.GetAlunoByTag(tagCode);

                var tempo = DateTime.Now;

                var listaPresenca = _listaPresenca.BuscaListaPrensecaPorData(tempo);

                if (listaPresenca !=null) {
                    _presenca.InsertPresencaRFID(listaPresenca.ListaPresencaId, listaPresenca.TurmaId, aluno.AlunoId);

                    return Request.CreateResponse(HttpStatusCode.OK, "OK");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Error!!, verifique o horário");
                }

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }


        [HttpGet]
        [Route("getGeneralList/{idTurma}")]
        public HttpResponseMessage getGeneralList( int idTurma)
        {

            try
            {

                int totalDias = _presenca.GetTotalPresenca(idTurma);

                var alunosTurma = _turma.GetAlunoByTurmaId(idTurma);

                Geral g = new Geral();

                g.TotalDias = totalDias;

                g.FrequenciaAlunos = _presenca.GetFrequenciaAlunos(  alunosTurma , idTurma, totalDias,0);

                g.Datas = _presenca.GetListaDatas(alunosTurma, idTurma, totalDias);

                //var _aluno = new AlunoRepository();

                //var aluno = _aluno.GetAlunoByUsuarioId(idUser);

                //_presenca.InsertPresenca(idPresenca, idTurma, aluno.AlunoId);

                //var lista = "{\"totalDias\":4,\"frequencia\":[\"aluno\":{\"strFoto\":\"www.foto.com.br\",\"strNome\":\"Felipe\",\"data\":[\"01-01-2019\",\"02-01-2019\",\"03-01-2019\",\"04-01-2019\"],\"presencas\":3,\"faltas\":0},\"aluno\":{\"strFoto\":\"www.foto.com.br\",            \"strNome\":\"Felipe\",            \"data\":[\"01-01-2019\",\"02-01-2019\",\"03-01-2019\",\"04-01-2019\"],	    \"presencas\":2,	    \"faltas\":1        }        ,        \"aluno\":{              \"strFoto\":\"www.foto.com.br\",\"strNome\":\"Felipe\",\"data\":[\"01-01-2019\",\"02-01-2019\",\"03-01-2019\",\"04-01-2019\"],\"presencas\":0,\"faltas\":3}]}";

                return Request.CreateResponse(HttpStatusCode.OK, g);

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }

        [HttpGet]
        [Route("getGeneralList/{idTurma}/{idUser}")]
        public HttpResponseMessage getGeneralListIndividual(int idTurma, int iduser)
        {

            try
            {

                int totalDias = _presenca.GetTotalPresenca(idTurma);

                var alunosTurma = _turma.GetAlunoByTurmaId(idTurma).Where( a => a.UsuarioId == iduser );

                Geral g = new Geral();

                g.TotalDias = totalDias;

                g.FrequenciaAlunos = _presenca.GetFrequenciaAlunos(alunosTurma, idTurma, totalDias,iduser).OrderBy( o => o.NomeCompleto).ToList();

                g.Datas = _presenca.GetListaDatas(alunosTurma, idTurma, totalDias);

                //var _aluno = new AlunoRepository();

                //var aluno = _aluno.GetAlunoByUsuarioId(idUser);

                //_presenca.InsertPresenca(idPresenca, idTurma, aluno.AlunoId);

                //var lista = "{\"totalDias\":4,\"frequencia\":[\"aluno\":{\"strFoto\":\"www.foto.com.br\",\"strNome\":\"Felipe\",\"data\":[\"01-01-2019\",\"02-01-2019\",\"03-01-2019\",\"04-01-2019\"],\"presencas\":3,\"faltas\":0},\"aluno\":{\"strFoto\":\"www.foto.com.br\",            \"strNome\":\"Felipe\",            \"data\":[\"01-01-2019\",\"02-01-2019\",\"03-01-2019\",\"04-01-2019\"],	    \"presencas\":2,	    \"faltas\":1        }        ,        \"aluno\":{              \"strFoto\":\"www.foto.com.br\",\"strNome\":\"Felipe\",\"data\":[\"01-01-2019\",\"02-01-2019\",\"03-01-2019\",\"04-01-2019\"],\"presencas\":0,\"faltas\":3}]}";

                return Request.CreateResponse(HttpStatusCode.OK, g);

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
                List<Aluno> listaAlunoTurma = alunosTurma.Select(x => x.AlunoLista.ToList()).FirstOrDefault().ToList();

                //Lista dos faltosos
                listaAlunoTurma.AddRange(  listPresencaAluno  );

                var listFaltosos = listaAlunoTurma.Distinct().Where(x => !listPresencaAluno.Any(e => x.AlunoId == e.AlunoId)).ToList();


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
