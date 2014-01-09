using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Objects.General;

namespace SkyMovie.Model
{
    [Serializable]
    class Movie
    {
       public int Id { get; set; }
        public string Nom { get; set; }
        public string Genre { get; set; }
        public string Duree { get; set; }
        public string Overview { get; set; }
        public string PosterPath { get; set; }

        public Movie(int id, string nom, List<Genre> genre, string duree, string overview, string posterpath)
        {
            this.Id = id;
            this.Nom = nom;
            var newGenre = "";
            foreach (Genre g in genre)
            {
                newGenre = newGenre+g.Name+",";
            }

            this.Genre = newGenre.Substring(0, newGenre.Length - 1);
            this.Duree = duree;
            this.Overview = overview;
            this.PosterPath = posterpath;
        }


    }
}
