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
using Genre = SkyMovie.Model.Sk_Genre;
using Movie = SkyMovie.Model.Movie;

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
        private StatusBar myStatusBar;
        private Home _homeWpf;

        ObservableCollection<SearchData> SearchResult = new ObservableCollection<SearchData>();
        ObservableCollection<Movie> MovieDB = new ObservableCollection<Movie>();
        Persistance persistanceMovieDB = new Persistance("MovieDB.bin");

        public MainWindow()
        {
            MovieDB = persistanceMovieDB.getPersistance("MovieDB.bin");

           
            

            InitializeComponent();
            ImageSource source = this.Icon;
            myStatusBar = new StatusBar();
            myStatusBar.StatusText = "test";

            _statistiquesmWpf = new Statistiques();

            _listFilmWpf = new ListFilm();
            _rechercheWpf = new Recherche();
            _homeWpf = new Home();

            contentGrid.Children.Add((UserControl) _homeWpf);
            //contentGrid.Children.Add((UserControl)_listFilmWpf);

            AddToCollectionBtn.IsEnabled = false;
            DelFromCollectionBtn.IsEnabled = false;
        }



        


        /* Method used for dragging the title bar */
        bool inDrag = false;
        Point anchorPoint;

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
        private void SkyMenu_Click(object sender, RoutedEventArgs e)
        {

           // SkyMenu.SelectedItem = SkyMenu.Items.CurrentItem;
        }

        private void SkyMenu_MaCollection_Click(object sender, RoutedEventArgs e)
        {
            RemoveChild();

            _listFilmWpf.List_Grid.ItemsSource = MovieDB;
            
            contentGrid.Children.Add((UserControl)_listFilmWpf);

        }
        private void SkyMenu_Exporter_Click(object sender, RoutedEventArgs e)
        {
            RemoveChild();
        }
        private void SkyMenu_Statistiques_Click(object sender, RoutedEventArgs e)
        {
            RemoveChild();
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

        private void AddToCollectionBtn_Click(object sender, RoutedEventArgs e)
        {
            
            DataGridCellInfo cell = _rechercheWpf.Search_Grid.SelectedCells[0];
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
            persistanceMovieDB.setPersistance("MovieDB.bin",MovieDB);
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

            persistanceMovieDB.setPersistance("MovieDB.bin", MovieDB);

            ((MainWindow)Application.Current.MainWindow).DelFromCollectionBtn.IsEnabled = false;

        }

        
    }
}
