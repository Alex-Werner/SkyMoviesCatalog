﻿using System;
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
    public partial class WishList : UserControl, IDisplayer
    {
        public WishList()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Triggered when we select a cell in the wishList. Activate Delete from WishList and Add To Collection button (In the last case, automatically delete from wishlist)
        /// </summary>
        private void List_Grid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            int selectedCol = -1;
            foreach (var selectedCell in e.AddedCells)
            {
                var main = ((MainWindow)Application.Current.MainWindow);
                var selectedItem = selectedCell.Item;
                selectedCol = selectedCell.Column.DisplayIndex;

                var selectedId = ((Movie)(selectedItem)).Id;
                var selectedName = ((Movie)(selectedItem)).Nom;

                main.DelFromWishlistBtn.IsEnabled = true;

                main.SelectedFilmName.Text = selectedName;
                main.SelectedFilmOverview.Text = ((Movie)(selectedItem)).Overview;
                var img_uri = "http://image.tmdb.org/t/p/w92" + ((Movie)(selectedItem)).PosterPath;
                var img = new BitmapImage(new Uri(img_uri, UriKind.Absolute));
                main.SelectedFilmPoster.Source = img;

            }
            MainWindow.saveContext();

        }


    }
}
