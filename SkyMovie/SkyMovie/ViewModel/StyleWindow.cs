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
        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.WindowState = WindowState.Minimized;
        }
        private void MaxButton_Click(object sender, RoutedEventArgs e)
        {
            //Comme on sait pas comment faire pour enlever la bordure dégeu lorsque resize n'est pas égale à none. Et bien l'user aura deux choix et pis voila !
            App.Current.MainWindow.WindowState = WindowState.Maximized;
        }

    }

}
