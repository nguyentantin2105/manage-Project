using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string DisplayName { get; set; }

        public bool IsSuperUser { get; set; }
        
    }
}
