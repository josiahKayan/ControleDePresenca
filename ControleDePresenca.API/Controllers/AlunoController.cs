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
    [RoutePrefix("aluno")]
    public class AlunoController : ApiController
    {

        IAlunoRepository _aluno;

        public AlunoController()
        {
            _aluno = new AlunoRepository();
        }

        /// <summary>
        ///  Get all alunos
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [Route("alunos")]
        public HttpResponseMessage GetAlunos()
        {

            try
            {

                var listAlunos = _aluno.GetAll();

                return Request.CreateResponse(HttpStatusCode.OK, listAlunos);

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }

        [HttpPost]
        [Route("addaluno")]
        public HttpResponseMessage NewAluno([FromBody] AlunoViewModel alunoVm)
        {

            try
            {

                Aluno aluno = new Aluno();

                aluno.Nome = alunoVm.Nome;
                aluno.NomeCompleto = alunoVm.NomeCompleto;
                aluno.Idade = alunoVm.Idade;
                aluno.DataNascimento = alunoVm.DataNascimento;
                aluno.Tag = alunoVm.Tag;
                aluno.Turma = alunoVm.Turma;
                aluno.Usuario = alunoVm.Usuario;
                
                _aluno.Add(aluno);

                return Request.CreateResponse(HttpStatusCode.OK, "Ok");

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }

        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetAluno(string id)
        {

            try
            {

                var aluno = _aluno.GetEntityById(int.Parse(id));

                return Request.CreateResponse(HttpStatusCode.OK, aluno);

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }

        [HttpDelete]
        [Route("delete/{id}")]
        public HttpResponseMessage DeleteAluno(string id)
        {

            try
            {

                var aluno = _aluno.GetEntityById(int.Parse(id));

                _aluno.Remove(aluno);

                return Request.CreateResponse(HttpStatusCode.OK, "The object was removed");

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }

        [HttpPut]
        [Route("update/{id}")]
        public HttpResponseMessage UpdateProfessor([FromBody] AlunoViewModel alunoVm, string id)
        {

            try
            {

                Aluno aluno = _aluno.GetEntityById(int.Parse(id));

                aluno.Nome = alunoVm.Nome;
                aluno.NomeCompleto = alunoVm.NomeCompleto;
                aluno.Idade = alunoVm.Idade;
                aluno.DataNascimento = alunoVm.DataNascimento;
                aluno.Tag = alunoVm.Tag;
                aluno.Turma = alunoVm.Turma;
                aluno.Usuario = alunoVm.Usuario;


                _aluno.Update(aluno);

                return Request.CreateResponse(HttpStatusCode.OK, "The object was updated");

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }

    }
}
