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
    [RoutePrefix("lista-presenca")]
    public class ListaPresencaController : ApiController
    {

        ListaPresencaRepository _listaPresenca;
 
        ITurmaRepository _turma;
        AlunoRepository _aluno;



        public ListaPresencaController()
        {
            _listaPresenca = new ListaPresencaRepository();
            _turma = new TurmaRepository();
            _aluno = new AlunoRepository();

        }


        [HttpGet]
        [Route("GetListasPresencaByIdTurma/{id}")]
        public HttpResponseMessage GetListasPresencaByIdTurma( int id)
        {

            try
            {

                var listPresenca = _listaPresenca.GetListaPresencaByIdTurma(id);

                return Request.CreateResponse(HttpStatusCode.OK, listPresenca);

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }



        [HttpPost]
        [Route("insertListaPresenca/{id}")]
        public HttpResponseMessage insertListaPresenca(int id)
        {

            NotificationFireBase notificationFireBase = null;
            NotificationParams notification = null;

            try
            {

                ListaPresenca l = new ListaPresenca();

                l.Ano = DateTime.Now.Year;
                l.Ativo = true;
                l.Dia = DateTime.Now.Day;
                l.Mes = DateTime.Now.Month;
                l.HoraEntrada = DateTime.Now;
                l.TurmaId = id;

                _listaPresenca.Add(l);

                //Instancia notificação
                notificationFireBase = new NotificationFireBase();
                notification = new NotificationParams();

                //Inicia o objeto
                notification.Title = "Nova lista de chamada";
                notification.Body = "Atenção, o seu professor começou uma nova lista de chamada.";

                var usuarios = _aluno.GetAlunosComUsuarioPorIdTurma(id);

                var listaIdUsuarios = usuarios.Select(x => x.NotificacaoId).ToList();

                listaIdUsuarios = listaIdUsuarios.Where(x => x != null).ToList();

                //var listaIdUsuarios = usuarios.Select(x => x.Usuario.NotificacaoId).ToList();
                //var usuarios = _aluno.GetAlunosComUsuarioPorIdTurma(1);
                //var listaIdUsuarios = usuarios.Select(x => x.NotificacaoId).ToList();

                if (listaIdUsuarios.Count > 0) {
                    notificationFireBase.SendMessage(notification, listaIdUsuarios);
                }

                return Request.CreateResponse(HttpStatusCode.OK, "Ok");

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }


        }


    }
}
