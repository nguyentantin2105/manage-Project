using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class BaseService
    {
        protected UnitOfWork uow;
        public BaseService(UnitOfWork _uow)
        {
            uow = _uow;
        }
    }
}
