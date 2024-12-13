using System.ComponentModel.DataAnnotations;

namespace Identity.ViewModels.Account
{
    public class AccountResetPasswordVM
    {
        [Required(ErrorMessage = "New password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm new password is required")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Confirm password is wrong")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
        public string Token { get; set; }
    }
}
