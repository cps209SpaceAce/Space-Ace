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

        public void update()
        {
            
            Canvas.SetTop(i, e.loc.Y);
            Canvas.SetLeft(i, e.loc.X);
        
        }
    }
    public partial class GameWindow : Window
    {
        public List<Icon> icons = new List<SpaceAce.Icon>();

        public GameController cltr;

        public List<Image> images = new List<Image>();

        public int spawnCounter = 0;

        public bool isPaused = false;

        public DispatcherTimer timer;

        public GameWindow(Difficulty setDiff)
        {
            InitializeComponent();
            // Load from levels

            cltr = new GameController(setDiff);
        }
        // Don't Delete this VVV
        //WindowState="Maximized"
        //WindowStyle="None"

        public void Window_Loaded2(Object sender, RoutedEventHandler stuff)
        {
            // Create Player
            Image img = new Image() { Source = new BitmapImage(new Uri("images/" + "spaceship-hi.png", UriKind.Relative)) };
            WorldCanvas.Children.Add(img);
            img.Width = 50;
            Canvas.SetLeft(img, 0);
            Canvas.SetTop(img,0);
            icons.Add(new Icon() { i = images[0], e = cltr.player });

            // Start Timer
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0,10,10);
            timer.Start();

        }

        public void Timer_Tick(object sender, EventArgs e)
        {
            cltr.player.UpdatePosition(); // Update the Player Positions
            cltr.UpdateWorld();           // Update the Model
            SpawnEntities();              // Spawn Entities

            // Update GUI
            foreach(Icon ic in icons)
                ic.update();

            //if(cltr.difficulty == Difficulty.Easy)
            //{Console.WriteLine("EASY");}
            //else{Console.WriteLine("OTHER");}
        }
        // Spawing Logic - Every 5 Seconds - Pop 5
        private void SpawnEntities()
        {

            if (spawnCounter > 4)
            {
                spawnCounter = 0;
                for (int index = 0; index < 5; ++index)  // Pop 5 and add to current_Enimies
                {
                    if (index < cltr.enemie_Que.Count)
                    {
                        if (cltr.enemie_Que[0] != null)
                        {
                            cltr.current_Enemies.Add(cltr.enemie_Que[0]); // Add to Model
                            Image img = new Image() { Source = new BitmapImage(new Uri("images/" + "Ship 1.png", UriKind.Relative)) };
                            WorldCanvas.Children.Add(img);
                            img.Width = 50;
                            Canvas.SetLeft(img, 0);
                            Canvas.SetTop(img, 0);
                            // THIS VVV

                            // The Index is crashing
                            icons.Add(new Icon() { i = img, e = cltr.current_Enemies[cltr.current_Enemies.Count - 1] });
                            // -----

                            cltr.enemie_Que.RemoveAt(0); // Remove from spawn QUE
                        }
                    }
                }
            }
            else
            {
                ++spawnCounter;
            }
        }


        private void WorldCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    if (!isPaused)
                    {
                        timer.Stop();
                        isPaused = true;
                        // Display Pause Menu
                    }
                    else
                    {
                        timer.Start();
                        isPaused = false;
                        // Close Pause Menu
                    }
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

     

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Image img = new Image() { Source = new BitmapImage(new Uri("images/" + "spaceship-hi.png", UriKind.Relative)) };
            WorldCanvas.Children.Add(img);
            img.Width = 50;
            
            Canvas.SetLeft(img, 0);
            Canvas.SetTop(img, 0);
            icons.Add(new Icon() { i = img, e = cltr.player });

            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.Start();
            // Create New canvas items

            // Start Timer

            // Take diff from ctrl
            // Load from levels

        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    // Nothing
                    break; //TODO: pause game
                case Key.Left:
                    cltr.left = false;
                    break;
                case Key.Right:
                    cltr.right = false;
                    break;
                case Key.Up:
                    cltr.up = false;
                    break;
                case Key.Down:
                    cltr.down = false;
                    break;
                case Key.Space:
                    cltr.fired = false;
                    break;
                case Key.B:
                    cltr.bomb = false;
                    break;
                default:
                    break;
            }
        }
    }
}
