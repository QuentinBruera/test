using System.ComponentModel.DataAnnotations;

namespace NegosudModel.Request
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "The email address is not valid.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public required string Password { get; set; }
    }
}
