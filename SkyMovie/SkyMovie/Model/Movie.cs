using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Objects.General;

namespace SkyMovie.Model
{
    [Serializable]
    public class Movie
    {
        #region Property Changed Business
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
       public int Id { get; set; }
        public string Nom { get; set; }
        public string Genre { get; set; }
        public string Duree { get; set; }
        public string Overview { get; set; }
        public string PosterPath { get; set; }
        public bool _seen { get; set; }

       // public bool _seen;

        public Movie(int id, string nom, List<Genre> genre, string duree, string overview, string posterpath)
        {
            this.Seen = true;
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
        public bool Seen
        {
            get { return _seen; }
            set
            {
                _seen = value;
                RaisePropertyChanged("Seen");
            }
        }

    }
}
