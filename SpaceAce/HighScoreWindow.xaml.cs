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

/// <summary>
/// Window for Displaying the Saved High Score
/// </summary>

namespace SpaceAce
{
    /// <summary>
    /// Interaction logic for HighScoreWindow.xaml
    /// </summary>
    public partial class HighScoreWindow : Window
    {
        HighScoreManager hsManager; // HighScore manager for display
        public HighScoreWindow()
        {
            InitializeComponent();
        }
        public void Window_Loaded(Object sender, RoutedEventArgs stuff)
        {
            hsManager = new HighScoreManager();


            lstHighScores.FontSize = 26;
            hsManager.Sort();
            lstHighScores.Items.Add(" NAME ----- LEVEL ----- DIfficulty ----- SCORE");
            for (int i = 0; i < 10; i++)
            {
                if (hsManager.highScores[i] != null) // so it doesn't crash
                    lstHighScores.Items.Add(hsManager.highScores[i].ToString()); // add image later
            }

        }
        // Closes the Window
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}