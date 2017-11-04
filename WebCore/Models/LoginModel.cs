using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
