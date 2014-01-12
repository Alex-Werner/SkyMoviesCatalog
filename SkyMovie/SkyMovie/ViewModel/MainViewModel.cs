using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TMDbLib.Client;
using TMDbLib.Objects;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;
using TMDbLib.Utilities;

namespace SkyMovie.ViewModel
{ 
   
    public class MainViewModel : INotifyPropertyChanged
    {
        public static TMDbClient APIClient { get; set; }

        #region Property Changed Business
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Bindable Properties

        private String _statusMiniBarHours;
        private String _statusMiniBarText;
        private bool _addToCollectedIsEnabled;
        public String StatusMiniBarHours
        {
            get { return _statusMiniBarHours; }
            set
            {
                _statusMiniBarHours = value;
                RaisePropertyChanged("StatusMiniBarHours");
            }
        }
        
        public bool EnableAddToCollectionBtn
        {
            get { return _addToCollectedIsEnabled; }
            set
            {
                _addToCollectedIsEnabled = value;
                RaisePropertyChanged("AddToCollectedIsEnabled");
            }
        }

        public String StatusMiniBarText
        {
            get { return _statusMiniBarText; }
            set
            {
                _statusMiniBarText = value;
                RaisePropertyChanged("StatusMiniBarText");
            }
        }

       
        #endregion

        public MainViewModel()
        {
            // Display the hours in the bottom-right of the main UI
            Timer timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(DisplayActualHours); 
            timer.Interval = 1000; // Timer will tick every second 
            timer.Enabled = true;
            timer.Start(); // Start the timer 
            
            StatusMiniBarText = "Ready !";
            EnableAddToCollectionBtn = false;

            APIClient = new TMDbClient(ConfigurationManager.AppSettings["ApiKey"]);
            setStatusBarText("Connected to TMDb");
            

            
        }
        /// <summary>
        /// Set the MiniBar text
        /// </summary>
        /// <param name="text"></param>
        void setStatusBarText(string text)
        {
            StatusMiniBarText = text;
        }

        void DisplayActualHours(object sender, EventArgs e)
        {
            StatusMiniBarHours = DateTime.Now.ToString();
        }
        void MovieDB_CollectionChange(object sender, CollectionChangeEventArgs e)
        {
            
        }
    }
}
