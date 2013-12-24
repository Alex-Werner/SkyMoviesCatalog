using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using SkyMovie.Model;
using SkyMovie.ViewModel;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace SkyMovie.View
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {
        private ListFilm _listFilmWpf;
        private Statistiques _statistiquesmWpf;
        private Recherche _rechercheWpf;
        private StatusBar myStatusBar;
        public MainWindow()
        {
            InitializeComponent();
            ImageSource source = this.Icon;
            myStatusBar = new StatusBar();
            myStatusBar.StatusText = "test";

            _statistiquesmWpf = new Statistiques();

            _listFilmWpf = new ListFilm();
            _rechercheWpf = new Recherche();
            
            contentGrid.Children.Add((UserControl)_listFilmWpf);

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
            if(searchText.Text.Length>1)
            {
                SearchContainer<SearchMovie> results = MainViewModel.APIClient.SearchMovie(searchText.Text);
                ObservableCollection<SearchData> SearchResult = new ObservableCollection<SearchData>();
                foreach (SearchMovie result in results.Results)
                {
                    SearchResult.Add(new SearchData(result.Title, "...."));
                }
                _rechercheWpf.Search_Grid.ItemsSource = SearchResult;

            }


           
            //SkyMovie.View.Recherche.
            
            /*var search = searchText.Text.ToString();
            SearchContainer<SearchMovie> results = MainViewModel.APIClient.SearchMovie("Die Hard");
            foreach (SearchMovie result in results.Results)
            {
                
            }*/
            //SearchContainer<SearchMovie> results = MainViewModel.APIClient.SearchMovie(searchText.Text.ToString());
            //foreach (var name in results)
            //{
                //SearchResult.Add(new )
            //}
            //SearchResult
            //searchText.Text;
           
            RemoveChild();
            contentGrid.Children.Add((UserControl) _rechercheWpf);
        }
    }
}
