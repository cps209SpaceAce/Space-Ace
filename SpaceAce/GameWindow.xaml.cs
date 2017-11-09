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
using System.Windows.Threading;
using Model;
namespace SpaceAce
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {

        public GameController cltr = new GameController();

        public List<Image> images = new List<Image>();

        public DispatcherTimer timer;

        public GameWindow()
        {
            InitializeComponent();
        }
        //WindowState="Maximized"
        //WindowStyle="None"

        public void Window_Loaded()
        {
            images.Add(new Image() { Source = new BitmapImage(new Uri("images/" + "spaceship-hi.png", UriKind.Relative)) });

            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            // Create New canvas items

            // Start Timer

            // Take diff from ctrl
            // Load from levels

        }

        public void Timer_Tick(object sender, EventHandler e)
        {
            cltr.player.UpdatePosition();
            
        }


        private void WorldCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    break; //TODO: pause game
                case Key.Left:
                    cltr.left = true;
                    break;
                case Key.Right:
                    cltr.right = true;
                    break;
                case Key.Up:
                    cltr.up = true;
                    break;
                case Key.Down:
                    cltr.down = true;
                    break;
                case Key.Space:
                    cltr.fired = true;
                    break;
                case Key.B:
                    cltr.bomb = true;
                    break;
                default:
                    break;
            }
        }
    }
}
