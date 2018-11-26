using ControleDePresenca.Domain.Entities;
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
    [RoutePrefix("lista-presenca")]
    public class ListaPresencaController : ApiController
    {

        ListaPresencaRepository _listaPresenca;

        public ListaPresencaController()
        {
            _listaPresenca = new ListaPresencaRepository();
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

                return Request.CreateResponse(HttpStatusCode.OK, "Ok");

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }


        }


    }
}
