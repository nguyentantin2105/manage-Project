using Core.Entities;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class FunctionModel
    {
        private Function _Function;

        public Function Function
        {
            get
            {
                return _Function == null ? new Function() : _Function;
            }
            set
            {
                _Function = value;
            }
        }
        
        public UserModel Developer { get; set; }

        public string StatusName { get; set; }
    }
}