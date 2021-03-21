using Core.Entities;
using Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Core.Services
{
    public class ProjectService:BaseService
    {
        public ProjectService(UnitOfWork uow) : base(uow)
        {

        }

        public List<ProjectPersonModel> GetPersons(int projectId)
        {
            var project = uow.ProjectRepo.GetAll();
            var person = uow.ProjectPersonRepo.GetAll().Where(m => m.ProjectId == projectId);
            var result = from p in project
                         join c in person on p.ID equals c.ProjectId
                         select new ProjectPersonModel
                         {
                             FullName = "",
                             ProjectId = p.ID,
                             UserId = c.UserId,
                             Id = c.Id
                         };
            return result.ToList();
        }
    }
}
