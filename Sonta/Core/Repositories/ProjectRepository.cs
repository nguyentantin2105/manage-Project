using Core.Entities;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class ProjectRepository : GenericRepo<Project>
    {
        public ProjectRepository(ProjectContext context) : base(context)
        {

        }
        public Paging<Project> Paging(string keywords, int pageIdx, int pageSize)
        {
            var query = GetAll();
            if (!string.IsNullOrEmpty(keywords))
            {
                query = query.Where(s => s.ProjectName.Contains(keywords));
            }
            return query.OrderBy(s => s.ProjectName).GetPaged(pageIdx, pageSize);
        }
        public bool Existed (string name)
        {
            return GetAll().Any(s => s.ProjectName == name);
        }

        public Paging<ProjectModel> PagingModel(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var userQuery = (from user in Context.Set<Users>()
                             select user) as IQueryable<Users>;
            var query = (from pro in Context.Set<Project>()
                         join fun in Context.Set<Function>() on pro.ID equals fun.ProjectID into funall
                         join proPer in (from proPerson in Context.Set<ProjectPerson>() 
                               join user in Context.Set<Users>() on proPerson.UserId equals user.UserId
                               select new
                               {
                                   ProjectId = proPerson.ProjectId,
                                   UserId = user.UserId,
                                   DisplayName = user.DisplayName,
                                   IsSuperUser = user.IsSuperUser,
                                   UserName = user.UserName
                               }) on pro.ID equals proPer.ProjectId into userall
                         
                         select new ProjectModel
                         {
                             Project = pro,
                             Total = funall.Count(s => s.ProjectID == pro.ID),
                             CompleteCount = funall.Count(s => s.ProjectID == pro.ID && s.Status == "HT"),
                             Employees = userall.Where(s => s.ProjectId == pro.ID)
                                            .Select(s => new UserModel
                                            {
                                                UserId = s.UserId,
                                                DisplayName = s.DisplayName,
                                                IsSuperUser = s.IsSuperUser,
                                                UserName = s.UserName
                                            }),
                             Manager = userQuery.Where(s => s.UserId == pro.ManagerID)
                                       .Select(s => new UserModel
                                       {
                                           UserId = s.UserId,
                                           DisplayName = s.DisplayName,
                                           IsSuperUser = s.IsSuperUser,
                                           UserName = s.UserName
                                       }).FirstOrDefault()
                         });
            return query.OrderBy(s => s.Project.ProjectName).GetPaged<ProjectModel>(pageIndex, pageSize);
        }

        public ProjectModel Detail(int projectId)
        {
            var userQuery = (from user in Context.Set<Users>()
                             select user) as IQueryable<Users>;
            var query = (from pro in Context.Set<Project>()
                         join fun in Context.Set<Function>() on pro.ID equals fun.ProjectID into funall
                         join proPer in (from proPerson in Context.Set<ProjectPerson>()
                                         join user in Context.Set<Users>() on proPerson.UserId equals user.UserId
                                         select new
                                         {
                                             ProjectId = proPerson.ProjectId,
                                             UserId = user.UserId,
                                             DisplayName = user.DisplayName,
                                             IsSuperUser = user.IsSuperUser,
                                             UserName = user.UserName
                                         }) on pro.ID equals proPer.ProjectId into userall

                         where pro.ID == projectId
                         select new ProjectModel
                         {
                             Project = pro,
                             Total = funall.Count(s => s.ProjectID == pro.ID),
                             CompleteCount = funall.Count(s => s.ProjectID == pro.ID && s.Status == "HT"),
                             Employees = userall.Where(s => s.ProjectId == pro.ID)
                                            .Select(s => new UserModel
                                            {
                                                UserId = s.UserId,
                                                DisplayName = s.DisplayName,
                                                IsSuperUser = s.IsSuperUser,
                                                UserName = s.UserName
                                            }),
                             Manager = userQuery.Where(s => s.UserId == pro.ManagerID)
                                       .Select(s => new UserModel
                                       {
                                           UserId = s.UserId,
                                           DisplayName = s.DisplayName,
                                           IsSuperUser = s.IsSuperUser,
                                           UserName = s.UserName
                                       }).FirstOrDefault()
                         });
            return query.FirstOrDefault();
        }

        public IEnumerable<Users> GetEmployees(int projectId)
        {
            var query = (from user in Context.Set<Users>()
                         join proPerson in Context.Set<ProjectPerson>() on user.UserId equals proPerson.UserId
                         where proPerson.ProjectId == projectId
                         select user);
            return query.ToList();
        }

        
    }
}
