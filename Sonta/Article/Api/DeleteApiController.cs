using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FbService
{
    public class DeleteApiController : BaseApiController
    {
        [HttpPost]
        public HttpResponseMessage Delete(int id, string type)
        {
            try
            {
                new DeleteService(uow).DeleteById(id, type);
                uow.SaveAndClose();
                return Request.CreateResponse(HttpStatusCode.OK, "Delete success!");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
