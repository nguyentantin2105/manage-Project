using Core.Entities;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class FunctionRepository : GenericRepo<Function>
    {
        public FunctionRepository(ProjectContext context) : base(context)
        {

        }

        public bool Existed(string name)
        {
            return GetAll().Any(s => s.FuncName == name);
        }

        public IEnumerable<FunctionModel> GetByProject(int projectId)
        {
            var query = (from fun in Context.Set<Function>()
                         join pro in Context.Set<Project>() on fun.ProjectID equals pro.ID into funall
                         join proPer in (from proPerson in Context.Set<ProjectPerson>()
                                         join user in Context.Set<Users>() on proPerson.UserId equals user.UserId
                                         select new
                                         {
                                             ProjectId = proPerson.ProjectId,
                                             UserId = user.UserId,
                                             DisplayName = user.DisplayName,
                                             IsSuperUser = user.IsSuperUser,
                                             UserName = user.UserName
                                         }) on fun.ProjectID equals proPer.ProjectId into userall
                         join status in Context.Set<ProjectStatus>() on fun.Status equals status.Code into statuses

                         from status in statuses.DefaultIfEmpty()
                         where fun.ProjectID == projectId
                         select new FunctionModel
                         {
                             Function = fun,
                             Developer = userall.Where(s => s.UserId == fun.DevID)
                                       .Select(s => new UserModel
                                       {
                                           UserId = s.UserId,
                                           DisplayName = s.DisplayName,
                                           IsSuperUser = s.IsSuperUser,
                                           UserName = s.UserName
                                       }).FirstOrDefault(),
                             StatusName = status.Name
                         });
            return query.ToList();
        }

        public Users GetDevFunction(int funcID)
        {
            var query = (from user in Context.Set<Users>()
                         join func in Context.Set<Function>() on user.UserId equals func.DevID
                         where func.ID == funcID
                         select user);
            return query.FirstOrDefault();
        }

        public FunctionModel Detail(int funcId)
        {
            var query = (from fun in Context.Set<Function>()
                         join pro in Context.Set<Project>() on fun.ProjectID equals pro.ID into funall
                         join proPer in (from proPerson in Context.Set<ProjectPerson>()
                                         join user in Context.Set<Users>() on proPerson.UserId equals user.UserId
                                         select new
                                         {
                                             ProjectId = proPerson.ProjectId,
                                             UserId = user.UserId,
                                             DisplayName = user.DisplayName,
                                             IsSuperUser = user.IsSuperUser,
                                             UserName = user.UserName
                                         }) on fun.ProjectID equals proPer.ProjectId into userall
                          join status in Context.Set<ProjectStatus>() on fun.Status equals status.Code into statuses

                         from status in statuses.DefaultIfEmpty()
                         where fun.ID == funcId
                         select new FunctionModel
                         {
                             Function = fun,
                             Developer = userall.Where(s => s.UserId == fun.DevID)
                                       .Select(s => new UserModel
                                       {
                                           UserId = s.UserId,
                                           DisplayName = s.DisplayName,
                                           IsSuperUser = s.IsSuperUser,
                                           UserName = s.UserName
                                       }).FirstOrDefault(),
                             StatusName = status.Name
                         });
            return query.FirstOrDefault();
        }

        
    }
}
