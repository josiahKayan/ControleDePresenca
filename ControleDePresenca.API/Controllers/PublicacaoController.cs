using ControleDePresenca.API.ViewModels;
using ControleDePresenca.Domain.Entities;
using ControleDePresenca.Domain.Interfaces.Repositories;
using ControleDePresenca.Infra.Data.Repositories;
using ControleDePresenca.Notifications;
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
    [RoutePrefix("publicacao")]
    public class PublicacaoController : ApiController
    {

        PublicacaoRepository _publicacao;
        UsuarioRepository _usuario;
        ITurmaRepository _turma;
        AlunoRepository _aluno;
        ProfessorRepository _professor;



        public PublicacaoController()
        {
            _publicacao = new PublicacaoRepository();
            _usuario = new UsuarioRepository();
            _turma = new TurmaRepository();
            _aluno = new AlunoRepository();
            _professor = new ProfessorRepository();

        }


        [HttpPost]
        [Route("novapublicacao")]
        public HttpResponseMessage NovaPublicacao([FromBody] PublicacaoViewModel publicacao)
        //public HttpResponseMessage NovaPublicacao( int idturma)
        {

            try
            {

                MensagemRapida p = new MensagemRapida();

                p.Ano = DateTime.Now.Year;
                p.Dia = DateTime.Now.Day;
                p.Mes = DateTime.Now.Month;
                p.HoraPublicacao = DateTime.Now;
                p.Mensagem = publicacao.Mensagem;
                p.Titulo = publicacao.Titulo;
                p.Responsavel = publicacao.UsuarioId;

                p.IdUnico = "" + DateTime.Now.Day +"" + DateTime.Now.Month +""+ DateTime.Now.Year +""+ DateTime.Now.Hour+""+DateTime.Now.Minute + ""+DateTime.Now.Second;

                List<int> lisInt = new List<int>();

                var l = new List<string>();

                try
                {
                    l.AddRange( publicacao.TurmasId.Split(','));
                }
                catch(Exception e)
                {
                    l.Add(publicacao.TurmasId);                    
                }

                foreach (var item in l)
                {
                    lisInt.Add( int.Parse( item   ) );
                }

                foreach (var idturma in lisInt)
                {
                    _publicacao.AddPublicacao(p, idturma);

                    //Instancia notificação
                    var notificationFireBase = new NotificationFireBase();
                    var notification = new NotificationParams();

                    //Inicia o objeto
                    notification.Title = p.Titulo;
                    notification.Body = p.Mensagem;

                    var usuarios = _aluno.GetAlunosComUsuarioPorIdTurma(idturma);

                    var listaIdUsuarios = usuarios.Select(x => x.NotificacaoId).ToList();

                    listaIdUsuarios = listaIdUsuarios.Where(x => x != null).ToList();

                    if (listaIdUsuarios.Count > 0)
                    {
                        notificationFireBase.SendMessage(notification, listaIdUsuarios);
                    }

                }

                

                return Request.CreateResponse(HttpStatusCode.OK, "ok");

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.InnerException.Message);
            }

        }



        [HttpGet]
        [Route("delete/{id}")]
        public HttpResponseMessage DeletePublicacao(string id)
        {

            try
            {

                var publicacao = _publicacao.GetEntityById(int.Parse(id));

                _publicacao.Remove(publicacao);

                return Request.CreateResponse(HttpStatusCode.OK, "The object was removed");

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }


        [HttpGet]
        [Route("publicacoes/professor/{idUser}/{quantidadeRegistros}/{pagina}")]
        public HttpResponseMessage GetPublicacoes( int idUser, int quantidadeRegistros, int pagina)
        {

            try
            {

                var listPublicacoes = _publicacao.GetAllIncludesPaginacaoProfessor( idUser,  quantidadeRegistros, pagina);

                listPublicacoes = listPublicacoes.GroupBy( x => new
                {
                    Responsavel = x.Responsavel,
                    HoraPublicacao = x.HoraPublicacao,
                    Mensagem = x.Mensagem,
                    Mes = x.Mes,
                    Ano = x.Ano,
                    Dia = x.Dia,
                    Titulo = x.Titulo,
                    IdUnico = x.IdUnico
                    
                }).Select( a => new MensagemRapida()
                {
                    Responsavel = a.Key.Responsavel,
                    HoraPublicacao = a.Key.HoraPublicacao,
                    Mensagem = a.Key.Mensagem,
                    Titulo = a.Key.Titulo,
                    Mes = a.Key.Mes,
                    Ano = a.Key.Ano,
                    Dia = a.Key.Dia,
                    IdUnico = a.Key.IdUnico

                }).Distinct().ToList();

                List<MensagemRapidaViewModel> mr = new List<MensagemRapidaViewModel>();

                foreach (var item in listPublicacoes)
                {

                    var m = new MensagemRapidaViewModel();

                    m.Ano = item.Ano;
                    m.Dia = item.Dia;
                    m.HoraPublicacao = item.HoraPublicacao;
                    m.IdUnico = item.IdUnico;

                    var p = _professor.GetEntityById(item.Responsavel);

                    m.Nome = p.Nome;
                    m.NomeCompleto = p.NomeCompleto;
                    m.Imagem = p.Imagem;

                    m.Mensagem = item.Mensagem;
                    m.Mes = item.Mes;
                    m.PessoaDestino = item.PessoaDestino;
                    m.Responsavel = item.Responsavel;
                    m.Titulo = item.Titulo;

                    mr.Add(m);

                }

                return Request.CreateResponse(HttpStatusCode.OK, mr);

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }
        }

        [HttpGet]
        [Route("publicacoes/aluno/{idUser}/{quantidadeRegistros}/{pagina}")]
        public HttpResponseMessage GetPublicacoesAlunos(int idUser, int quantidadeRegistros, int pagina)
        {
            try
            {
                
                var listPublicacoes = _publicacao.GetAllIncludesPaginacaoAlunos(idUser, quantidadeRegistros , pagina);
                //var listPublicacoes = _publicacao.GetAllIncludesAlunos(idUser);

                List<MensagemRapidaViewModel> mr = new List<MensagemRapidaViewModel>();

                foreach (var item in listPublicacoes)
                {

                    var m = new MensagemRapidaViewModel();

                    m.Ano = item.Ano;
                    m.Dia = item.Dia;
                    m.HoraPublicacao = item.HoraPublicacao;
                    m.IdUnico = item.IdUnico;

                    var p = _professor.GetEntityById(item.Responsavel);

                    m.Nome = p.Nome;
                    m.NomeCompleto = p.NomeCompleto;
                    m.Imagem = p.Imagem;

                    m.Mensagem = item.Mensagem;
                    m.Mes = item.Mes;
                    m.PessoaDestino = item.PessoaDestino;
                    m.Responsavel = item.Responsavel;
                    m.Titulo = item.Titulo;

                    mr.Add(m);

                }

                return Request.CreateResponse(HttpStatusCode.OK, mr);

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }


    }
}
