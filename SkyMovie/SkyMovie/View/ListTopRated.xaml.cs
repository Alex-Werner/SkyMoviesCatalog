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
    /// Logique d'interaction pour ListTopRated.xaml
    /// </summary>
    public partial class ListTopRated : UserControl, IDisplayer
    {
        public ListTopRated()
        {
            InitializeComponent();
        }

        private void Search_Grid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            foreach (var selectedCell in e.AddedCells)
            {
                var selectedItem = selectedCell.Item;
                var selectedCol = selectedCell.Column.DisplayIndex;

                var selectedId = ((SearchData)(selectedItem)).Id;
                var selectedName = ((SearchData)(selectedItem)).Nom;

                ((MainWindow)Application.Current.MainWindow).AddToCollectionBtn.IsEnabled = true;
                ((MainWindow)Application.Current.MainWindow).AddToWishlistBtn.IsEnabled = true;

            }
        }
    }
}
