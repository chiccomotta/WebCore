using System.ComponentModel.DataAnnotations;

namespace WebCore.Models
{
    public class ChangePasswordModel
    {
        [Required]
        public string Password { get; set; }

        [Required]        
        [Compare("Password", ErrorMessage = "Password e password di conferma devono coincidere")]
        public string ConfirmPassword { get; set; }        
    }
}