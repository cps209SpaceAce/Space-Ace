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

    public struct Icon
    {
        public Image i;
        public Entity e;
    }
    public partial class GameWindow : Window
    {
        public List<Icon> icons = new List<SpaceAce.Icon>();

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
            WorldCanvas.Children.Add(images[0]);
            icons.Add(new Icon() { i = images[0], e =cltr.player });
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            // Create New canvas items

            // Start Timer

            // Take diff from ctrl
            // Load from levels

        }

        public void Timer_Tick(object sender, EventArgs e)
        {
            cltr.player.UpdatePosition();
            Icon i = icons[0];
            Canvas.SetTop(i.i, i.e.loc.Y);
            Canvas.SetLeft(i.i, i.e.loc.X);

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
