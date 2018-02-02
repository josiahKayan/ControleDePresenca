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
    [RoutePrefix("turma")]
    public class TurmaController : ApiController
    {

        ITurmaRepository _turma;
        IAlunoRepository _aluno;

        public TurmaController()
        {
            _turma = new TurmaRepository();
            _aluno = new AlunoRepository();
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


                var listTurma = _turma.GetAll();
               
                return Request.CreateResponse(HttpStatusCode.OK, listTurma);

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
            Log log;

            try
            {
                Turma turma = new Turma();

                //turma.Curso = turmaVm.Curso;

                CultureInfo cp = new CultureInfo("PT-br");

                turma.DataInicio = DateTime.Parse(turmaVm.DataInicio, cp);

                turma.DataTermino = DateTime.Parse(turmaVm.DataTermino, cp);

                turma.HoraFinal = ConvertFormat(turmaVm.HoraFinal);
                turma.HoraInicial = ConvertFormat(turmaVm.HoraInicial);
                turma.NomeTurma = turmaVm.NomeTurma;
                turma.PresencaLista = turmaVm.PresencaLista;

                
                turma.ProfessorId = turmaVm.Professor.ProfessorId;
                turma.CursoId = turmaVm.Curso.CursoId;

                // Compose a string that consists of three lines.
                string lines = "Escrevendo no arquivo txt o que foi recebido";

                lines = lines + "\n" + turma.DataInicio;

                lines = lines + "\n" + turma.DataTermino;

                lines = lines + "\n" + turma.HoraFinal;

                lines = lines + "\n" + turma.HoraInicial;

                lines = lines + "\n" + turma.NomeTurma;

                lines = lines + "\n" + turma.ProfessorId;

                lines = lines + "\n" + turma.CursoId;

                // Write the string to a file.
                System.IO.StreamWriter file = new System.IO.StreamWriter("C:\\Temp\\Log_AddTurma.txt");
                file.WriteLine(lines);

                file.Close();


                _turma.UpdateTurmaProfessorCurso(turma, turmaVm.Curso, turmaVm.Professor);


                log = new Log();
                log.Message = "The object was updated";
                log.Status = 1;
                log.Type = "success";
               

                return Request.CreateResponse(HttpStatusCode.OK, log);

            }
            catch (Exception e)
            {

                log = new Log();
                log.Message = e.Message + "    " + e.InnerException.InnerException.Message ;
                log.Status = 0;
                log.Type = "error";

                // Compose a string that consists of three lines.
                string lines = "Escrevendo no arquivo txt o que foi recebido";

                lines = lines + "\n" + log.Message;



                // Write the string to a file.
                System.IO.StreamWriter file = new System.IO.StreamWriter("C:\\Temp\\Log_AddTurma.txt");
                file.WriteLine(lines);

                file.Close();

                return Request.CreateResponse(HttpStatusCode.OK, log);
            }

        }

        public DateTime ConvertFormat(string date)
        {
            DateTime dt;

            if (date.Contains(":"))
            {
                dt = DateTime.ParseExact(date, "H:mm", null, System.Globalization.DateTimeStyles.None);

                return dt;
            }
            else
            {

                date = date + ":00";

                dt = DateTime.ParseExact(date, "H:mm", null, System.Globalization.DateTimeStyles.None);
                return dt;
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
        /// Get turma by cursoId
        /// </summary>
        /// <remarks>
        /// Get a Turma by id
        /// </remarks>
        /// <param name="id">Id do curso</param>
        /// <returns></returns>
        /// <response code="200">Turma found</response>
        /// <response code="404">Turma not founded</response>
        [HttpGet]
        [Route("cursos/{id}")]
        public HttpResponseMessage GetTurmaPeloCursoId(int id)
        {

            try
            {

                List<TurmaViewModel> listaTurmas = new List<TurmaViewModel>();

                var turma = _turma.GetTurmasPeloCursoId(id);

                foreach (var item in turma)
                {
                    TurmaViewModel t = new TurmaViewModel();
                    t.AlunoLista = item.AlunoLista;
                    t.Curso = item.Curso;
                    t.DataInicio = item.DataInicio.ToString("dd/MM/yyyy");
                    t.DataTermino = item.DataTermino.ToString("dd/MM/yyyy");
                    t.HoraFinal = item.HoraFinal.ToString("hh:mm tt");
                    t.HoraInicial = item.HoraInicial.ToString("hh:mm tt");
                    t.NomeTurma = item.NomeTurma;
                    t.PresencaLista = item.PresencaLista;
                    t.Professor = item.Professor;
                    t.TurmaId = item.TurmaId;
                    t.ProfessorId = item.ProfessorId;
                    listaTurmas.Add(t);
                }

                //curso.PeriodoInicial = DateTime.ParseExact(cursoVm.PeriodoInicial.Replace("/", ""), "ddMMyyyy", CultureInfo.InvariantCulture);
                //curso.PeriodoFinal = DateTime.ParseExact(cursoVm.PeriodoFinal.Replace("/", ""), "ddMMyyyy", CultureInfo.InvariantCulture);



                return Request.CreateResponse(HttpStatusCode.OK, listaTurmas);

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }


        /// <summary>
        /// Get turma by cursoId
        /// </summary>
        /// <remarks>
        /// Get a Turma by id
        /// </remarks>
        /// <param name="id">Id do curso</param>
        /// <returns></returns>
        /// <response code="200">Turma found</response>
        /// <response code="404">Turma not founded</response>
        [HttpGet]
        [Route("alunos/{id}")]
        public HttpResponseMessage GetAlunosPeloTurmaId(int id)
        {

            try
            {

                List<TurmaViewModel> listaTurmas = new List<TurmaViewModel>();

                var listaAluno = _turma.GetAlunoByTurmaId(id);

                return Request.CreateResponse(HttpStatusCode.OK, listaAluno.ToList());

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }



        /// <summary>
        /// Get turma by cursoId
        /// </summary>
        /// <remarks>
        /// Get a Turma by id
        /// </remarks>
        /// <param name="id">Id do curso</param>
        /// <returns></returns>
        /// <response code="200">Turma found</response>
        /// <response code="404">Turma not founded</response>
        [HttpGet]
        [Route("professores/ordenados/{id}")]
        public HttpResponseMessage GetTurmaPeloId(int id)
        {

            try
            {

                var listTurma = _turma.GetAll();


                listTurma.OrderBy(x => x.ProfessorId == id);

                //curso.PeriodoInicial = DateTime.ParseExact(cursoVm.PeriodoInicial.Replace("/", ""), "ddMMyyyy", CultureInfo.InvariantCulture);
                //curso.PeriodoFinal = DateTime.ParseExact(cursoVm.PeriodoFinal.Replace("/", ""), "ddMMyyyy", CultureInfo.InvariantCulture);



                return Request.CreateResponse(HttpStatusCode.OK, listTurma);

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
        [HttpPost]
        [Route("delete/{id}")]
        public HttpResponseMessage DeleteTurma(int id)
        {
            Log log;

            try
            {

                var turma = _turma.GetEntityById(id);

                _turma.Remove(turma);

                log = new Log();
                log.Message = "Removido com sucesso";
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
        [HttpPut]
        [Route("update/{id}")]
        public HttpResponseMessage UpdateCurso([FromBody] TurmaViewModel turmaVm, string id)
        {

            try
            {

                Turma turma = _turma.GetEntityById(int.Parse(id));

                //turma.DataInicio = turmaVm.DataInicio;
                //turma.DataTermino = turmaVm.DataTermino;
                turma.Curso = turmaVm.Curso;
                turma.Professor = turmaVm.Professor;

                _turma.Update(turma);

                return Request.CreateResponse(HttpStatusCode.OK, "The course was updated");

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }

        [HttpPost]
        [Route("{idTurma}/{idAluno}")]
        public HttpResponseMessage AddAlunoATurma(string idTurma, string idAluno)
        {
            try
            {


                // Compose a string that consists of three lines.
                string lines = "Escrevendo no arquivo txt o que foi recebido";

                lines = lines + "\n" + idTurma;

                lines = lines + "\n" + idAluno;

               
                // Write the string to a file.
                System.IO.StreamWriter file = new System.IO.StreamWriter("C:\\Temp\\Log AddAlunoaTurma.txt");
                file.WriteLine(lines);

                file.Close();


                using(var repositorio = new TurmaRepository())
                {
                    Turma turma = repositorio.GetEntityById(int.Parse(idTurma));

                    Aluno aluno = repositorio.GetAlunoByIdIncludesTurma(int.Parse(idAluno));

                    turma.AlunoLista.Add(aluno);

                    repositorio.AddAlunoAturma(turma);
                }

              

                Log log = new Log();
                log.Message = "Aluno foi adicionado na turma";
                log.Status = 1;

                return Request.CreateResponse(HttpStatusCode.OK, log);
            }
            catch(Exception e)
            {
                Log log = new Log();
                log.Message = "Erro";
                log.Status = 1;
                return Request.CreateResponse(HttpStatusCode.OK, log);

            }


        }


        /// <summary>
        /// Get turma by cursoId
        /// </summary>
        /// <remarks>
        /// Get a Turma by id
        /// </remarks>
        /// <param name="id">Id do curso</param>
        /// <returns></returns>
        /// <response code="200">Turma found</response>
        /// <response code="404">Turma not founded</response>
        [HttpGet]
        [Route("turmas/{professorid}")]
        public HttpResponseMessage GetTurmaPorProfessorId(int professorid)
        {

            try
            {

                var listTurma = _turma.GetTurmaPorProfessorId(professorid);


                //listTurma.OrderBy(x => x.ProfessorId == id);

                //curso.PeriodoInicial = DateTime.ParseExact(cursoVm.PeriodoInicial.Replace("/", ""), "ddMMyyyy", CultureInfo.InvariantCulture);
                //curso.PeriodoFinal = DateTime.ParseExact(cursoVm.PeriodoFinal.Replace("/", ""), "ddMMyyyy", CultureInfo.InvariantCulture);



                return Request.CreateResponse(HttpStatusCode.OK, listTurma);

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }
    }
}
