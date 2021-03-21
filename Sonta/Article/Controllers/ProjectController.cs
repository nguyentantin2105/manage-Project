using Article.Models;
using Core.Entities;
using Core.Models;
using Core.Services;
using DotNetNuke.Security.Roles;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Article.Controllers
{
    public class ProjectController : BaseDnnController
    {
        // GET: Project
        public ActionResult Index(FilterModel filter)
        {
            ViewBag.Filter = filter;
            return View(uow.ProjectRepo.PagingModel(filter.Keyword, filter.PageIndex, filter.PageSize));
        }
        
        public ActionResult Create(Project model, List<int> emp, HttpPostedFileBase file)
        {
            var list = RoleController.Instance.GetUserRoles(PortalSettings.PortalId, "", "Manager");
            ViewBag.Users = new SelectList(list, "UserID", "FullName", model.ManagerID);
            var listEmployee = RoleController.Instance.GetUserRoles(PortalSettings.PortalId, "", "Employee");
            ViewBag.Employees = new SelectList(listEmployee, "UserID", "FullName", emp);
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
                    if (uow.ProjectRepo.Existed(model.ProjectName))
                    {
                        ModelState.AddModelError("", "Project is existed");
                        return View(model);
                    }
                    //
                    else
                    {
                        //save person for project
                        using (DbContextTransaction dbTran = uow._context.Database.BeginTransaction())
                        {
                            try
                            {
                                model.ProcessStatus = "KH";
                                uow.ProjectRepo.Insert(model);
                                uow.Save();

                                if (emp != null)
                                {
                                    foreach (var item in emp)
                                    {
                                        uow.ProjectPersonRepo.Insert(new ProjectPerson
                                        {
                                            ProjectId = model.ID,
                                            UserId = item
                                        });
                                    }
                                    uow.Save();
                                }

                                if (file.ContentLength > 0)
                                {
                                    uow.DocumentRepo.Insert(new Documents
                                    {
                                        ProjectId = model.ID,
                                        Name = Path.GetFileNameWithoutExtension(file.FileName),
                                        Path = UploadService.Upload(file),
                                        Extension = Path.GetExtension(file.FileName),
                                        PostDate = DateTime.Now
                                    });

                                    uow.Save();
                                }

                                dbTran.Commit();
                                return RedirectToDefaultRoute();
                            }
                            catch (Exception ex)
                            {
                                ModelState.AddModelError("", ex.Message);
                                dbTran.Rollback();
                            }
                            finally{
                                uow.Close();
                            }
                        }
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id, List<int> emp)
        {
            var item = uow.ProjectRepo.GetByID(id);
            var list = RoleController.Instance.GetUserRoles(PortalSettings.PortalId, "", "Manager");
            ViewBag.Users = new SelectList(list, "UserID", "FullName", item.ManagerID);
            var listOldEmployees = uow.ProjectRepo.GetEmployees(item.ID).Select(s => s.UserId);
            var listEmployee = RoleController.Instance.GetUserRoles(PortalSettings.PortalId, "", "Employee");
            //ViewBag.Employees = new SelectList(listEmployee, "UserID", "FullName", listOldEmployees.Select(s=>s.UserId));
            var listtemp = listEmployee.Select(s => new SelectListItem
            {
                Text = s.FullName,
                Value = s.UserID.ToString(),
                Selected = listOldEmployees.Contains(s.UserID)
            });
            ViewBag.Employees = listtemp;
            ViewBag.Status = new SelectList(uow.ProjectStatusRepo.GetAll(), "Code", "Name", item.ProcessStatus);
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(Project model, List<int> emp)
        {
            if (ModelState.IsValid)
            {
                model.StartDate = DateTime.ParseExact(model.StartDateTemp, "dd/MM/yyyy", null);
                model.ExpectEndDate = DateTime.ParseExact(model.ExpectEndDateTemp, "dd/MM/yyyy", null);
                var item = uow.ProjectRepo.GetByID(model.ID);
                if (item != null)
                {

                    item.ProjectName = model.ProjectName;
                    item.Customer = model.Customer;
                    item.ManagerID = model.ManagerID;
                    item.Description = model.Description;
                    item.StartDate = model.StartDate;
                    item.ExpectEndDate = model.ExpectEndDate;
                    item.ProcessStatus = model.ProcessStatus;

                    using (DbContextTransaction dbTran = uow._context.Database.BeginTransaction())
                    {
                        uow.ProjectPersonRepo.DeleteEmployees(model.ID);

                        if (emp != null)
                        {
                            foreach (var item1 in emp)
                            {
                                uow.ProjectPersonRepo.Insert(new ProjectPerson
                                {
                                    ProjectId = model.ID,
                                    UserId = item1
                                });
                            }
                            uow.Save();
                        }
                        uow.ProjectRepo.Update(item);
                        uow.Save();
                        dbTran.Commit();
                        uow.Close();
                        return RedirectToDefaultRoute();
                    }
                }
                ModelState.AddModelError("", "Database connection error!");
            }
            return View(model);
        }

        public ActionResult Detail(int id = 0)
        {
            ViewBag.Functions = uow.FunctionRepo.GetByProject(id);
            ViewBag.Documents = uow.DocumentRepo.GetByProject(id);
            var item = uow.ProjectRepo.Detail(id);
            return View(item);
        }

        [HttpGet]
        public ActionResult Delete(int id=0)
        {
            var item = uow.ProjectRepo.Find(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult Delete(Project project, int id)
        {
            var item = uow.ProjectRepo.GetByID(id);
            uow.ProjectRepo.Delete(item);
            uow.Save();
            return RedirectToDefaultRoute();
        }
    }
}