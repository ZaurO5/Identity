using System.ComponentModel.DataAnnotations;

namespace Identity.ViewModels.Account
{
    public class AccountForgetPasswordVM
    {
        [Required(ErrorMessage = "Required. Please Enter")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
