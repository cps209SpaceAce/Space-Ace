﻿using System;
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

/// <summary>
/// Window for adding an Score
/// Difference based on Win or Lose
/// </summary>

namespace SpaceAce
{
    /// <summary>
    /// Interaction logic for AddScoreWindow.xaml
    /// </summary>
    public partial class AddScoreWindow : Window
    {

        public GameController ctrlSave; // Passed GameController
        MediaPlayer ResultSound;        // MediaPlayer for Sound
        public AddScoreWindow(GameController ctrl)
        {
            ctrlSave = ctrl;
           
            
            
            InitializeComponent();

            if(ctrlSave.gameResult == GameResult.Lost)
            {
                ResultSound = new MediaPlayer();
                ResultSound.Open(new Uri(System.Environment.CurrentDirectory + "/Resources/loseMusic.wav", UriKind.Absolute));
                Application.Current.Dispatcher.BeginInvoke(new Action(() => ResultSound.Play()));

            }
            else
            {
                ResultSound = new MediaPlayer();
                ResultSound.Open(new Uri(System.Environment.CurrentDirectory + "/Resources/WinMusic.wav", UriKind.Absolute));
                Application.Current.Dispatcher.BeginInvoke(new Action(() => ResultSound.Play()));

            }

        }
        // Addeds a HighScore and Closes Window
        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {

            HighScoreManager sc = new HighScoreManager();
            sc.Update(new HighScore(txbName.Text, ctrlSave.level, ctrlSave.difficulty, ctrlSave.score, "spaceship-hi.png"));
            sc.Save();
            ResultSound.Stop();
            Close();
            // Not adding, overriding the first
            // Not sorting by score
            // Need to downgrade level if game over
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (ctrlSave.gameResult == GameResult.Lost)
            {
                imgGameResult.Source = new BitmapImage(new Uri("Images/gameover.png", UriKind.Relative));
                
            }

            else
            {
                ImageSource img = new BitmapImage(new Uri("Images/victory.png", UriKind.Relative));
                imgGameResult.Source = img;
                
            }
        }
    }

    
}
