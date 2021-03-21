using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class UserModel
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string DisplayName { get; set; }

        public bool IsSuperUser { get; set; }
        
    }
}
