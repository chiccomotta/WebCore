using FluentValidation.Results;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebCore.Models;
using WebCore.Services;

namespace WebCore.Controllers
{
    public class HomeController : Controller
    {
        protected readonly IDataProtector protector;
        protected readonly IMemoryCacheService MemoryCacheService;

        public HomeController(IDataProtectionProvider provider, IMemoryCacheService cache)
        {
            protector = provider.CreateProtector("WebCore.HomeController.v1");
            MemoryCacheService = cache;
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

        //[Authorize]
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


        public IActionResult TestMemory()
        {
            var name = MemoryCacheService.GetName();
            return new JsonResult(name);
        }

        [HttpPost]
        [Route("api/testapi")]
        public async Task<string> TestApi([FromBody] User user, CancellationToken cancellationToken)
        {
            //CancellationToken token = HttpContext.RequestAborted;
            try
            {
                // Passo il token di cancellazione al metodo per annullarlo; viene sollevata un'eccezione di tipo
                // TaskCanceledException (eredita da OperationCanceledException)
                await Task.Delay(4000, cancellationToken);
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.ToString());
            }

            if (cancellationToken.IsCancellationRequested)
            {
                Debug.WriteLine("Request cancelled!");
                return string.Empty;
            }

            //cancellationToken.ThrowIfCancellationRequested();
            return "This is a test API";
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
        public IActionResult Post([FromBody]Customer customer)  
        {
            if (!ModelState.IsValid)
                return new JsonResult("Data model not valid");

            CustomerValidator validator = new CustomerValidator();
            ValidationResult results = validator.Validate(customer);

            bool validationSucceeded = results.IsValid;
            IList<ValidationFailure> failures = results.Errors;

            Debug.WriteLine(ModelState.Values);
           
            // protect the payload
            string protectedPayload = protector.Protect("ciccio");
            Debug.WriteLine($"Protect returned: {protectedPayload}");

            // unprotect the payload
            string unprotectedPayload = protector.Unprotect(protectedPayload);
            Debug.WriteLine($"Unprotect returned: {unprotectedPayload}");

            return new JsonResult(unprotectedPayload);
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

        [Route("api/validate")]
        [HttpPost]
        public IActionResult Post([FromBody]JObject model)
        {
            // Lista degli eventuali errori di validazione del modello
            List<string> errorList = new List<string>();

            JsonConvert.DeserializeObject(model.ToString(), typeof(Person)
                , new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Error,
                    Error = (sender, args) =>
                    {
                        var currentError = args.ErrorContext.Error.Message;
                        args.ErrorContext.Handled = true;

                        errorList.Add(currentError);
                    }
                });

            // Ci sono errori
            if (errorList.Any())
            {
                string description = string.Join("[ERR]: ", errorList.ToArray())
                    .Insert(0, "La struttura dei dati non è conforme al modello dati del DataBucket. [ERR]: ");

                return new BadRequestObjectResult(description);
            }

            return new JsonResult(model);
        }        
    }
}
