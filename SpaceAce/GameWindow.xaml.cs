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

        public bool update()
        {
            if (e != null)
            {
                if (e.alive == true)
                {
                    Canvas.SetTop(i, e.Y);
                    Canvas.SetLeft(i, e.X);
                    return true;
                }
            }
            return false;
        
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
        public string currentLevel = "Level 1";

        public GameWindow(Difficulty setDiff, bool isLoad) //Joanna: isLoad checks whether to load game or start new one
        {
            InitializeComponent();
            // Load from levels

            cltr = new GameController(setDiff);

            if (isLoad)
                cltr.Load("SaveData.txt");
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



        public enum Id { player, computer }

        public void MakeBullet(Id id)
        {
            if (id == Id.player)
            {
                Player p = cltr.player;
                double y = p.Y + 10;
                double x = p.X + 50;
                Bullet b = new Bullet(x, y);
                cltr.player_fire.Add(b);
                Image img = new Image() { Source = new BitmapImage(new Uri("images/" + "P_bullet.png", UriKind.Relative)) };
                img.Width = 20;
                Icon i = new Icon() { i = img, e = b };
                i.update();
                WorldCanvas.Children.Add(img);
                icons.Add(i);
            }
        }


        public void Timer_Tick(object sender, EventArgs e)
        {
            List<Icon> dead = new List<Icon>();
            cltr.player.UpdatePosition(); // Update the Player Positions
            cltr.UpdateWorld();           // Update the Model
            SpawnEntities();              // Spawn Entities
            
            if (cltr.player.FiredABullet == true)
            {
                MakeBullet(Id.player);
                cltr.player.FiredABullet = false;
            }
            // Update GUI
            foreach (Icon ic in icons)
            {
                if (ic.update() == false)
                {
                    dead.Add(ic);
                    WorldCanvas.Children.Remove(ic.i);
                }
                    
            }

            foreach (Icon ic in dead)
            {
                icons.Remove(ic);
            }

            // Update Score GUI
            labelScore.Content = "Score: " + cltr.score;
            ++cltr.score;// Delete later
            
            // Update Lives
            // TODO: We can change to images for bonus
            labelLives.Content = "Lives: " + String.Concat(Enumerable.Repeat("< ", cltr.player.lives));
            labelBombs.Content = "Bombs: " + cltr.player.bombs;
            labelLevel.Content = currentLevel;
        }

        private void SpawnEntities()
        {

            if (spawnCounter > 25)
            {
                spawnCounter = 0;
                Entity newEntity = Levels.returnCurrentLevelEntity(cltr.difficulty);
                cltr.current_Enemies.Add(newEntity); // Add to Model
                Image img = null;
                string pngName = "";

                if (newEntity is Asteroid)
                    {pngName = "asteroid.png";}
                else if(newEntity is AI)
                    {pngName = "Ship 1.png";}
                
                img = new Image() { Source = new BitmapImage(new Uri("images/" + pngName, UriKind.Relative)) };
                WorldCanvas.Children.Add(img);
                img.Width = 50;
                Canvas.SetLeft(img, 0);
                Canvas.SetTop(img, 0);
                icons.Add(new Icon() { i = img, e = cltr.current_Enemies[cltr.current_Enemies.Count - 1] });                        
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
                    }
                    else
                    {
                        timer.Start();
                        isPaused = false;                        
                    }
                    break;
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

                case Key.S:
                    cltr.Save("SaveData.txt"); //added by JOANNA
                    //also, would ctrl be a better name though?
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
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    break;
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
