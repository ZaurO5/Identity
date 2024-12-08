using System.ComponentModel.DataAnnotations;

namespace Identity.ViewModels.Account
{
    public class AccountRegisterVM
    {
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required. Please Enter")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Required. Please Enter")]
        public string City { get; set; }

        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Required. Please Enter")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Required. Please Enter")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password is not the same")]
        public string ConfirmPassword { get; set; }
    }
}
