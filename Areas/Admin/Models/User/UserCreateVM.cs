using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Identity.Areas.Admin.Models.User
{
    public class UserCreateVM
    {
        public UserCreateVM()
        {
            RoleIds = new List<string>();
        }
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required. Please Enter")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Required. Please Enter")]
        public string City { get; set; }

        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Required. Please Enter")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Required. Please Enter")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password is not the same")]
        public string ConfirmPassword { get; set; }

        public List<SelectListItem>? Roles { get; set; }
        public List<string> RoleIds { get; set; }
    }
}
