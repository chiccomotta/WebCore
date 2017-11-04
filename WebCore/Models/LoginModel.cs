using System.ComponentModel.DataAnnotations;

namespace WebCore.Models
{
    public class LoginModel
    {
        [Required]      
        public string Username { get; set; } = "default";

        [Required]        
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password e password di conferma devono coincidere")]
        public string ConfirmPassword { get; set; }

        public string GetFullName() => Username;
    }
}
