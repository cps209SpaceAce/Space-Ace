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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using Model;


namespace SpaceAce
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GameController ctrl = new GameController();

            

        }

        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            
            GameWindow gameWindow = new GameWindow();
            gameWindow.Show();
        }
        private void btnLoadGame_Click(object sender, RoutedEventArgs e)
        {
            
            GameWindow gameWindow = new GameWindow();
            
            // Load Game

            gameWindow.Show();
        }
        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            
            System.Windows.Application.Current.Shutdown();
        }
        // On diff button click change ctrl diff

    }
}
