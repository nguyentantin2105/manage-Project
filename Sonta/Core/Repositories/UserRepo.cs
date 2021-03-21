using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class UserRepo : GenericRepo<Users>
    {
        public UserRepo(ProjectContext context) : base(context)
        {

        }

       
    }
}
