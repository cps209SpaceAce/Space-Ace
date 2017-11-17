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
using System.Windows.Shapes;
using Model;

namespace SpaceAce
{
    /// <summary>
    /// Interaction logic for AddScoreWindow.xaml
    /// </summary>
    public partial class AddScoreWindow : Window
    {

        public GameController ctrlSave;
        public AddScoreWindow(GameController ctrl)
        {
            ctrlSave = ctrl;
            //if (ctrl.gameResult == GameResult.Lost)
            //{
            //    imgGameResult.Source = new BitmapImage(new Uri("Images/gameover.png", UriKind.Relative));
            //}
            
            //else
            //{
            //    ImageSource img = new BitmapImage(new Uri("Images/victory.png", UriKind.Relative));
            //    imgGameResult.Source = img;
            //}
            
            
            InitializeComponent();
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {

            HighScoreManager sc = new HighScoreManager();
            sc.Update(new HighScore(txbName.Text, ctrlSave.level, ctrlSave.difficulty, ctrlSave.score, "spaceship-hi.png"));
            sc.Save();
            Close();
            // Not adding, overriding the first
            // Not sorting by score
            // Need to downgrade level if game over
        }
    }

    
}
