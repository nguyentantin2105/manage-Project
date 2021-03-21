using Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class ProjectContext : DbContext
    {
        public ProjectContext() : base("SiteSqlServer") {

        }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<Function> Function { get; set; }
        public virtual DbSet<ProjectStatus> ProjectStatus { get; set; }
        public virtual DbSet<ProjectPerson> ProjectPerson { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Documents> Documents { get; set; }
    }
}
