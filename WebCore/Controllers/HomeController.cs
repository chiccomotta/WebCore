using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Chunks.Generators;
using WebCore.Models;

namespace WebCore.Controllers
{
    public class HomeController : Controller
    {

        protected readonly IDataProtector protector;

        public HomeController(IDataProtectionProvider provider)
        {
            protector = provider.CreateProtector("WebCore.HomeController.v1");
        }

        
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult list()
        {
            return View();
        }

        public ViewResult About()
        {            
            ViewBag.Message = "My personal ViewBag message";
            ViewBag.NomeECognome = "Cristiano Motta";
            ViewBag.Indirizzo = "Via Valeriana, 14";

            
            ViewData["Address"] = new AddressViewModel()
            {
                Address = "Via Valeriana, 14",
                City = "Sondrio",
                PostalCode = "23100",
                Phone = "00000000000"
            };

            // Specificare un Path per la view
            //return View("~/Views/Account/Login");

            return View();
        }

        [Authorize]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public ViewResult CustomVariable()
        {
            ViewData["id"] = RouteData.Values["id"].ToString();
            return View();
        }

        [HttpGet]
        [Route("api/ApiTest")]
        public string ApiTest()
        {
            return "Ciao a tutti";
        }

        [Route("api/save")]
        public string save()
        {
            HttpContext.Session.SetString("Utente", "CRISTIANO MOTTA");
            return "";
        }

        [Route("api/read")]
        public string read()
        {
            var t = HttpContext.Session.GetString("Utente");
            return t;
        }

        [Route("api/post")]
        [HttpPost]
        public string post([FromBody]Customer customer)
        {
            // protect the payload
            string protectedPayload = protector.Protect("ciccio");
            Debug.WriteLine($"Protect returned: {protectedPayload}");

            // unprotect the payload
            string unprotectedPayload = protector.Unprotect(protectedPayload);
            Debug.WriteLine($"Unprotect returned: {unprotectedPayload}");

            return unprotectedPayload;
        }
    }
}
