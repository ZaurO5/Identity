using System.ComponentModel.DataAnnotations;

namespace Identity.ViewModels.Account
{
    public class AccountForgetPasswordVM
    {
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
