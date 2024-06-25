using Google.Protobuf.WellKnownTypes;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace VPMS.Lib.Data.Models
{
    public class LoginModel
    {
        [Display(Name = "Username")]
        public string Username { get; set; } = null!;

        [RegularExpression(@"(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^\w\s])^.{4,}$" , ErrorMessage = "Password must contain atleast one uppercase, lowercase, number, special character.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;

        [RegularExpression(@"(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^\w\s])^.{4,}$", ErrorMessage = "Password must contain atleast one uppercase, lowercase, number, special character.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; } = "Abcd@1234";
    }


    public class RegisterModel : LoginModel
    {

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
