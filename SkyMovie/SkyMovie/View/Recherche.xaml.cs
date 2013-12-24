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
using TMDbLib.Objects.Movies;

namespace SkyMovie.View
{
    /// <summary>
    /// Logique d'interaction pour ListFilm.xaml
    /// </summary>
    public partial class Recherche : UserControl, IDisplayer
    {
        private ObservableCollection<SearchData> SearchResult { get; set; }
        
        public Recherche()
        {
           
            InitializeComponent();
          
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            /*SearchResult = new ObservableCollection<SearchData>();
            SearchResult.Add(new SearchData("Film1", "Action"));
            SearchResult.Add(new SearchData("Film2", "Action"));

            var grid = sender as DataGrid;
            grid.ItemsSource = SearchResult;*/
        }

    }
    public class SearchData
    {
        public string Nom { get; set; }
        public string Genre { get; set; }
        public SearchData(string nom, string genre)
        {
            this.Nom = nom;
            this.Genre = genre;
        }
    }
}
