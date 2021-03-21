using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class UnitOfWork
    {
        public ProjectContext _context;
        public UnitOfWork(ProjectContext context)
        {
            _context = context;
        }

        public ProjectRepository ProjectRepo { get { return new ProjectRepository(_context); } }
        public FunctionRepository FunctionRepo { get { return new FunctionRepository(_context); } }
        public ProjectStatusRepo ProjectStatusRepo { get { return new ProjectStatusRepo(_context); } }
        public ProjectPersonRepo ProjectPersonRepo { get { return new ProjectPersonRepo(_context); } }
        public UserRepo UserRepo { get { return new UserRepo(_context); } }
        public DocumentRepo DocumentRepo { get { return new DocumentRepo(_context); } }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void SaveAndClose()
        {
            Save();
            Close();
        }
        public void Close()
        {
            if (_context.Database.Connection.State == System.Data.ConnectionState.Open)
                _context.Database.Connection.Close();

        }
    }
    }
