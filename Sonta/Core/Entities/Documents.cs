using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    [Table("A_Document")]
    public class Documents
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
        public int? ProjectId { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime PostDate { get; set; }

        [NotMapped]
        public string PostDateTemp { get; set; }
    }
}
