using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Article.Controllers
{
    public class ProjectStatusController : BaseDnnController
    {
        // GET: ProjectStatus
        public ActionResult Index()
        {
            var list = uow.ProjectStatusRepo.GetAll();
            return View(list);
        }

        public ActionResult Create(ProjectStatus model)
        {
            if(Request.HttpMethod == "GET")
            {
                ModelState.Clear();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    if(uow.ProjectStatusRepo.Existed(model.Code, model.Name))
                    {
                        ModelState.AddModelError("", "Đã tồn tại");
                        return View(model);
                    }

                    uow.ProjectStatusRepo.Insert(model);
                    uow.SaveAndClose();
                    return RedirectToDefaultRoute();
                }
            }
            return View(model);
        }

        public ActionResult Edit(ProjectStatus model, string oldcode, string oldname, int id)
        {
            if (Request.HttpMethod == "GET")
            {
                model = uow.ProjectStatusRepo.GetByID(id);
                ModelState.Clear();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    if ((oldcode != model.Code || oldname != model.Name) 
                        && uow.ProjectStatusRepo.Existed(model.Code, model.Name))
                    {
                        ModelState.AddModelError("", "Đã tồn tại");
                        return View(model);
                    }

                    uow.ProjectStatusRepo.Update(model);
                    uow.SaveAndClose();
                    return RedirectToDefaultRoute();
                }
            }
            return View(model);
        }
    }
}