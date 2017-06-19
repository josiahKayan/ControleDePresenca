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
                //Presenca p = new Presenca();
                //p.Ano = 2017;
                //p.Mes = 6;
                //p.Dia = 1;
                //p.PresencaId = 1;

                //Presenca p1 = new Presenca();
                //p.Ano = 2017;
                //p.Mes = 7;
                //p.Dia = 2;
                //p.PresencaId = 2;

                //List<Presenca> listPresenca = new List<Presenca>();
                //listPresenca.Add(p);
                //listPresenca.Add(p1);

                Aluno aluno1 = new Aluno();
                aluno1.AlunoId = 1;
                aluno1.DataNascimento = DateTime.Now;
                aluno1.Idade = 10;
                aluno1.Nome = "Daniel";
                aluno1.NomeCompleto = "Daniel da cova dos leões";
                aluno1.Tag = new Tag { Code = "123", Status = 1, TagId = 1 };

                Aluno aluno2 = new Aluno();
                aluno2.AlunoId = 2;
                aluno2.DataNascimento = DateTime.Now;
                aluno2.Idade = 8;
                aluno2.Nome = "Josias";
                aluno2.NomeCompleto = "Rei Josias";
                aluno2.Tag = new Tag { Code = "123", Status = 1, TagId = 1 };


                var listAluno = new List<Aluno>();
                listAluno.Add(aluno1);
                listAluno.Add(aluno2);

                Curso curso1 = new Curso();
                curso1.Ativo = true;
                curso1.CursoId = 1;
                curso1.Descricao = "Estudo da Origem da Vida";
                curso1.Nome = "Origens";


                Professor professor1 = new Professor();
                professor1.Nome = "Ismael";
                professor1.NomeCompleto = "Ismael Ismael";
                professor1.ProfessorId = 1;
                professor1.DataNascimento = DateTime.Now;
                professor1.Idade = 25;

                List<Turma> listTurmas = new List<Turma>();

                Turma turma1 = new Turma();
                turma1.TurmaId = 1;
                turma1.Professor = professor1;
                turma1.DataInicio = DateTime.Now;
                turma1.DataTermino = DateTime.Now;
                turma1.Curso = curso1;
                turma1.AlunoLista = listAluno;
                //turma1.PresencaLista = listPresenca;

                listTurmas.Add(turma1);


                //var listTurmas = _turma.GetAll();

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

            try
            {

                Turma turma = new Turma();

                turma.DataInicio = turmaVm.DataInicio;
                turma.DataTermino = turmaVm.DataTermino;
                turma.Curso = turmaVm.Curso;
                turma.Professor = turmaVm.Professor;
                
                _turma.Add(turma);

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
        /// <param name="id">Id da Turma</param>
        /// <returns></returns>
        /// <response code="200">Turma found</response>
        /// <response code="404">Turma not founded</response>
        [HttpDelete]
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
        [HttpPut]
        [Route("update/{id}")]
        public HttpResponseMessage UpdateCurso([FromBody] TurmaViewModel turmaVm, string id)
        {

            try
            {

                Turma turma = _turma.GetEntityById(int.Parse(id));

                turma.DataInicio = turmaVm.DataInicio;
                turma.DataTermino = turmaVm.DataTermino;
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


    }
}
