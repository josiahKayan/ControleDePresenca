using AdvertenciasApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AdvertenciasApp.Controllers
{
    [EnableCors(origins:"*",headers:"*",methods:"*")]
    [RoutePrefix("advertencias")]
    public class AdvertenciasController : ApiController
    {

        [HttpGet]
        [Route("")]
        public HttpResponseMessage ListarAdvertencia()
        {
            try
            {

                Aluno aluno = new Aluno();
                aluno.Matricula = 111;
                aluno.Nome = "Josias";

                Advertencia adv = new Advertencia();
                adv.AdvertenciaId = 1112;
                adv.Descricao = "Aluno jogou tinta na cadeira";
                aluno.Advertencia = adv;


                Aluno aluno2 = new Aluno();
                aluno2.Matricula = 222;
                aluno2.Nome = "Sammya";

                Advertencia adv2 = new Advertencia();
                adv2.AdvertenciaId = 2222;
                adv2.Descricao = "Aluno dormiu na aula";
                aluno2.Advertencia = adv2;

                Aluno aluno3 = new Aluno();
                aluno3.Matricula = 333;
                aluno3.Nome = "Josias";

                Advertencia adv3 = new Advertencia();
                adv3.AdvertenciaId = 3333;
                adv3.Descricao = "Aluno colocou tachinha na cadeira";
                aluno3.Advertencia = adv3;

                List<Aluno> listaAlunos = new List<Aluno>();

                listaAlunos.Add(aluno);
                listaAlunos.Add(aluno2);
                listaAlunos.Add(aluno3);
    
                return Request.CreateResponse(HttpStatusCode.OK, listaAlunos);
            }
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);

            }

        }

        [HttpPost]
        [Route("concordar")]
        public HttpResponseMessage Concordar(Termo termo)
        {
            return Request.CreateResponse(HttpStatusCode.OK, termo);
        }
    }
}
