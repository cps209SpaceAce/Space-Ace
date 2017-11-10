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
<<<<<<< HEAD
=======
            
>>>>>>> 25a47fc934073f8321a9e575eb6efcf29bd2a74e
        }

        private void btnHighScorePage_Click(object sender, RoutedEventArgs e)
        {
            HighScoreWindow gameWindow = new HighScoreWindow();
            gameWindow.Show();
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
        private void btnDifficulty_Click(object sender, RoutedEventArgs e)
        {
            GameWindow gameWindow = new GameWindow();
            gameWindow.Show();
        }

        private void btnHighScorePage_Click(object sender, RoutedEventArgs e)
        {
            HighScoreWindow highscreWindow = new HighScoreWindow();
            highscreWindow.Show();
        }

        private void btnAboutPage_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Show();
        }

        private void btnHelpPage_Click(object sender, RoutedEventArgs e)
        {
            HelpWindow helpWindow = new HelpWindow();
            helpWindow.Show();
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
