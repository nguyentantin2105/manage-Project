using Core.Entities;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class DocumentRepo : GenericRepo<Documents>
    {
        public DocumentRepo(ProjectContext context) : base(context)
        {

        }

        public IEnumerable<DocumentModel> GetByProject(int projectId)
        {
            var query = (from doc in Context.Set<Documents>()
                         join pro in Context.Set<Project>() on doc.ProjectId equals pro.ID into docall
                         join proPer in (from proPerson in Context.Set<ProjectPerson>()
                                         join user in Context.Set<Users>() on proPerson.UserId equals user.UserId
                                         select new
                                         {
                                             ProjectId = proPerson.ProjectId,
                                             UserId = user.UserId,
                                             DisplayName = user.DisplayName,
                                             IsSuperUser = user.IsSuperUser,
                                             UserName = user.UserName
                                         }) on doc.ProjectId equals proPer.ProjectId into userall
                         where doc.ProjectId == projectId
                         select new DocumentModel
                         {
                             Document = doc
                         });
            return query.ToList();
        }
    }
}
