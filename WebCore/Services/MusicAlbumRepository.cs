using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace WebCore.Services
{
    public class MusicAlbumRepository
    {
        public List<MusicAlbum> AlbumList = new List<MusicAlbum>()
        {
            new MusicAlbum()
            {
                Name = "In Rock",
                Artist = "Deep Purple",
                Production = "Harvest",
                Nationality = "Great Britain",
                Duration = new TimeSpan(0, 41, 46),
                ReleaseDate = 1970
            },
            new MusicAlbum()
            {
                Name = "Tommy",
                Artist = "The Who",
                Production = "Track",
                Nationality = "Great Britain",
                Duration = new TimeSpan(0, 75, 03),
                ReleaseDate = 1969
            },
            new MusicAlbum()
            {
                Name = "Sgt. Pepper's Lonely Hearts Club Band",
                Artist = "The Beatles",
                Production = "Parlophone",
                Nationality = "Great Britain",
                Duration = new TimeSpan(0, 39, 50),
                ReleaseDate = 1967
            },
            new MusicAlbum()
            {
                Name = "Innervisions",
                Artist = "Stevie Wonder",
                Production = "Motown",
                Nationality = "USA",
                Duration = new TimeSpan(0,44,12),
                ReleaseDate = 1973
            }
        };
        
        public void CercaAlbum()
        {
            var album = AlbumList.Where(x => x.ReleaseDate < 2000 && x.ReleaseDate > 1980)
                .ToList();
        }
    }


    public class MusicAlbum
    {
        [Required(ErrorMessage = "Inserire un nome!")]
        [MaxLength(20)]
        public string Name { get; set; }
        public string Artist { get; set; }
        public int ReleaseDate { get; set; }
        public string Nationality { get; set; }    
        public string Production { get; set; }
        public TimeSpan Duration { get; set; }        
    }
}
