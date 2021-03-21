using Core;
using Core.Common;
using DotNetNuke.Web.Api;
using Module.AdAuth;
using Module.DNNLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ADApiService
{
    [DnnAuthorize]
    public class AdController : DnnApiController
    {
        UowTacNghiep uow;
        public AdController()
        {
            uow = new UowTacNghiep(new TacNghiepContext());
        }

        [HttpPost]
        public HttpResponseMessage AddRole(int portalId, string username, string rolename)
        {
            DnnUser dnnUser = new DnnUser(username);
            var result = dnnUser.AddRole(portalId, rolename);
            if (result)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true });
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false });
        }

        [HttpPost]
        public HttpResponseMessage DeleteRole(int portalId, string username, string rolename)
        {
            DnnUser dnnUser = new DnnUser(username);
            bool result = dnnUser.DeleteRole(portalId, username, rolename);

            if (result)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { success = true });
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false });
        }

        [HttpPost]
        public HttpResponseMessage DeleteUSer(string username)
        {
            DnnUser dnnUser = new DnnUser(username);

            Dictionary<string, string> listParam = uow.SystemParamRepo.GetByGroupKey("AD");
            var pass = GlobalCommon.DecryptString(listParam[ADManager.AD_ADMIN_PASS] ?? "");
            ADManager manager = new ADManager(listParam[ADManager.AD_DOMAIN], listParam[ADManager.AD_ADMIN_USER], pass);
            if (manager.IsAuthenticated())
            {
                string message = manager.Delete(username);
                if (string.IsNullOrEmpty(message))
                {
                    bool result = dnnUser.DeleteUser(username);
                    if (result)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, new { success = true });
                    }else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, new { message = "Error delete user DNN" });
                    }
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest, new { message = message });
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, new { success = false });
        }


    }
}
