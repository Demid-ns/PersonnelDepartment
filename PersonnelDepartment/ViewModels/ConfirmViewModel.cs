using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelDepartment.ViewModels
{
    public class ConfirmViewModel
    {
        [Required]
        public int WorkerId { get; set; }
        [Required]
        [MinLength(36)]
        [MaxLength(36)]
        public string Password { get; set; }
    }
}
