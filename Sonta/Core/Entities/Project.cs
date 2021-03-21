using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    [Table("A_Project")]
    public class Project
    {
        [Key]
        public int ID { get; set; }
        public string ProjectName { get; set; }
        public string Customer { get; set; }
        public string Description { get; set; }
        public int ManagerID { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime ExpectEndDate { get; set; }
        public DateTime? RealEndDate { get; set; }
        public string ProcessStatus { get; set; }
        public int Price { get; set; }

        [NotMapped]
        public string StartDateTemp { get; set; }

        [NotMapped]
        public string ExpectEndDateTemp { get; set; }

        [NotMapped]
        public string RealEndDateTemp { get; set; }
    }
}
