using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Windows;


using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Windows.Media;
using System.Collections;

namespace SkyMovie
{
    public partial class StyleWindow
    {
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO : LOGIQUE DE VERIFICATION (SYNCHRO BDD AVANT FERMETURE)
            App.Current.MainWindow.Close();
        }
    }
}
