using System;
using Core;
using Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            UnitOfWork uow = new UnitOfWork(new ProjectContext());
            ProjectService pser = new ProjectService(uow);
            using(var transaction = uow._context.Database.BeginTransaction())
            {
                try
                {
                    var list = pser.GetPersons(1016);
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                }
            }

            //int count = list.Count;
        }

        [TestMethod]
        public void DetailProject()
        {
            UnitOfWork uow = new UnitOfWork(new ProjectContext());
            var projectDe = uow.FunctionRepo.GetByProject(17);

            int count = 0;
        }
    }
}
