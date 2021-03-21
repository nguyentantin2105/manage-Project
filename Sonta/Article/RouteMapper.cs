using DotNetNuke.Web.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.SessionState;

namespace ADApiService
{
    public class RouteMapper : IServiceRouteMapper
    {
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            var routes = mapRouteManager.MapHttpRoute("FbService", "default", "{controller}/{action}", new[] { "FbService" });
            foreach (Route r in routes)
            {
                r.RouteHandler = new SessionBasedControllerRouteHandler();
            }
        }

        public class SessionBasedControllerHandler : HttpControllerHandler, IRequiresSessionState
        {
            public SessionBasedControllerHandler(RouteData routeData) : base(routeData)
            {
            }
        }
        public class SessionBasedControllerRouteHandler : HttpControllerRouteHandler
        {
            protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
            {
                return new SessionBasedControllerHandler(requestContext.RouteData);
            }
        }
    }
}