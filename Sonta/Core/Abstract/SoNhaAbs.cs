using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Abstract
{
    public abstract class SoNhaAbs
    {
        [Key]
        public int Id { get; set; }
    }
}
