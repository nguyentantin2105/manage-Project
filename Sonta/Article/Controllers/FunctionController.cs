using Core.Entities;
using Article.Models;
using Core.Models;
using DotNetNuke.Security.Roles;
using DotNetNuke.Web.Mvc.Common;
using DotNetNuke.Web.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Article.Controllers
{
    public class FunctionController : BaseDnnController
    {
        // GET: Function
        public ActionResult Index(int projectId)
        {
            var project = uow.ProjectRepo.GetByID(projectId);
            ViewBag.Project = project != null ? project : new Project();
            //var list = uow.FunctionRepo.GetAll().Where(m => m.ProjectID == projectId);
            var list = uow.FunctionRepo.GetByProject(projectId);
            return View(list);
        }

        public ActionResult Create(Function model, int projectId=0)
        {
            ViewBag.Users = new SelectList(uow.ProjectRepo.GetEmployees(projectId), "UserID", "DisplayName", model.DevID);
            ViewBag.Status = new SelectList(uow.ProjectStatusRepo.GetAll(), "Code", "Name", model.Status);
            var project = uow.ProjectRepo.GetByID(projectId);
            ViewBag.Project = project;
            if (Request.HttpMethod == "GET")
            {
                model.StartDate = model.ExpectEndDate = DateTime.Now;
                ModelState.Clear();
            }
            else
            {
                model.StartDate = DateTime.ParseExact(model.StartDateTemp, "dd/MM/yyyy", null);
                model.ExpectEndDate = DateTime.ParseExact(model.ExpectEndDateTemp, "dd/MM/yyyy", null);
                if (ModelState.IsValid)
                {
                    if (uow.FunctionRepo.GetByProject(projectId).Any(s=>s.Function.FuncName==model.FuncName))
                    {
                        ModelState.AddModelError("", "Function is existed");
                        return View(model);
                    }
                    else
                    {
                        if (DateTime.Compare(model.StartDate, project.ExpectEndDate) > 0 && DateTime.Compare(model.ExpectEndDate, project.ExpectEndDate) > 0)
                        {
                            ModelState.AddModelError("", "Ngày bắt đầu và ngày kết thúc của tính năng không hợp lệ!");
                        }
                        else if ((DateTime.Compare(model.ExpectEndDate, project.ExpectEndDate) > 0 && DateTime.Compare(model.StartDate, project.ExpectEndDate)<0) || (DateTime.Compare(model.ExpectEndDate, model.StartDate) < 0))
                        {
                            ModelState.AddModelError("ExpectEndDate", "Ngày kết thúc của tính năng không hợp lệ!");
                        }
                        else if (DateTime.Compare(model.ExpectEndDate, project.ExpectEndDate) < 0 && DateTime.Compare(model.StartDate, project.ExpectEndDate) > 0)
                        {
                            ModelState.AddModelError("StartDate", "Ngày bắt đầu của tính năng không hợp lệ!");
                        }
                        else
                        {
                            uow.FunctionRepo.Insert(model);
                            uow.Save();
                            uow.Close();
                            var routeVals = TypeHelper.ObjectToDictionary(new { });
                            routeVals["controller"] = "Project";
                            routeVals["action"] = "Detail";
                            var url = ModuleRoutingProvider.Instance().GenerateUrl(routeVals, ModuleContext) + "?id=" + model.ProjectID;
                            return Redirect(url);
                        }
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var item = uow.FunctionRepo.GetByID(id);
            if (item == null)
            {
                item = new Function();
            }
            ViewBag.Users = new SelectList(uow.ProjectRepo.GetEmployees(item.ProjectID), "UserID", "DisplayName", item.DevID);
            ViewBag.Status = new SelectList(uow.ProjectStatusRepo.GetAll(), "Code", "Name", item.Status);
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(Function model)
        {
            if (ModelState.IsValid)
            {
                model.StartDate = DateTime.ParseExact(model.StartDateTemp, "dd/MM/yyyy", null);
                model.ExpectEndDate = DateTime.ParseExact(model.ExpectEndDateTemp, "dd/MM/yyyy", null);
                var item = uow.FunctionRepo.GetByID(model.ID);
                if (item != null)
                {
                    item.FuncName = model.FuncName;
                    item.Description = model.Description;
                    item.DevID = model.DevID;
                    item.StartDate = model.StartDate;
                    item.ExpectEndDate = model.ExpectEndDate;
                    item.Status = model.Status;
                    item.ProjectID = model.ProjectID;

                    uow.FunctionRepo.Update(item);
                    uow.Save();
                    var routeVals = TypeHelper.ObjectToDictionary(new { projectId = model.ProjectID });
                    routeVals["controller"] = "Function";
                    routeVals["action"] = "Index";
                    return Redirect(ModuleRoutingProvider.Instance().GenerateUrl(routeVals, ModuleContext));
                }
                ModelState.AddModelError("", "Database connection error!");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            var item = uow.FunctionRepo.Detail(id);
            return View(item);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var item = uow.FunctionRepo.GetByID(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = uow.FunctionRepo.GetByID(id);
            uow.FunctionRepo.Delete(item);
            uow.Save();
            uow.Close();
            var routeVals = TypeHelper.ObjectToDictionary(new { projectId = item.ProjectID });
            routeVals["controller"] = "Function";
            routeVals["action"] = "Index";
            return Redirect(ModuleRoutingProvider.Instance().GenerateUrl(routeVals, ModuleContext));
        }
    }
}
