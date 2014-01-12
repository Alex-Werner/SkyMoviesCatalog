using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using SkyMovie.ViewModel;
using TMDbLib.Objects.Movies;

namespace SkyMovie.View
{
    /// <summary>
    /// Logique d'interaction pour ListFilm.xaml
    /// </summary>
    public partial class Recherche : UserControl, IDisplayer
    {
        private ObservableCollection<SearchData> SearchResult { get; set; }
        private DependencyObject _parent;
        
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
        private void Search_Grid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            foreach (var selectedCell in e.AddedCells)
            {
                var selectedItem = selectedCell.Item;
                var selectedCol = selectedCell.Column.DisplayIndex;
                
                var selectedId = ((SearchData)(selectedItem)).Id;
                var selectedName = ((SearchData)(selectedItem)).Nom;

                ((MainWindow) Application.Current.MainWindow).AddToCollectionBtn.IsEnabled = true;
                ((MainWindow) Application.Current.MainWindow).AddToWishlistBtn.IsEnabled = true;

            }
        }

    }
}
