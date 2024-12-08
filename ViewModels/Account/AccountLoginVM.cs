using System.ComponentModel.DataAnnotations;

namespace Identity.ViewModels.Account
{
    public class AccountLoginVM
    {
        [Required(ErrorMessage = "Required. Please Enter")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required. Please Enter")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
