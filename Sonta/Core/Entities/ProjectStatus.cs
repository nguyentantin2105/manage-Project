using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    [Table("A_ProjectStatus")]
    public class ProjectStatus
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tên")]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }
    }
}
