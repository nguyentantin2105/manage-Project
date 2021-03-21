using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    [Table("A_Function")]
    public class Function
    {
        [Key]
        public int   ID { get; set; }
        public string FuncName { get; set; }
        public string Description { get; set; }

        public int ProjectID { get; set; }
        public string Status { get; set; }
        public int DevID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpectEndDate { get; set; }
        public DateTime? RealEndDate { get; set; }

        [NotMapped]
        public string StartDateTemp { get; set; }

        [NotMapped]
        public string ExpectEndDateTemp { get; set; }

        [NotMapped]
        public string RealEndDateTemp { get; set; }
    }
}
