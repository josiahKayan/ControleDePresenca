using ControleDePresenca.API.ViewModels;
using ControleDePresenca.Domain.Entities;
using ControleDePresenca.Domain.Interfaces.Repositories;
using ControleDePresenca.Infra.Data.Repositories;
using ControleDePresenca.Library.Log;
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
    [RoutePrefix("tag")]
    public class TagController : ApiController
    {

        ITagRepository _tag;
        Log log;

        public TagController()
        {
            _tag = new TagRepository();
        }

        /// <summary>
        /// Lista de Tags
        /// </summary>
        /// <remarks>
        /// Exibe uma lista de tag
        /// </remarks>
        /// <returns> Lista de Tags</returns>
        /// <response code="200">Lista de Tag</response>
        /// <response code="404">Tag not founded</response>
        [HttpGet]
        [Route("tags")]
        public HttpResponseMessage GetTags()
        {

            try
            {

                var listTags = _tag.GetAll();

                return Request.CreateResponse(HttpStatusCode.OK, listTags);

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }

        /// <summary>
        /// Método para adicionar uma tag
        /// </summary>
        /// <remarks>
        /// Método que adiciona
        /// </remarks>
        /// <param name="tagVm">TagViewModel</param>
        /// <returns></returns>
        /// <response code="200">Tag</response>
        /// <response code="404">Tag not foundd</response>
        [HttpPost]
        [Route("addtag")]
        public HttpResponseMessage NewTag([FromBody] TagViewModel tagVm)
        {

            try
            {

                Tag tag = new Tag();

                tag.Code = tagVm.Code;
                tag.Status = tagVm.Status;
                _tag.Add(tag);
                log = new Log();
                log.Message = "The object was added";
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
        /// Get tag by id
        /// </summary>
        /// <remarks>
        /// Get a Tag by id
        /// </remarks>
        /// <param name="id">Id of tags</param>
        /// <returns></returns>
        /// <response code="200">Tag found</response>
        /// <response code="404">Tag not founded</response>
        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetTag(string id)
        {

            try
            {

                var tag = _tag.GetEntityById(int.Parse(id));

                return Request.CreateResponse(HttpStatusCode.OK, tag);

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }

        /// <summary>
        /// Get tag by id
        /// </summary>
        /// <remarks>
        /// Get a tag by id
        /// </remarks>
        /// <param name="id">Id of tag</param>
        /// <returns></returns>
        /// <response code="200">Tag found</response>
        /// <response code="404">Tag not foundd</response>
        [HttpDelete]
        [Route("delete/{id}")]
        public HttpResponseMessage DeleteTag(string id)
        {

            try
            {

                var tag = _tag.GetEntityById(int.Parse(id));

                if (tag==null)
                {

                    Exception e = new Exception("The tag was not finded");
                    throw e;

                }

                _tag.Remove(tag);

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

        /// <summary>
        /// Get curso by id
        /// </summary>
        /// <remarks>
        /// Get a Tag by id
        /// </remarks>
        /// <param name="id">Id of course</param>
        /// <param name="tagVm">Id of course</param>
        /// <returns></returns>
        /// <response code="200">Tag found</response>
        /// <response code="404">Tag not foundd</response>
        [HttpPut]
        [Route("update/{id}")]
        public HttpResponseMessage UpdateTag([FromBody] TagViewModel tagVm, string id)
        {

            try
            {

                Tag tag = _tag.GetEntityById(int.Parse(id));

                tag.Code = tagVm.Code;
                tag.Status = tagVm.Status;

                _tag.Update(tag);

                return Request.CreateResponse(HttpStatusCode.OK, "The tag was updated");

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.OK, e.Message);
            }

        }


    }
}
