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

        public Difficulty menuDiff = Difficulty.Easy;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnHighScorePage_Click(object sender, RoutedEventArgs e)
        {
            HighScoreWindow gameWindow = new HighScoreWindow();
            gameWindow.Show();
        }
        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            GameWindow gameWindow = new GameWindow(menuDiff, false);
            gameWindow.Show();
        }
        private void btnLoadGame_Click(object sender, RoutedEventArgs e)
        {
            // Not menuDiff
            GameWindow gameWindow = new GameWindow(menuDiff, true);
            
            // Load Game

            gameWindow.Show();
        }
        private void btnDifficulty_Click(object sender, RoutedEventArgs e)
        {
            switch (menuDiff)
            {
                case Difficulty.Easy:
                    menuDiff = Difficulty.Medium;
                    break;
                case Difficulty.Medium:
                    menuDiff = Difficulty.Hard;
                    break;
                case Difficulty.Hard:
                    menuDiff = Difficulty.Easy;
                    break;
            }
            btnDifficulty.Content = "Difficulty: " + menuDiff.ToString();

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
