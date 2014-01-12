using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using SkyMovie.Interface;
using SkyMovie.Model;
using SkyMovie.ViewModel;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;
using Application = System.Windows.Application;
using Genre = SkyMovie.Model.Sk_Genre;
using MessageBox = System.Windows.MessageBox;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using Movie = SkyMovie.Model.Movie;
using StatusBar = SkyMovie.Model.StatusBar;
using UserControl = System.Windows.Controls.UserControl;
using Export = SkyMovie.Model.Export.ExportToExcel;
namespace SkyMovie.View
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window, IDisplayer
    {
        private ListFilm _listFilmWpf;
        private Statistiques _statistiquesmWpf;
        private Recherche _rechercheWpf;
        private ListLastOut _listLastOutWpf;
        private ListTopRated _listTopRatedWpf;
        private ListPopular _listPopularWpf;
        private WishList _wishListWpf;
        private static StatusBar myStatusBar;
        private Home _homeWpf;

        private Export _exportToExcel;

        
        ObservableCollection<SearchData> SearchResult = new ObservableCollection<SearchData>();
        static ObservableCollection<Movie> MovieDB = new ObservableCollection<Movie>();
        static ObservableCollection<Movie> WishedMovieDB = new ObservableCollection<Movie>(); 
        static Persistance persistanceMovieDB = new Persistance("MovieDB.bin");
        static Persistance persistanceWishedMovieDB = new Persistance("WishedMovieDB.bin");


        public MainWindow()
        {
            MovieDB = persistanceMovieDB.getPersistance("MovieDB.bin");
            WishedMovieDB = persistanceWishedMovieDB.getPersistance("WishedMovieDB.bin");
           
            

            InitializeComponent();
            ImageSource source = this.Icon;


            _statistiquesmWpf = new Statistiques();

            _listFilmWpf = new ListFilm();
            _listLastOutWpf = new ListLastOut();
            _listTopRatedWpf = new ListTopRated();
            _listPopularWpf = new ListPopular();
            _wishListWpf = new WishList();
            _rechercheWpf = new Recherche();
            _homeWpf = new Home();

            contentGrid.Children.Add((UserControl) _homeWpf);

            AddToCollectionBtn.IsEnabled = false;
            DelFromCollectionBtn.IsEnabled = false;
            AddToWishlistBtn.IsEnabled = false;
            DelFromWishlistBtn.IsEnabled = false;
        }



        


        /* Method used for dragging the title bar */
        bool inDrag = false;
        Point anchorPoint;

        public static void saveContext()
        {
            persistanceMovieDB.setPersistance("MovieDB.bin", MovieDB);
            persistanceWishedMovieDB.setPersistance("WishedMovieDB.bin", WishedMovieDB);
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            anchorPoint = PointToScreen(e.GetPosition(this));
            inDrag = true;
            CaptureMouse();
            e.Handled = true;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (inDrag)
            {
                Point currentPoint = PointToScreen(e.GetPosition(this));
                this.Left = this.Left + currentPoint.X - anchorPoint.X;
                this.Top = this.Top + currentPoint.Y - anchorPoint.Y;
                anchorPoint = currentPoint;
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (inDrag)
            {
                ReleaseMouseCapture();
                inDrag = false;
                e.Handled = true;
                
            }
        }

        public void RemoveChild()
        {
            contentGrid.Children.Clear();
        }
        #region MenuButtons
        private void SkyMenu_MaCollection_Click(object sender, RoutedEventArgs e)
        {
            RemoveChild();

            _listFilmWpf.List_Grid.ItemsSource = MovieDB;
            
            contentGrid.Children.Add((UserControl)_listFilmWpf);

        }
        private void SkyMenu_MaWishList_Click(object sender, RoutedEventArgs e)
        {
            RemoveChild();

            _wishListWpf.List_Grid.ItemsSource = WishedMovieDB;

            contentGrid.Children.Add((UserControl)_wishListWpf);

        }
        private void SkyMenu_Discover_Click(object sender, RoutedEventArgs e)
        {
            RemoveChild();
        }
        private void SkyMenu_TopGender_Click(object sender, RoutedEventArgs e)
        {
            RemoveChild();
        }
        private void SkyMenu_DernieresSorties_Click(object sender, RoutedEventArgs e)
        {
            RemoveChild();
            SearchResult.Clear();
            
            var nbPageMax = 3;

            for(var nbPage=1; nbPage<=nbPageMax;nbPage++)
            {

                SearchContainer<MovieResult> results = MainViewModel.APIClient.GetMovieList(MovieListType.Popular, nbPage);
                foreach (MovieResult result in results.Results)
                {
                    SearchResult.Add(new SearchData(result.Id, result.Title, "Note:" + result.VoteAverage + " (" + result.VoteCount + ")"));
                }
            }

            _listLastOutWpf.Search_Grid.ItemsSource = SearchResult;
            contentGrid.Children.Add((UserControl) _listLastOutWpf);
        }
        private void SkyMenu_TopRated_Click(object sender, RoutedEventArgs e)
        {
            RemoveChild();
            SearchResult.Clear();

            var nbPageMax = 3;

            for (var nbPage = 1; nbPage <= nbPageMax; nbPage++)
            {

                SearchContainer<MovieResult> results = MainViewModel.APIClient.GetMovieList(MovieListType.TopRated,nbPage);
                foreach (MovieResult result in results.Results)
                {
                    SearchResult.Add(new SearchData(result.Id, result.Title,
                                                    "Note:" + result.VoteAverage + " (" + result.VoteCount + ")"));
                }
            }
            _listTopRatedWpf.Search_Grid.ItemsSource = SearchResult;
            contentGrid.Children.Add((UserControl)_listTopRatedWpf);

        }
        private void SkyMenu_Popular_Click(object sender, RoutedEventArgs e)
        {
            RemoveChild();
            SearchResult.Clear();

            var nbPageMax = 3;
            for (var nbPage = 1; nbPage<=nbPageMax; nbPage++)
            {
                SearchContainer<MovieResult> results = MainViewModel.APIClient.GetMovieList(MovieListType.Popular, nbPage);
                foreach (MovieResult result in results.Results)
                {
                    SearchResult.Add(new SearchData(result.Id, result.Title,
                                                    "Note:" + result.VoteAverage + " (" + result.VoteCount + ")"));
                }   
            }
            _listPopularWpf.Search_Grid.ItemsSource = SearchResult;
            contentGrid.Children.Add((UserControl) _listPopularWpf);
        }
        private void SkyMenu_Exporter_Click(object sender, RoutedEventArgs e)
        {
            _exportToExcel = new Export();
            _exportToExcel.ExportDbToFile("MyMovieDb.xlsx", MovieDB, WishedMovieDB);
        }
        private void SkyMenu_Statistiques_Click(object sender, RoutedEventArgs e)
        {
            RemoveChild();
            _statistiquesmWpf.DoSoMuchStats(MovieDB, WishedMovieDB);
            contentGrid.Children.Add((UserControl)_statistiquesmWpf);

        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchResult.Clear();

            if(searchText.Text.Length>1)
            {

                SearchContainer<SearchMovie> results = MainViewModel.APIClient.SearchMovie(searchText.Text);
                foreach (SearchMovie result in results.Results)
                {
                    SearchResult.Add(new SearchData(result.Id,result.Title,"...."));
               }
                _rechercheWpf.Search_Grid.ItemsSource = SearchResult;
            }
           
            RemoveChild();
            contentGrid.Children.Add((UserControl) _rechercheWpf);
        }
        #endregion

        #region Add/Del Button
        private void AddToCollectionBtn_Click(object sender, RoutedEventArgs e)
        {


            DataGridCellInfo cell;
            if (contentGrid.Children[0].ToString() == "SkyMovie.View.ListLast.Out")
            {
                cell = _listLastOutWpf.Search_Grid.SelectedCells[0];

            }


            else if (contentGrid.Children[0].ToString() == "SkyMovie.View.ListPopular")
            {
                cell = _listPopularWpf.Search_Grid.SelectedCells[0];

            }


            else if (contentGrid.Children[0].ToString() == "SkyMovie.View.ListTopRated")
            {
                cell = _listTopRatedWpf.Search_Grid.SelectedCells[0];

            }
            else//Search
            {
                cell = _rechercheWpf.Search_Grid.SelectedCells[0];

            }
            
            var col = cell.Column;
            var content = col.GetCellContent(cell.Item);
            var dt = content.DataContext;


            var selectedName = ((SearchData) (dt)).Nom;
            var selectedId = ((SearchData)(dt)).Id;
            var selectedGenre = ((SearchData)(dt)).Genre;


            TMDbLib.Objects.Movies.Movie getMovie = MainViewModel.APIClient.GetMovie(selectedId, MovieMethods.Casts);
            

            Movie selectedMovie = new Movie(selectedId, selectedName, getMovie.Genres, getMovie.Runtime.ToString(), getMovie.Overview, getMovie.PosterPath.ToString());

            MessageBox.Show(selectedMovie.Nom+" a bien été ajouté.");
            MovieDB.Add(selectedMovie);
            saveContext();
            ((MainWindow)Application.Current.MainWindow).AddToCollectionBtn.IsEnabled = false;

        }
        private void DelFromCollectionBtn_Click(object sender, RoutedEventArgs e)
        {

            DataGridCellInfo cell = _listFilmWpf.List_Grid.SelectedCells[0];
            var col = cell.Column;
            var content = col.GetCellContent(cell.Item);
            var dt = content.DataContext;


            var selectedName = ((Movie)(dt)).Nom;
            var selectedId = ((Movie)(dt)).Id;
            

            for (int i = MovieDB.Count - 1; i >= 0; i--)
            {
                var item = MovieDB[i];
                if (item.Id == selectedId)
                {
                    MovieDB.Remove(item);
                }
            }

            MessageBox.Show(selectedName + " a bien été retiré.");
            _listFilmWpf.List_Grid.ItemsSource = MovieDB;

            saveContext();

            ((MainWindow)Application.Current.MainWindow).DelFromCollectionBtn.IsEnabled = false;

        }

        private void AddToWishListBtn_Click(object sender, RoutedEventArgs e)
        {
            DataGridCellInfo cell;
            if(contentGrid.Children[0].ToString() == "SkyMovie.View.ListLast.Out")
            {
                cell = _listLastOutWpf.Search_Grid.SelectedCells[0];

            }


            else if (contentGrid.Children[0].ToString() == "SkyMovie.View.ListPopular")
            {
                cell = _listPopularWpf.Search_Grid.SelectedCells[0];

            }


            else if (contentGrid.Children[0].ToString() == "SkyMovie.View.ListTopRated")
            {
                cell = _listTopRatedWpf.Search_Grid.SelectedCells[0];

            }
            else//Search
            {
                cell = _rechercheWpf.Search_Grid.SelectedCells[0];

            }

            var col = cell.Column;
            var content = col.GetCellContent(cell.Item);
            var dt = content.DataContext;

            var selectedName = ((SearchData)(dt)).Nom;
            var selectedId = ((SearchData)(dt)).Id;
            var selectedGenre = ((SearchData)(dt)).Genre;

            TMDbLib.Objects.Movies.Movie getMovie = MainViewModel.APIClient.GetMovie(selectedId, MovieMethods.Casts);

            Movie selectedMovie = new Movie(selectedId, selectedName, getMovie.Genres, getMovie.Runtime.ToString(), getMovie.Overview, getMovie.PosterPath.ToString());

            MessageBox.Show(selectedMovie.Nom + " a bien été ajouté.");

            WishedMovieDB.Add(selectedMovie);
            saveContext();

            ((MainWindow)Application.Current.MainWindow).AddToCollectionBtn.IsEnabled = false;
            ((MainWindow)Application.Current.MainWindow).AddToWishlistBtn.IsEnabled = false;



        }
        /// <summary>
        /// Delete a node from WishList. Trigger a window asking us if we want to del it for an add in our film list or just delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DelFromWishListBtn_Click(object sender, RoutedEventArgs e)
        {

            DataGridCellInfo cell = _wishListWpf.List_Grid.SelectedCells[0];
            var col = cell.Column;
            var content = col.GetCellContent(cell.Item);
            if (content != null)
            {
                var dt = content.DataContext;


                var selectedName = ((Movie)(dt)).Nom;
                var selectedId = ((Movie)(dt)).Id;

                WishedMovieDB = persistanceWishedMovieDB.getPersistance("WishedMovieDB.bin");


                MessageBoxResult result = MessageBox.Show("Voulez-vous en plus de la suppression de votre wishlist, ajouter ce film à votre liste de film acquis ?", "Ajouter à votre Wishlist ?", MessageBoxButton.YesNo);
               
                for (int i = WishedMovieDB.Count - 1; i >= 0; i--)
                {
                    var item = WishedMovieDB[i];
                    if (item.Id == selectedId)
                    {
                        if (result == MessageBoxResult.Yes)
                        {
                            MovieDB.Add(item);
                        }

                        WishedMovieDB.Remove(item);
                    }
                }

                MessageBox.Show(selectedName + " a bien été retiré.");
            }
            _wishListWpf.List_Grid.ItemsSource = MovieDB;

            saveContext();

            ((MainWindow)Application.Current.MainWindow).DelFromWishlistBtn.IsEnabled = false;

        }
        #endregion


    }
}
