using System.ComponentModel.DataAnnotations;

namespace Identity.ViewModels.Account
{
    public class AccountResetPasswordVM
    {
        [Required(ErrorMessage = "Required. Please Enter")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Required. Please Enter")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Password is not the same")]
        public string ConfirmNewPassword { get; set; }

        public string Email { get; set; }
        public string Token { get; set; }
    }
}
