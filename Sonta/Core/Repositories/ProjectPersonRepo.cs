using Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class ProjectPersonRepo : GenericRepo<ProjectPerson>
    {
        public ProjectPersonRepo(ProjectContext context) : base(context)
        {

        }

        public IEnumerable<ProjectPerson> GetByProject(int projectId)
        {
            return FindBy(s => s.ProjectId == projectId);
        }

        public void DeleteEmployees(int projectId)
        {
            var listEmp = FindBy(s => s.ProjectId == projectId);
            foreach (var item in listEmp)
            {
                Delete(item);
            }
        }
    }
}
