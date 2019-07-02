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

        AlunoRepository _aluno;
        TagRepository _tag;

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

                var listAlunos = _aluno.GetAll().OrderBy( o => o.NomeCompleto);

                return Request.CreateResponse(HttpStatusCode.OK, listAlunos);

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }


        [HttpGet]
        [Route("alunos/peloid/{id}")]
        public HttpResponseMessage GetAlunosPeloId( int id)
        {

            try
            {

                var listNaoPertencentes = _aluno.GetAlunosNaoPertencentesNessaTurma(id);
                var listPertencentes = _aluno.GetAlunosNessaTurma(id);

                List<AlunoViewModel> listaAlunos = new List<AlunoViewModel>();

                foreach (var item in listNaoPertencentes)
                {
                    AlunoViewModel vm = new AlunoViewModel();

                    vm.AlunoId = item.AlunoId;
                    vm.Nome = item.Nome;
                    vm.TurmaId = id;
                    vm.NomeCompleto = item.NomeCompleto;
                    vm.Selected = false;

                    listaAlunos.Add(vm);
                }

                foreach (var item in listPertencentes)
                {
                    AlunoViewModel vm = new AlunoViewModel();

                    vm.AlunoId = item.AlunoId;
                    vm.TurmaId = id;
                    vm.Nome = item.Nome;
                    vm.NomeCompleto = item.NomeCompleto;
                    vm.Selected = true;

                    listaAlunos.Add(vm);
                }

              

                return Request.CreateResponse(HttpStatusCode.OK, listaAlunos);

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }

        [HttpGet]
        [Route("ping/{id}")]
        public HttpResponseMessage GetAlunosPing(int id)
        {

            try
            {

                

                return Request.CreateResponse(HttpStatusCode.OK, "ok");

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
            Tag tag = null;
            _tag = new TagRepository();

            try
            {

                Aluno aluno = new Aluno();

                aluno.Nome = alunoVm.Nome;
                aluno.NomeCompleto = alunoVm.NomeCompleto;
                aluno.Idade = int.Parse(alunoVm.Idade);
                aluno.DataNascimento = alunoVm.DataNascimento;
                aluno.Tag = alunoVm.Tag;
                
                //aluno.Turma.aalunoVm.Turma;
                aluno.Usuario = alunoVm.Usuario;
                //aluno.Imagem = alunoVm.Imagem;

                tag = _tag.SearchTagByCode(alunoVm.Tag.Code);

                aluno.Tag = tag;

                aluno.Tag.Status = 1;

                _tag.EditarStatusTag(aluno.Tag);
                

                _aluno.addAluno(aluno);

                return Request.CreateResponse(HttpStatusCode.OK, "ok");

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.InnerException.Message);
            }

        }

        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetAluno(string id)
        {

            try
            {

                var aluno = _aluno.GetAlunoByIdIncludes(int.Parse(id));

                return Request.CreateResponse(HttpStatusCode.OK, aluno);

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }

        [HttpGet]
        [Route("usuario/{id}")]
        public HttpResponseMessage GetAlunoByUsuario(string id)
        {

            try
            {

                var aluno = _aluno.GetAlunoByUsuarioId(int.Parse(id));

                return Request.CreateResponse(HttpStatusCode.OK, aluno);

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

                var aluno = _aluno.GetAlunoByIdIncludes(int.Parse(id));


                _tag = new TagRepository();

                var tag = _tag.GetEntityById(aluno.TagId);

                tag.Status = 0;

                _tag.Update(tag);

                _aluno.Remove(aluno);

                return Request.CreateResponse(HttpStatusCode.OK, "The object was removed");

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }

        [HttpPost]
        [Route("update/{id}")]
        public HttpResponseMessage UpdateAluno([FromBody] AlunoViewModel alunoVm, string id)
        {

            try
            {

                Aluno aluno = new Aluno();

                aluno.Nome = alunoVm.Nome;
                aluno.NomeCompleto = alunoVm.NomeCompleto;
                aluno.Idade = int.Parse( alunoVm.Idade);
                aluno.DataNascimento = alunoVm.DataNascimento;
                aluno.Tag = alunoVm.Tag;
                //aluno.Turma = alunoVm.Turma;
                aluno.Usuario = alunoVm.Usuario;
                aluno.Imagem = alunoVm.Imagem;

                aluno.AlunoId = int.Parse(id);


                //Atualiza a Tag antiga para DESABILITADA
                var alunoTAG = new AlunoRepository();
                var a = alunoTAG.GetAlunoByIdIncludes(int.Parse(id));
                var _t = new TagRepository();
                var t = _t.GetEntityById(a.TagId);
                t.Status = 0;
                _t.Update(t);
                //////



                Tag tag = null;
                _tag = new TagRepository();

                tag = _tag.SearchTagByCode(alunoVm.Tag.Code);

                aluno.Tag = tag;

                aluno.Tag.Status = 1;

                _tag.EditarStatusTag(aluno.Tag);

                aluno.Tag = null;

                _aluno.UpdateAluno(aluno);

                return Request.CreateResponse(HttpStatusCode.OK, "The object was updated");

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }

        [HttpPost]
        [Route("update-aluno-turma/{id}")]
        public HttpResponseMessage UpdateAlunoTurma([FromBody] List<AlunoViewModel> alunos, int id)
        {

            try
            {


                int turmaId = id;

                List<int> listaSelecionados = alunos.Select(a => a.AlunoId).ToList();

                _aluno.AddOuRemoveAlunoNaTurma(id, listaSelecionados);

                return Request.CreateResponse(HttpStatusCode.OK, "The object was updated");

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }

        //[HttpGet]
        //[Route("update-aluno-turma/{id}")]
        //public HttpResponseMessage UpdateAlunoTurma( int id)
        //{

        //    try
        //    {


        //        int turmaId = 3;

        //        List<int> listaSelecionados = new List<int>();

        //        listaSelecionados.Add(3);
        //        listaSelecionados.Add(4);


        //        _aluno.AddOuRemoveAlunoNaTurma(id, listaSelecionados);

        //        return Request.CreateResponse(HttpStatusCode.OK, "The object was updated");

        //    }
        //    catch (Exception e)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.OK, e.Message);
        //    }

        //}


    }
}
