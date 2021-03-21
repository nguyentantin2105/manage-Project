using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Article.Models
{
    public class Project
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ManagerID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpectEndDate { get; set; }
        public DateTime RealEndDate { get; set; }
        public int ProcessStatus { get; set; }
    }
    public class ProjectContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
    }
}