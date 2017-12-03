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

/// <summary>
/// Window for info about the game.
/// </summary>

namespace SpaceAce
{
    /// <summary>
    /// Interaction logic for HelpWindow.xaml
    /// </summary>
    public partial class HelpWindow : Window
    {
        public HelpWindow()
        {
            InitializeComponent();
        }

        public void Window_Loaded(Object sender, RoutedEventArgs stuff)
        {
        }
        // Closes the Window
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
