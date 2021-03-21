using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class ProjectModel
    {
        private Project _Project;

        public Project Project {
            get
            {
                return _Project == null ? new Project() : _Project;
            }
            set
            {
                _Project = value;
            }
        }

        public IEnumerable<UserModel> Employees { get; set; }
        public UserModel Manager { get; set; }

        public int Total { get; set; }
        public int CompleteCount { get; set; }
        public float Percent
        {
            get
            {
                if (CompleteCount <= 0)
                {
                    return 0.00F;
                }
                return (float)CompleteCount / Total * 100;
            }
        }
    }
}
