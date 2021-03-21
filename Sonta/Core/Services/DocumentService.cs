using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class DocumentService
    {
        UnitOfWork uow;
        public DocumentService(UnitOfWork _uow)
        {
            uow = _uow;
        }
        
    }
}
