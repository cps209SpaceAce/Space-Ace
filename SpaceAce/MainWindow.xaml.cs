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
using System.Media;

namespace SpaceAce
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public bool isCheating = false;
        public Difficulty menuDiff = Difficulty.Easy;

        int shipIndex = 0;
        List<string> shipIMGS = new List<string>()
        {
            "player1.png",
            "player2.png",
            "player3.png",
            "player4.png",
            "player5.png"
        };



        public MainWindow()
        {
            InitializeComponent();
            //using (var soundPlayer = new SoundPlayer(SpaceAce.Properties.Resources.mainMenu))
            //{
            //    soundPlayer.Play();
            //}

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

       

        private void btnHighScorePage_Click(object sender, RoutedEventArgs e)
        {
            HighScoreWindow highScoreWindow = new HighScoreWindow();
            highScoreWindow.Show();
        }
        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            GameWindow gameWindow = new GameWindow(menuDiff, false, isCheating, shipIMGS[shipIndex]);
            gameWindow.Show();
        }
        private void btnLoadGame_Click(object sender, RoutedEventArgs e)
        {
            // Not menuDiff
            GameWindow gameWindow = new GameWindow(menuDiff, true, isCheating , shipIMGS[shipIndex]);
            
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

        private void cheating_Click(object sender, RoutedEventArgs e)
        {
            if (isCheating)
            {
                isCheating = false;
                cheating.Content = "Cheat: False"; // False
            }
            else
            {
                isCheating = true;
                cheating.Content = "Cheat: True"; // True
            }
        }

        private void imgUP_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (shipIndex == shipIMGS.Count - 1)
            {
                shipIndex = 0;
            }
            else
            {
                shipIndex += 1;
            }   
            imgSHIP.Source = new BitmapImage(new Uri("images/playerships/" + shipIMGS[shipIndex], UriKind.Relative));
        }

        private void imgDOWN_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (shipIndex == 0)
            {
                shipIndex = shipIMGS.Count - 1;
            }
            else
            {
                shipIndex -= 1;
            }
            imgSHIP.Source = new BitmapImage(new Uri("images/playerships/" + shipIMGS[shipIndex], UriKind.Relative));
        }
    }
}
