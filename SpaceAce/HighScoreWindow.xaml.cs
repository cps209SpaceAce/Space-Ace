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
    /// Interaction logic for HighScoreWindow.xaml
    /// </summary>
    public partial class HighScoreWindow : Window
    {
        HighScoreManager hsManager;
        public HighScoreWindow()
        {
            InitializeComponent();
        }
        public void Window_Loaded(Object sender, RoutedEventArgs stuff)
        {
            hsManager = new HighScoreManager();


            lstHighScores.FontSize = 26;
            hsManager.Sort();
            lstHighScores.Items.Add("NAME ----- LEVEL ----- DIfficulty ----- SCORE");
            for (int i = 0; i < hsManager.highScores.Count; i++)
            {
                
                lstHighScores.Items.Add(hsManager.highScores[i].ToString()); // add image later
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}