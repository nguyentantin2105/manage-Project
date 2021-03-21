using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class DocumentModel
    {
        private Documents _Document;

        public Documents Document
        {
            get
            {
                return _Document == null ? new Documents() : _Document;
            }
            set
            {
                _Document = value;
            }
        }
    }
}
