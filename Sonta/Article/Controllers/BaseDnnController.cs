using Core;
using DotNetNuke.Web.Mvc.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Article.Controllers
{
    public class BaseDnnController : DnnController
    {
        protected UnitOfWork uow = new UnitOfWork(new ProjectContext());
        public BaseDnnController()
        {

        }

        protected HttpCookie CreateCookie(string name, string value)
        {
            HttpCookie cookie = new HttpCookie(name);
            cookie.Value = value;
            cookie.Expires = DateTime.Now.AddYears(1);
            return cookie;
        }

        protected string GetCookie(string name)
        {
            if(Request.Cookies.Get(name) == null)
            {
                return "";
            }
            return Request.Cookies.Get(name).Value;
        }

        protected string UploadFile(HttpPostedFileBase file)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    DateTime now = DateTime.Now;
                    string extension = Path.GetExtension(file.FileName);
                    string name = now.ToString("yyyyMMddHHmmssfff") + extension;
                    string childPath = Server.MapPath(PortalSettings.HomeDirectory) + "Uploads\\" + now.Year + "\\" + now.Month;

                    if(!Directory.Exists( childPath))
                    {
                        Directory.CreateDirectory(childPath);
                    }

                    string _path = Path.Combine(childPath, name);
                    file.SaveAs(_path);
                    return now.Year + "/" + now.Month + "/" + name;
                }
            }
            catch(Exception ex)
            {
                return "";
            }
            return "";
        }
    }
}