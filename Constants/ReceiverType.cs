using System.ComponentModel.DataAnnotations;

namespace Identity.Constants
{
    public enum ReceiverType
    {
        [Display(Name = ("All Users"))]
        AllUsers,
        Subscribers
    }
}
