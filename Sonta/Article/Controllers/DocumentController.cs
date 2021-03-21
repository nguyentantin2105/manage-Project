using Core.Entities;
using Core.Services;
using DotNetNuke.Web.Mvc.Common;
using DotNetNuke.Web.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Article.Controllers
{
    public class DocumentController : BaseDnnController
    {
        // GET: Document
        public ActionResult Index(int projectId)
        {
            var project = uow.ProjectRepo.GetByID(projectId);
            ViewBag.Project = project != null ? project : new Project();
            //var list = uow.FunctionRepo.GetAll().Where(m => m.ProjectID == projectId);
            var list = uow.DocumentRepo.GetByProject(projectId);
            return View(list);
        }

        public ActionResult Create(Documents model, HttpPostedFileBase file, int projectId = 0)
        {
            if (Request.HttpMethod == "GET")
            {
                model.PostDate = DateTime.Now;
                ModelState.Clear();
            }
            else
            {   
                //model.PostDate = DateTime.ParseExact(model.PostDateTemp, "dd/MM/yyyy", null);
                if (ModelState.IsValid)
                {
                    if (uow.DocumentRepo.GetByProject(projectId).Any(s => s.Document.Name == model.Name))
                    {
                        ModelState.AddModelError("", "Document is existed");
                        return View(model);
                    }
                    else
                    {
                        if (file.ContentLength > 0)
                        {
                            string pathfile = UploadService.Upload(file);
                            if (!string.IsNullOrEmpty(pathfile)) {// Upload success
                                uow.DocumentRepo.Insert(new Documents
                                {
                                    ProjectId = model.ProjectId,
                                    Name = Path.GetFileNameWithoutExtension(file.FileName),
                                    Path = pathfile,
                                    Extension = Path.GetExtension(file.FileName),
                                    PostDate = DateTime.Now
                                });
                                uow.Save();
                                uow.Close();
                                var routeVals = TypeHelper.ObjectToDictionary(new { });
                                routeVals["controller"] = "Project";
                                routeVals["action"] = "Detail";
                                var url = ModuleRoutingProvider.Instance().GenerateUrl(routeVals, ModuleContext) + "?id=" + model.ProjectId;
                                return Redirect(url);
                            }
                            ModelState.AddModelError("", "Upload file fail");
                        }
                        ModelState.AddModelError("", "Cannot upload file have length equal 0 byte");

                    }
                }
            }
            return View(model);
        }
    }
}