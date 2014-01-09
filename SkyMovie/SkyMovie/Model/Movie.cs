using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyMovie.Model
{
    [Serializable]
    class Movie
    {
       public int Id { get; set; }
        public string Nom { get; set; }
        public string Genre { get; set; }
        public Movie(int id, string nom, string genre)
        {
            this.Id = id;
            this.Nom = nom;
            this.Genre = genre;
        }


    }
}
