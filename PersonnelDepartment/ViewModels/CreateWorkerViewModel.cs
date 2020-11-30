using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PersonnelDepartment.Validators;

namespace PersonnelDepartment.ViewModels
{
    public class CreateWorkerViewModel
    {
        [AllowedExtensions(new string[] { ".png" })]
        [Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        [MaxFileSize(5 * 1024 * 1024)]
        public IFormFile Avatar { get; set; }
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
    }
}
