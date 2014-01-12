using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SkyMovie.Interface;
using SkyMovie.Model;

namespace SkyMovie.View
{
    /// <summary>
    /// Logique d'interaction pour ListFilm.xaml
    /// </summary>
    public partial class Statistiques : UserControl, IDisplayer
    {
        public Statistiques()
        {
            InitializeComponent();
        }

        public void DoSoMuchStats(ObservableCollection<Movie> MovieDB, ObservableCollection<Movie> WishedMovieDB)
        {
            int SawMovie = 0;
            int HowManySecWasted = 0;
            int HowManySeen = 0;

            AcquieredMovie.Text = MovieDB.Count.ToString();
            WantedMovie.Text = WishedMovieDB.Count.ToString();



            for (int i = MovieDB.Count - 1; i >= 0; i--)
            {
                var item = MovieDB[i];
                if (item.Seen == true)
                {
                    HowManySecWasted = + Convert.ToInt32(item.Duree);
                    HowManySeen++;
                }
            }
            SeenMovie.Text = HowManySecWasted.ToString();

            var span = new TimeSpan(0, 0, HowManySecWasted);
            var day = span.Days;
            var hours = span.Hours;
            var min = span.Minutes;
            var sec = span.Seconds;

            TimeWasted.Text = "Vous avez perdu "+   day.ToString() +" jours, " + hours.ToString()+"heures, "+      min.ToString() +"minutes et"+sec.ToString()+ "secondes";
        }
    }
}
