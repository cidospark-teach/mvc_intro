using System;
using System.ComponentModel.DataAnnotations;

namespace Magazine011.ViewModels
{
    public class AddUserViewModel
    {
        [Required]
        [StringLength(30, ErrorMessage = "Name must be less than 30 characters and greater than 3 characters", MinimumLength =3)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
