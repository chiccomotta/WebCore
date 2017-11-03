using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebCore.Services;

namespace WebCore.Controllers
{
    public class MusicAlbumController : Controller
    {
        private readonly MusicAlbumRepository musicRepository;


        public MusicAlbumController(MusicAlbumRepository musicRepository)
        {
            this.musicRepository = musicRepository;
        }

        public ViewResult Index()
        {
            return View(musicRepository.AlbumList);
        }

        public IActionResult Change()
        {
            // Cambio la data di Release degli album
            foreach (var album in this.musicRepository.AlbumList)
            {
                album.ReleaseDate = 3333;
            }

            return View("Index", musicRepository.AlbumList);
        }        
    }
}