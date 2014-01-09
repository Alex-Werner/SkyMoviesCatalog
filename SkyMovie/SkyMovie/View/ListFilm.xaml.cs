using System;
using System.Collections.Generic;
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
    public partial class ListFilm : UserControl, IDisplayer
    {
        public ListFilm()
        {
            InitializeComponent();
        }

        private void List_Grid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
           
            foreach (var selectedCell in e.AddedCells)
            {
                var main = ((MainWindow) Application.Current.MainWindow);
                var selectedItem = selectedCell.Item;
                var selectedCol = selectedCell.Column.DisplayIndex;
                
                var selectedId = ((Movie)(selectedItem)).Id;
                var selectedName = ((Movie)(selectedItem)).Nom;

                main.DelFromCollectionBtn.IsEnabled = true;

                main.SelectedFilmName.Text = selectedName;
                main.SelectedFilmOverview.Text = ((Movie)(selectedItem)).Overview;
                var img_uri = "http://image.tmdb.org/t/p/w92" + ((Movie) (selectedItem)).PosterPath;
                var img = new BitmapImage(new Uri(img_uri, UriKind.Absolute));
                main.SelectedFilmPoster.Source = img;
                    
                    

                

            }
        
        }
        

    }
}
