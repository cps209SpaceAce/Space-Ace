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
using System.Media;

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

                    if (e is Player)
                    {
                        Player p = e as Player;
                        if (p.HitCoolDown % 5 > 0)
                        {
                            i.Source = null;
                        }
                        else
                        {
                            i.Source = new BitmapImage(new Uri("images/" + p.image, UriKind.Relative));
                        }

                    }

                    return true;
                }
            }
            return false;
         
        }
    }

    

    public partial class GameWindow : Window
    {
        public List<Icon> icons = new List<SpaceAce.Icon>();

        public GameController gameCtrl;

        public List<Image> images = new List<Image>();

        public int spawnCounter = 0;

        public bool isPaused = false;
        Button btnQUIT;
        Button btnSAVE;
        public DispatcherTimer timer;
        public SoundManager soundPlayer;


        public GameWindow(Difficulty setDiff, bool isLoad) //Joanna: isLoad checks whether to load game or start new one
        {
            InitializeComponent();
            CanvasBorder.BorderThickness = new Thickness(2);
            // Load from levels
            gameCtrl = new GameController(setDiff, Width, Height);

            if (isLoad)
            {
                gameCtrl.Load("SaveData.txt");
                Draw_Load();
            }

            // Quit Button
            btnQUIT = new Button { Content = "QUIT", Width = 150, Height = 50 };
            Canvas.SetLeft(btnQUIT, 150);
            Canvas.SetTop(btnQUIT, 50);
            btnQUIT.Click += btnQUIT_Click;

            // Save
            btnSAVE = new Button { Content = "SAVE & QUIT", Width = 150, Height = 50 };
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
            gameCtrl.Save("SaveData.txt");
            this.Close();
        }

        public void Draw_Load()
        {
            string imgname = "";
            foreach (Entity ship in gameCtrl.current_Enemies)
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
            foreach (Entity b in gameCtrl.player_fire)
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
            icons.Add(new Icon() { i = img, e = gameCtrl.player });

            soundPlayer = new SoundManager();



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
                soundPlayer.PlayNoise(SoundType.Shoot1);
                double y = p.Y + 10;
                double x = p.X + 50;
                Bullet b = new Bullet(x, y);
                gameCtrl.player_fire.Add(b);
                Image img = new Image() { Source = new BitmapImage(new Uri("images/" + "P_bullet.png", UriKind.Relative)) };
                img.Width = 20;
                Icon i = new Icon() { i = img, e = b };
                i.update();
                WorldCanvas.Children.Add(img);
                icons.Add(i);
            }
            else
            {
                soundPlayer.PlayNoise(SoundType.Shoot2);
                double y = ship.Y + 10;
                double x = ship.X - 2;
                Bullet b = new Bullet(x, y) {direction = -1 };
                gameCtrl.current_Enemies.Add(b);
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
            gameCtrl.gameLevelTimer += 0.01;
            // Check if power up is being used
            gameCtrl.player.powerUpCounter += 0.01;
            gameCtrl.spawnPowerUpTimer += 0.01;

            List<Icon> dead = new List<Icon>();            

            gameCtrl.player.UpdatePosition(); // Update the Player Positions
            List<Entity> fired = gameCtrl.UpdateWorld();           // Update the Model. fired: list of ships that fired 

            
            CheckGameStatus();
            SpawnEntities();              // Spawn Entities
            SpawnPowerUp();

            // ---- Update New Bullets ---- //
            if (gameCtrl.player.FiredABullet == true)
            {
                MakeBullet(Id.player,gameCtrl.player);
                gameCtrl.player.FiredABullet = false;
            }

            foreach (Entity ship in fired)
                MakeBullet(Id.computer, ship);


            // ---- Update GUI ---- //
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
            labelScore.Content = "Score: " + gameCtrl.score;
            
            
            // Update Lives
            // TODO: We can change to images for bonus
            labelLives.Content = "Lives: " + String.Concat(Enumerable.Repeat("< ", gameCtrl.player.lives));
            labelBombs.Content = "Bombs: " + gameCtrl.player.bombs;
            labelLevel.Content = gameCtrl.level.ToString().Replace("Level_","LEVEL ");
        }

        private void SpawnPowerUp()
        {
            if(gameCtrl.spawnPowerUpTimer > 5)
            {

                Entity newEntity = Levels.Level_returnPowerUp();
                gameCtrl.current_Enemies.Add(newEntity);
                string pngName = "";
                switch((newEntity as Powerup).type)
                {
                    case PowerUp.Invinsible:
                        pngName = "shield.png";
                        break;
                    case PowerUp.Power:
                        pngName = "star.png";
                        break;
                    case PowerUp.ExtraLife:
                        pngName = "life.png";
                        break;
                }


                Image img = new Image() { Source = new BitmapImage(new Uri("Images/PowerUp/" + pngName, UriKind.Relative)) };
                WorldCanvas.Children.Add(img);
                img.Width = newEntity.hitbox.Width - 10;
                img.Height = newEntity.hitbox.Height - 10; //image is same size as hitbox

                Canvas.SetLeft(img, 0);
                Canvas.SetTop(img, 0);
                icons.Add(new Icon() { i = img, e = gameCtrl.current_Enemies[gameCtrl.current_Enemies.Count - 1] });
                gameCtrl.spawnPowerUpTimer = 0;
            }
        }

        private void CheckGameStatus()
        {

            if (gameCtrl.gameResult != GameResult.Running || gameCtrl.gameLevelTimer > 65) // IF WON/LOST
            {

                gameCtrl.score += gameCtrl.player.bombs * 250;
                gameCtrl.score += gameCtrl.player.lives * 300;
                switch (gameCtrl.difficulty)
                {
                    case Difficulty.Easy:
                        break;
                    case Difficulty.Medium:
                        gameCtrl.score = Convert.ToInt32(gameCtrl.score * 1.2);
                        break;
                    case Difficulty.Hard:
                        gameCtrl.score = Convert.ToInt32(gameCtrl.score * 1.5);
                        break;
                }
                if (gameCtrl.gameResult == GameResult.Won)
                    gameCtrl.score += 1000;

                AddScoreWindow addScoreWindow = new AddScoreWindow(gameCtrl); // Need to pass score
                addScoreWindow.Show();
                this.Close(); // Closing GameWindow

            }
            else if (gameCtrl.gameLevelTimer > 60)
            {
                gameCtrl.level = Level.Boss;
            }
            else if (gameCtrl.gameLevelTimer > 30)
            {
                gameCtrl.level = Level.Level_2;
            }
        }



        private void SpawnEntities()
        {

            if (spawnCounter > 25)
            {
                spawnCounter = 0;
                Entity newEntity = Levels.Level_reuturnEntity(gameCtrl.difficulty, gameCtrl.level);
                gameCtrl.current_Enemies.Add(newEntity); // Add to Model
                
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
                icons.Add(new Icon() { i = img, e = gameCtrl.current_Enemies[gameCtrl.current_Enemies.Count - 1] });
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
                    gameCtrl.left = true;
                    break;
                case Key.Right:
                    gameCtrl.right = true;
                    break;
                case Key.Up:
                    gameCtrl.up = true;
                    break;
                case Key.Down:
                    gameCtrl.down = true;
                    break;
                case Key.Space:
                    gameCtrl.fired = true;
                    break;
                case Key.B:
                    gameCtrl.bomb = true;
                    break;
                case Key.X:
                    gameCtrl.player.Activate_powerup();
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
                    gameCtrl.left = false;
                    break;
                case Key.Right:
                    gameCtrl.right = false;
                    break;
                case Key.Up:
                    gameCtrl.up = false;
                    break;
                case Key.Down:
                    gameCtrl.down = false;
                    break;
                case Key.Space:
                    gameCtrl.fired = false;
                    break;
                case Key.B:
                    gameCtrl.bomb = false;
                    break;
                default:
                    break;
            }
        }

        
    }
}
