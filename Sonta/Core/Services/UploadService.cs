using Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Core.Services
{
    public class UploadService
    {
        public static string Upload(HttpPostedFileBase file)
        {
            try
            {
                string path1 = "/Portals/0/Uploads";
                string path = System.Web.HttpContext.Current.Server.MapPath(path1);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var year = DateTime.Now.Year;
                path = path + "/" + year;
                path1 = path1 + "/" + year;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = path + "/" + DateTime.Now.Month;
                path1 = path1 + "/" + DateTime.Now.Month;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var filename = System.Guid.NewGuid() + "_" + DateTime.Now.ToString("ddMMMyyyy");
                path = path + "/" + filename + Path.GetExtension(file.FileName);
                path1 = path1 + "/" + filename + Path.GetExtension(file.FileName);
                
                if (!File.Exists(path))
                {
                    file.SaveAs(path);
                    return path1;
                }
                return "";
            }
            catch
            {
                return "";
            }
        }
    }
}
