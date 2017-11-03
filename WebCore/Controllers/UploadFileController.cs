using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebCore.Models;
using WebCore.Services;

namespace WebCore.Controllers
{
    public class UploadFileController : Controller
    {
        private IHostingEnvironment enviroment;
        private readonly ConnectionString connectionString;


        public UploadFileController(IHostingEnvironment env, IOptions<ConnectionString> connString, MusicAlbumRepository musicRepository)
        {
            this.enviroment = env;
            this.connectionString = connString.Value;


            Debug.WriteLine(connectionString.ServerInfo.IP);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Index(IFormFile file, Customer customer)
        {
            var uploads = Path.Combine(enviroment.WebRootPath, "uploads");

            using (var fs = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }

            return View();
        }
    }
}