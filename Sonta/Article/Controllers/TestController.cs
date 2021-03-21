using DotNetNuke.Web.Mvc.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Article.Controllers
{
    public class TestController : DnnController
    {
        // GET: Test
        public ActionResult Index()
        {

            return View();
        }
    }
}