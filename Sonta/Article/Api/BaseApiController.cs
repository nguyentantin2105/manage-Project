using Core;
using DotNetNuke.Web.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace FbService
{
    [DnnAuthorize]
    public class BaseApiController : DnnApiController
    {
        protected UnitOfWork uow;
        public BaseApiController()
        {
            uow = new UnitOfWork(new ProjectContext());
        }
        protected string ModelStateError(ModelStateDictionary modelstate)
        {
            try
            {
                if (!modelstate.IsValid)
                {
                    return string.Join("; ", modelstate.Values
                                            .SelectMany(x => x.Errors)
                                            .Select(x => x.Exception != null ? x.Exception.Message : x.ErrorMessage));
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Error server";
        }
    }
}
