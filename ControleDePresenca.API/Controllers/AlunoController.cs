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
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ControleDePresenca.API.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("aluno")]
    public class AlunoController : ApiController
    {

        IAlunoRepository _aluno;
        ITagRepository _tag;
        Log log;

        public AlunoController()
        {
            _aluno = new AlunoRepository();
            _tag = new TagRepository();
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

                AlunoViewModel alunoVm = new AlunoViewModel();

                var listAlunos = _aluno.GetAllAlunos();

                var list = alunoVm.ParserAluno(listAlunos.ToList());

                return Request.CreateResponse(HttpStatusCode.OK, list);

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

                var listaAlunos = _aluno.GetAllAlunos().ToList();
                var listaTags = _tag.ListaTagNaoArmazenada();
                listaTags = listaTags.Where(a => listaAlunos.Any(x => x.TagId == a.TagId)).ToList();

                foreach (var item in listaTags)
                {
                    item.Status = 1;
                    _tag.EditarStatusTag(item);
                }

             

                Aluno aluno = new Aluno();

                if (alunoVm.AlunoId != 0)
                {
                    aluno = _aluno.GetEntityById(alunoVm.AlunoId);

                    if (aluno != null)
                    {

                        aluno.Nome = alunoVm.Nome;
                        aluno.NomeCompleto = alunoVm.NomeCompleto;
                        aluno.Idade = int.Parse(alunoVm.Idade);
                        aluno.DataNascimento = DateTime.ParseExact(alunoVm.DataNascimento.Replace("/", ""), "ddMMyyyy", CultureInfo.InvariantCulture);
                        aluno.TagId = alunoVm.TagId;
                        aluno.Turma = alunoVm.Turmas;
                        aluno.Usuario = new Usuario();
                        aluno.Usuario = new Usuario();
                        aluno.Usuario.Email = alunoVm.Usuario.Email;
                        aluno.Usuario.Senha = alunoVm.Usuario.Senha.ToString();
                        aluno.Usuario.Perfil = 0;
                        _aluno.Update(aluno);
                        log = new Log();
                        log.Message = "The object was updated";
                        log.Status = 1;
                        log.Type = "success";
                        return Request.CreateResponse(HttpStatusCode.OK, log);
                    }
                }


                aluno.Nome = alunoVm.Nome;
                aluno.NomeCompleto = alunoVm.NomeCompleto;
                aluno.Idade = int.Parse(alunoVm.Idade);
                aluno.DataNascimento = DateTime.ParseExact(alunoVm.DataNascimento.Replace("/", ""), "ddMMyyyy", CultureInfo.InvariantCulture);
                aluno.TagId = alunoVm.TagId;
                aluno.Turma = alunoVm.Turmas;

                aluno.Usuario = new Usuario();
                aluno.Usuario.Email = alunoVm.Usuario.Email;
                aluno.Usuario.Senha = alunoVm.Usuario.Senha.ToString();

                aluno.Usuario.Perfil = 0;

                //aluno.UsuarioId = alunoVm.UsuarioId;

                _aluno.Add(aluno);

              

                log = new Log();
                log.Message = "The object was added";
                log.Status = 1;
                log.Type = "success";
                return Request.CreateResponse(HttpStatusCode.OK, log);

            }
            catch (Exception e)
            {

                log = new Log();
                log.Message = e.InnerException.Message;
                log.Status = 0;
                log.Type = "error";
                return Request.CreateResponse(HttpStatusCode.OK, log);
            }

        }



        public void EscreveDadosAluno(AlunoViewModel aluno)
        {
            // Compose a string that consists of three lines.
            string lines = "Escrevendo no arquivo txt o que foi recebido";

            lines = lines + "\n" + aluno.Usuario.Email;

            lines = lines + "\n" + aluno.Usuario.Perfil;

            lines = lines + "\n" + aluno.Usuario.Senha;

            lines = lines + "\n" + aluno.DataNascimento;

            lines = lines + "\n" + aluno.Idade;

            lines = lines + "\n" + aluno.Nome;

            lines = lines + "\n" + aluno.NomeCompleto;

            lines = lines + "\n" + aluno.Tag;

            lines = lines + "\n" + aluno.TagId;

            lines = lines + "\n" + aluno.Turmas;

            lines = lines + "\n" + aluno.Usuario;


            // Write the string to a file.
            System.IO.StreamWriter file = new System.IO.StreamWriter("C:\\Temp\\test.txt");
            file.WriteLine(lines);

            file.Close();
        }

        public void EscreveLog(String log)
        {
            // Compose a string that consists of three lines.
            string lines = "Escrevendo no arquivo txt o que foi recebido";

            lines = lines + "\n" + log;


            // Write the string to a file.
            System.IO.StreamWriter file = new System.IO.StreamWriter("C:\\Temp\\log.txt");
            file.WriteLine(lines);

            file.Close();
        }

        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetAluno(string id)
        {
            try
            {
                var aluno = _aluno.GetAlunoByIdIncludes(int.Parse(id));

                AlunoViewModel alunoVm = new AlunoViewModel();
                alunoVm.Nome = aluno.Nome;
                alunoVm.NomeCompleto = aluno.NomeCompleto;
                alunoVm.Idade = ""+aluno.Idade;
                alunoVm.DataNascimento = aluno.DataNascimento.ToString("dd/MM/yyyy");
                alunoVm.AlunoId = aluno.AlunoId;
                alunoVm.Usuario = new Usuario();
                alunoVm.Usuario.Email = aluno.Usuario.Email;
                alunoVm.Usuario.Senha = null;
                alunoVm.Usuario.Perfil = aluno.Usuario.Perfil;
                alunoVm.Usuario.UsuarioId = aluno.Usuario.UsuarioId;
                alunoVm.UsuarioId = aluno.UsuarioId;
                alunoVm.Turmas = aluno.Turma.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, alunoVm);


            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }

        [HttpGet]
        [Route("delete/{id}")]
        public HttpResponseMessage DeleteAluno(string id)
        {

            try
            {
                AlunoRepository _aluno = new AlunoRepository();
                var aluno = _aluno.GetAlunoByIdIncludes(int.Parse(id));

                if (aluno==null)
                {
                    Exception e = new Exception("The object was not found");
                    throw e;
                }

                _aluno.RemoveComUsuario(aluno);

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
        public HttpResponseMessage UpdateProfessor([FromBody] AlunoViewModel alunoVm, string id)
        {

            try
            {

                Aluno aluno = _aluno.GetEntityById(int.Parse(id));

                aluno.Nome = alunoVm.Nome;
                aluno.NomeCompleto = alunoVm.NomeCompleto;
                //aluno.Idade = alunoVm.Idade;
                //aluno.DataNascimento = alunoVm.DataNascimento;
                aluno.Tag = alunoVm.Tag;
                //aluno.Turma = alunoVm.Turma;
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
