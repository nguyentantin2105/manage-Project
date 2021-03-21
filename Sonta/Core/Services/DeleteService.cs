using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class DeleteService
    {
        UnitOfWork uow;
        public DeleteService(UnitOfWork _uow)
        {
            uow = _uow;
        }
        public void DeleteById(int id, string type = "")
        {
            switch (type)
            {
                case "projectstatus":
                    uow.ProjectStatusRepo.Delete(id);
                    break;
                case "project":
                    uow.ProjectRepo.Delete(id);
                    break;
                case "function":
                    uow.FunctionRepo.Delete(id);
                    break;
                case "document":
                    uow.DocumentRepo.Delete(id);
                    break;
            }
        }
    }
}
