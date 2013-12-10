using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SkyMovie.ViewModel
{ 
    
    public class MainViewModel : INotifyPropertyChanged
    {
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

        public String StatusMiniBarHours
        {
            get { return _statusMiniBarHours; }
            set
            {
                _statusMiniBarHours = value;
                RaisePropertyChanged("StatusMiniBarHours");
            }
        }
        public String StatusMiniBarText
        {
            get { return _statusMiniBarHours; }
            set
            {
                _statusMiniBarHours = value;
                RaisePropertyChanged("StatusMiniBarText");
            }
        }
        #endregion
        public MainViewModel()
        {
            Timer timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(DisplayActualHours); 
            timer.Interval = 1000; // Timer will tick every second 
            timer.Enabled = true;
            timer.Start(); // Start the timer 

            StatusMiniBarText = "Ready !";
        }

        void DisplayActualHours(object sender, EventArgs e)
        {
            StatusMiniBarHours = DateTime.Now.ToString();
        }
    }
}
