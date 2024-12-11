using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Identity.Areas.Admin.Models.User
{
    public class UserUpdateVM
    {
        public UserUpdateVM()
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

        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Password is not the same")]
        public string? ConfirmNewPassword { get; set; }

        public List<SelectListItem>? Roles { get; set; }
        public List<string> RoleIds { get; set; }
    }
}
