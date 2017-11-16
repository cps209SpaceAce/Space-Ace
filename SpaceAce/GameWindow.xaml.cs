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

    // Don't Delete this VVV
    //WindowState="Maximized"
    //WindowStyle="None"

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
        Button btnQUIT;
        Button btnSAVE;
        public DispatcherTimer timer;
        public string currentLevel = "Level 1";

        public GameWindow(Difficulty setDiff, bool isLoad) //Joanna: isLoad checks whether to load game or start new one
        {
            InitializeComponent();
            // Load from levels

            cltr = new GameController(setDiff, Width, Height);

            if (isLoad)
            {
                cltr.Load("SaveData.txt");
                Draw_Load();
            }

            // Quit Button
            btnQUIT = new Button { Content = "QUIT", Width = 150, Height = 50 };
            Canvas.SetLeft(btnQUIT, 150);
            Canvas.SetTop(btnQUIT, 50);
            btnQUIT.Click += btnQUIT_Click;

            // Save
            btnSAVE = new Button { Content = "SAVE", Width = 150, Height = 50 };
            Canvas.SetLeft(btnSAVE, 350);
            Canvas.SetTop(btnSAVE, 50);
            btnSAVE.Click += btnSAVE_Click;

            // Load ?
        }

        private void btnQUIT_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSAVE_Click(object sender, RoutedEventArgs e)
        {
            cltr.Save("SaveData.txt");
        }

        public void Draw_Load()
        {
            string imgname = "";
            foreach (Entity ship in cltr.current_Enemies)
            {

                if (ship is Asteroid)
                { imgname = "asteroid.png"; }
                else if (ship is AI)
                { imgname = "Ship 1.png"; }
                else if (ship is Bullet)
                { imgname = "C_bullet.png"; }
                if (ship != null)
                {
                    Image img = new Image() { Source = new BitmapImage(new Uri("images/" + imgname, UriKind.Relative)) };
                    img.Width = ship.hitbox.Width;
                    img.Height = ship.hitbox.Height;
                    WorldCanvas.Children.Add(img);
                    icons.Add(new Icon() { i = img, e = ship });
                }
            }
            foreach (Entity b in cltr.player_fire)
            {
                Image img = new Image() { Source = new BitmapImage(new Uri("images/" + "P_bullet", UriKind.Relative)) };
                img.Width = b.hitbox.Width;
                img.Height = b.hitbox.Height;
                WorldCanvas.Children.Add(img);
                icons.Add(new Icon() { i = img, e = b });
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

        private void Window_Closed(object sender, EventArgs e)
        {
            timer.Stop();
        }


        public enum Id { player, computer }

        public void MakeBullet(Id id, Entity ship)
        {
            Entity p = ship;
            if (id == Id.player)
            {
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
            else
            {
                double y = ship.Y + 10;
                double x = ship.X - 2;
                Bullet b = new Bullet(x, y) {direction = -1 };
                cltr.current_Enemies.Add(b);
                Image img = new Image() { Source = new BitmapImage(new Uri("images/" + "C_bullet.png", UriKind.Relative)) };
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
            List<Entity> fired = cltr.UpdateWorld();           // Update the Model. fired: list of ships that fired 
            SpawnEntities();              // Spawn Entities

            Console.WriteLine("GAME IS STILL RUNNING");
            if (cltr.player.FiredABullet == true)
            {
                MakeBullet(Id.player,cltr.player);
                cltr.player.FiredABullet = false;
            }

            foreach (Entity ship in fired)
                MakeBullet(Id.computer, ship);
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
                Entity newEntity = Levels.Level_reuturnEntity(cltr.difficulty, cltr.level);
                cltr.current_Enemies.Add(newEntity); // Add to Model
                
                string pngName = "";

                if (newEntity is Asteroid)
                {
                    pngName = "asteroid.png";
                }
                else if (newEntity is Formation && newEntity.Flightpath == pattern.Sin)
                {
                    pngName = "Ship 2.png";
                }
                else if (newEntity is Formation && newEntity.Flightpath == pattern.Cos)
                {
                    pngName = "Ship 3.png";
                }
                else if (newEntity is Mine)
                {
                    pngName = "mine.png";
                }
                else if (newEntity is Tracker)
                {
                    pngName = "Ship 4.png";
                }
                else if (newEntity is AI)
                {
                    pngName = "Ship 1.png";
                }

                Image img = new Image() { Source = new BitmapImage(new Uri("images/" + pngName, UriKind.Relative)) };
                WorldCanvas.Children.Add(img);
                //img.Width = 50;


                img.Width = newEntity.hitbox.Width;
                img.Height = newEntity.hitbox.Height; //image is same size as hitbox

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
                        WorldCanvas.Children.Add(btnQUIT);
                        WorldCanvas.Children.Add(btnSAVE);

                        isPaused = true;
                    }
                    else
                    {
                        timer.Start();
                        WorldCanvas.Children.Remove(btnQUIT);
                        WorldCanvas.Children.Remove(btnSAVE);

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
                    //  Moved to Pause Menu
                    //cltr.Save("SaveData.txt"); //added by JOANNA
                    //also, would ctrl be a better name though?
                    break;

                default:
                    break;
            }
        }

     

    
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
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
