using Microsoft.AspNetCore.Http;
using PersonnelDepartment.Models;
using PersonnelDepartment.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelDepartment.ViewModels
{
    public class EditWorkerViewModel
    {
        [Display(Name = "Change Avatar")]
        [DataType(DataType.Upload)]
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".png" })]
        public IFormFile Avatar { get; set; }
        public int Id { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(30)]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public bool EmailConfirmed { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Education { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(30)]
        public string Position { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(30)]
        public string Status { get; set; }

        public byte[] Picture { get; set; }
    }
}
