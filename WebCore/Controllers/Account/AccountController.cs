using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using WebCore.Models;

namespace WebCore.Controllers.Account
{
    public class AccountController : Controller
    {
        private StringValues returnUrl;


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]       
        public async Task<IActionResult> Login([FromQuery] LoginModel loginModel,
            [FromHeader] string secretCode)
        {
            if (!ModelState.IsValid)
                return View();

            if (CheckUserCredentials(loginModel.Username, loginModel.Password))
            {
                // creo una lista di claims
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, loginModel.Username),
                    new Claim("LivelloUtente", "1")
                };

                // creo lo user identity
                var userIdentity = new ClaimsIdentity(claims, "login");

                // Creo il principal
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                // setto il context con il principal appena creato
                await HttpContext.Authentication.SignInAsync("MyApplicationAuth", principal);

                // OK
                // Leggo il return Url
                //HttpContext.Request.Query.TryGetValue("ReturnUrl", out returnUrl);
                //var _returnUrl = returnUrl[0];
                return Redirect("/Home/Contact");
            }
           
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.Authentication.SignOutAsync("MyApplicationAuth");
            return Redirect("/Account/Login");
        }


        public ViewResult ChangePassword(string code)
        {
            return View();
        }



        private bool CheckUserCredentials(string username, string password)
        {
            // Qui il controllo deve essere fatto su una base dati, per il momento torniamo sempre true
            return true;
        }
    }
}