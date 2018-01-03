﻿using System;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebCore.Filters;
using WebCore.ModelBinders;
using WebCore.Models;
using WebCore.Services;

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
        //[KindValidator]   // Filter attribute
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


        /// <summary>
        /// Esempio di Binder automatico di una striga in base64 a un bytes[] utilizzando la classe ByteArrayModelBinder
        /// </summary>
        /// <param name="file"></param>
        /// <param name="filename"></param>
        [Route("api/image")]
        [HttpPost]
        public void Post([ModelBinder(BinderType = typeof(ByteArrayModelBinder))]byte[] file, string filename)
        {
            Debug.WriteLine(file.Length);
            string filePath = Path.Combine(@"C:\temp", filename);
            if (System.IO.File.Exists(filePath)) return;
            System.IO.File.WriteAllBytes(filePath, file);
        }


        [HttpGet("api/binder")]
        public string Get(StringArrayModel viewModel)
        {
            if (viewModel == null)
                return "Woops. We received null!";

            Debug.WriteLine(viewModel.ValuesList.Length);

            return $"data model: {JsonConvert.SerializeObject(viewModel.ValuesList)}";
        }

        [HttpGet("api/bucket")]
        public IActionResult GetBucket(DataBucket<Customer> viewModel)
        {
            var result = new DataBucket<Customer>()
            {
                Nome = "My DataBucket",
                Data = new Customer() {Name = "Cristiano", Codice = 111}
            };

            return Ok(result);
        }

        [HttpPost("api/bucket")]
        public IActionResult PostBucket([FromBody]DataBucket<JObject> viewModel)
        {
            /***
             * 
             * JObject rappresenta un oggetto json qualunque passato dal client, es:
                    
                var json = 
                {
                    "name": "My DataBucket",
                    "data": {
                        "name": "Cristiano",
                        "codice": 111
                    }
                }
            */
                                    
            JObject jobj = viewModel.Data as JObject;

            var customer = jobj.ToObject<Customer>();
            return Ok(customer);
        }


        [HttpPost("api/customer/{id}")]
        public IActionResult Test([ModelBinder(typeof(AlbumModelBinder))] MusicAlbum model)
        {
            return Ok(model);
        }
    }
}
