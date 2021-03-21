using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FbService
{
    public class ProjectApiController : BaseApiController
    {
        [HttpGet]
        public HttpResponseMessage DetailByProject(int funcId)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, uow.FunctionRepo.Detail(funcId));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
