using System;
using System.ComponentModel.DataAnnotations;

namespace Magazine011.ViewModels
{
    public class EditProfileViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage ="This name field is required!")]
        public string Name { get; set; }

        [Required]
        public string Role { get; set; }

        public string Photo { get; set; }
    }
}
