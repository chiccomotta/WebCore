using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace ControllersAndActions.Controllers
{
    public class PocoController : Controller
    {
        public ViewResult Index() =>
            View("Result", $"This is an derived controller");


        public ViewResult Test()
        {
            // Iterazione su tutte le variabili di ambiente
            foreach (DictionaryEntry entry in Environment.GetEnvironmentVariables())
            {
                Debug.WriteLine(entry.Key + " --> " + entry.Value);
            }

            return View();
        }
    }
}