using Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class ProjectStatusRepo: GenericRepo<ProjectStatus>
    {
        public ProjectStatusRepo(DbContext context) : base(context)
        {

        }

        public bool Existed(string code, string name)
        {
            var item = GetAll().Where(s => s.Code == code || s.Name == name).FirstOrDefault();
            return item != null;
        }
    }
}
