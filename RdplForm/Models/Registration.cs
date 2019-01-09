using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RdplForm.Models
{
    public class Registration
    {
        [Key]
        [Required]
        [RegularExpression("^[a-zA-Z]{5,20}$", ErrorMessage = "Name must contains Atleast 5 characters ")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^([a-zA-Z])[a-zA-Z_-]*[\w_-]*[\S]$|^([a-zA-Z])[0-9_-]*[\S]$|^[a-zA-Z]*[\S]$", ErrorMessage = "Spaces Are Not Allowed.")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "minimum Length 8 ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [MinLength(8)]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "password must be same")]

        public string Confirm_pwd { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "insert only 10 digit number ")]
        [RegularExpression(@"^\d+$", ErrorMessage = "only numbers are allowed")]
        public string Mobile_no { get; set; }
    }
}