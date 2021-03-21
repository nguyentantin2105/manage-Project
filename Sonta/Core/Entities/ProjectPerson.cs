using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
        [Table("A_ProjectPerson")]
        public class ProjectPerson
        {
            [Key]
            public int Id { get; set; }

            [Required]
            [Display(Name = "Tên")]
            public int UserId { get; set; }

            [Required]
            public int ProjectId { get; set; }
    }
}
