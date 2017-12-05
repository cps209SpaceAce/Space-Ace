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

/// <summary>
/// Window for the main game
/// Controls the general game GUI
/// </summary>

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
                            i.Source = new BitmapImage(new Uri("Images/PlayerShips/" + p.image, UriKind.Relative));
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
        public List<Icon> icons = new List<SpaceAce.Icon>(); // list of images and corresponding objects used to keep images up to date
        public GameController gameCtrl;                      // Main GameController for Game
        public int spawnCounter = 0;                         // Timer for spawning entitys
        public bool isPaused = false;                        // Value for pause menu
        Button btnQUIT;                                      // Button for Quit
        Button btnSAVE;                                      // Button for Quit and Save
        public DispatcherTimer timer;                        // Main timer for updating the game
        MediaPlayer gameMusic;                               // MediaPlayer for Game Background Music
        public bool BossIsSpawned = false;                   // Value for showing whether Boss is spawned



        public GameWindow(Difficulty setDiff, bool isLoad, bool ischeating, string shipIMG)
        {
            InitializeComponent();
            CanvasBorder.BorderThickness = new Thickness(2);
            gameCtrl = new GameController(setDiff, Width, Height, ischeating, shipIMG);
            
            if (isLoad)
            {
                gameCtrl.Load("SaveData.txt");
                if (gameCtrl.level == Level.Boss)
                {
                    gameCtrl.BossIsSpawned = true;
                    BossIsSpawned = true;
                }        
                Draw_Load();
            }
            
            if(gameCtrl.level == Level.Boss)
            {
                gameMusic = new MediaPlayer();
                gameMusic.Open(new Uri(System.Environment.CurrentDirectory + "/Resources/BossMusic.wav", UriKind.Absolute));
                Application.Current.Dispatcher.BeginInvoke(new Action(() => gameMusic.Play()));
            }
            else
            {
                gameMusic = new MediaPlayer();
                gameMusic.Open(new Uri(System.Environment.CurrentDirectory + "/Resources/GameMusic.wav", UriKind.Absolute));
                Application.Current.Dispatcher.BeginInvoke(new Action(() => gameMusic.Play()));
            }

            // Quit Button
            btnQUIT = new Button { Content = "QUIT", Width = 150, Height = 50 };
            Canvas.SetLeft(btnQUIT, 150);
            Canvas.SetTop(btnQUIT, 50);
            btnQUIT.Click += btnQUIT_Click;

            // Save & Quit
            btnSAVE = new Button { Content = "SAVE & QUIT", Width = 150, Height = 50 };
            Canvas.SetLeft(btnSAVE, 350);
            Canvas.SetTop(btnSAVE, 50);
            btnSAVE.Click += btnSAVE_Click;

        }
        // Closes the Game Window
        private void btnQUIT_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Saves the Gmae & Closes the Game Windo
        private void btnSAVE_Click(object sender, RoutedEventArgs e)
        {
            gameCtrl.Save("SaveData.txt");
            this.Close();
        }

        // ReCreates the GameController on a Load
        public void Draw_Load()
        {
            string imgname = "";
            foreach (Entity ship in gameCtrl.current_Enemies)
            {
                if (ship is Asteroid)
                {
                    imgname = "asteroid.png";
                }
                else if (ship is AI)
                {
                    if (ship is Mine)
                    {
                        imgname = "mine.png";
                    }
                    else if (ship is Tracker)
                    {
                        imgname = "ship 4.png";
                    }
                    else if (ship is Formation)
                    {
                        if ((ship as Formation).Flightpath == pattern.Sin)
                        {
                            imgname = "Ship 2.png";
                        }
                        else if ((ship as Formation).Flightpath == pattern.Cos)
                        {
                            imgname = "Ship 3.png";
                        }
                    }
                    else
                    {
                        imgname = "Ship 1.png";
                    }
                }
                else if (ship is Bullet)
                {
                    imgname = "C_bullet.png";
                }

                else if (ship is Powerup)
                {
                    Powerup power = ship as Powerup;
                    switch (power.type)
                    {
                        case PowerUp.ExtraLife:
                            imgname = "Powerup/life.png";
                            break;
                        case PowerUp.Invincible:
                            imgname = "Powerup/star.png";
                            break;
                        case PowerUp.ExtraSpeed:
                            imgname = "Powerup/power.png";
                            break;
                        case PowerUp.Helix:
                            imgname = "Powerup/bullets3.png";
                            break;
                        case PowerUp.TripleShot:
                            imgname = "Powerup/bullets.png";
                            break;
                        case PowerUp.RapidFire:
                            imgname = "Powerup/bullets2.png";
                            break;
                        case PowerUp.ExtraBomb:
                            imgname = "Powerup/shield.png";
                            break;

                    }

                }
                else if (ship is Boss)
                {
                    if (ship is Boss_Medium)
                    { imgname = "noahShip.png"; }
                    if (ship is Boss_Easy)
                    { imgname = "robertShip.png"; }
                    if (ship is Boss_Hard)
                    { imgname = "JoannaShip.png"; }
                }
                
                
                if (ship != null)
                {
                    Image img = new Image() { Source = new BitmapImage(new Uri("Images/" + imgname, UriKind.Relative)) };
                    img.Width = ship.hitbox.Width;
                    img.Height = ship.hitbox.Height;
                    WorldCanvas.Children.Add(img);
                    icons.Add(new Icon() { i = img, e = ship });
                }
            }
            foreach (Entity b in gameCtrl.player_fire)
            {
                Image img = new Image() { Source = new BitmapImage(new Uri("images/" + "P_bullet.png", UriKind.Relative)) };
                img.Width = b.hitbox.Width;
                img.Height = b.hitbox.Height;
                WorldCanvas.Children.Add(img);
                icons.Add(new Icon() { i = img, e = b });
            }
            
        }


        
        //setup for the inital game state
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Image img = new Image() { Source = new BitmapImage(new Uri("Images/PlayerShips/" + "player1.png", UriKind.Relative)) };
            WorldCanvas.Children.Add(img);
            img.Width = 50;

            Canvas.SetLeft(img, 0);
            Canvas.SetTop(img, 0);
            icons.Add(new Icon() { i = img, e = gameCtrl.player });

            pbar_gamestatus.Minimum = 0;
            pbar_gamestatus.Maximum = 15;
            pbar_gamestatus.Value = 0;

            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.Start();
        }
        // Stops the Game Running and Music on Exit
        private void Window_Closed(object sender, EventArgs e)
        {
            gameMusic.Stop();
            timer.Stop();
        }


        public enum Id { player, computer }          // used to determen you fired the bullet

        // Creates a bullet and image and adds them to the model and gui.
        //id: who fired the bullet
        //ship enity that fired
        public void MakeBullet(Id id, Entity ship)
        {
            
            if (id == Id.player)
            {
                //if player has a power up active make the correct kind of bullet
                //else make normal bullet
                if (((Player)ship).triple)
                {
                    Make_TripleShot(ship);
                    return;
                }
                if (((Player)ship).wanderingbullets)
                {
                    Make_HelixShot(ship);
                    return;
                }

                var sound = new MediaPlayer();
                sound.Open(new Uri(System.Environment.CurrentDirectory + "/Resources/Shoot1.wav", UriKind.Absolute));
                Application.Current.Dispatcher.BeginInvoke(new Action(() => sound.Play()));

                //make bullet
                double y = ship.Y + 10;
                double x = ship.X + 50;
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

                if (ship is Boss)
                {
                    // calling correct bullet function for boss
                    Boss s = (Boss)ship;
                    if (s.fired_slanted_targeted_shot)
                        Make_Boss_slantedshot(s);
                    if (s.wall)
                        Make_bosswall(s);
                    if (s.FiredABullet)
                        Make_Boss_Bullet(s);
                    return;
                }

                var sound = new MediaPlayer();
                sound.Open(new Uri(System.Environment.CurrentDirectory + "/Resources/Shoot2.wav", UriKind.Absolute));
                Application.Current.Dispatcher.BeginInvoke(new Action(() => sound.Play()));
                //making normal bullet for enemy
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

        // Creates a bullet that follow the path of a line towards the player
        //boss: used for position data of boss and player
        public void Make_Boss_slantedshot(Boss boss)
        {
            double slope = (boss.Y+100 - boss.p_y) / (boss.X - boss.p_x);
            Bullet b = new Slanted_Bullet(boss.X, boss.Y + 100, slope) {id = ID.Hostile };
            if (boss.p_x > boss.X)
            {
                b.direction = 1;
               
            }
            else
            {
                b.direction = -1;
                (b as Slanted_Bullet).slope *= -1;
            }
            Image i = new Image() { Source = new BitmapImage(new Uri("images/" + "C_bullet.png", UriKind.Relative)), Width = 20, Height = 20 };
            WorldCanvas.Children.Add(i);
            icons.Add(new Icon() { i = i, e = b });
            gameCtrl.current_Enemies.Add(b);
        }

        // creates a wall of asteroids at player position heading right
        public void Make_bosswall(Boss ship)
        {
            double by = ship.p_y;
            by = by -100;
            for (int i = 0; i < 10; i++)
            {
                Entity a1 = new Asteroid(700, by+(i*20));
                a1.health = 2;
                a1.hitbox.Width = 20;
                a1.hitbox.Height = 20;
                gameCtrl.current_Enemies.Add(a1);
                Image img = new Image() { Source = new BitmapImage(new Uri("images/" + "asteroid.png", UriKind.Relative)) };
                img.Width = 20;
                img.Height = 20;
                WorldCanvas.Children.Add(img);
                icons.Add(new Icon() {e = a1, i = img });
            }
        }

        // creates a bullet at a specified location.
        public void Make_Boss_Bullet(Boss boss)
        {
            
            double y = boss.bullet_y;
            double x = boss.bullet_x;
            Bullet b = new Bullet(x, y);
            b.direction = -1;
            gameCtrl.current_Enemies.Add(b);
            Image img = new Image() { Source = new BitmapImage(new Uri("images/" + "C_bullet.png", UriKind.Relative)) };
            img.Width = 20;
            Icon i = new Icon() { i = img, e = b };
            i.update();
            WorldCanvas.Children.Add(img);
            icons.Add(i);
        }

        // creates two untargeted slanted bullets and one normal bullet at the player current position
        public void Make_TripleShot(Entity p)
        {

            if (p is Player)
            {
                var sound = new MediaPlayer();
                sound.Open(new Uri(System.Environment.CurrentDirectory + "/Resources/Shoot1.wav", UriKind.Absolute));
                Application.Current.Dispatcher.BeginInvoke(new Action(() => sound.Play()));

                double x = p.X + 50, y = p.Y + 10;
                //make bullets
                Slanted_Bullet up = new Slanted_Bullet(x, y, -1);
                Slanted_Bullet down = new Slanted_Bullet(x, y, 1);
                Bullet normal = new Bullet(x, y);
                up.direction = 1;
                down.direction = 1;
                normal.direction = 1;
                // images for bullets
                Image img_up = new Image() { Source = new BitmapImage(new Uri("images/" + "P_bullet.png", UriKind.Relative)) };
                Image img_down = new Image() { Source = new BitmapImage(new Uri("images/" + "P_bullet.png", UriKind.Relative)) };
                Image img_normal = new Image() { Source = new BitmapImage(new Uri("images/" + "P_bullet.png", UriKind.Relative)) };
                img_up.Width = 20;
                img_down.Width = 20;
                img_normal.Width = 20;
                // Icons to relate images and bullets
                Icon i_up = new Icon() { i = img_up, e = up };
                Icon i_down = new Icon() { i = img_down, e = down };
                Icon i_normal = new Icon() { i = img_normal, e = normal };
                //add bullets to game controller
                gameCtrl.player_fire.Add(up);
                gameCtrl.player_fire.Add(down);
                gameCtrl.player_fire.Add(normal);
                icons.Add(i_up);
                icons.Add(i_down);
                icons.Add(i_normal);
                //adding immages to the world
                WorldCanvas.Children.Add(img_up);
                WorldCanvas.Children.Add(img_down);
                WorldCanvas.Children.Add(img_normal);
                i_up.update();
                i_down.update();
                i_normal.update();
            }
        }

        // creates two bullets moving in sin and -sin patterns
        //ship: position data
        // enemies are currently unsupported
        public void Make_HelixShot(Entity ship) //broken: Noah Mansfield
        {
            if (ship is Player)
            {
                //play a sound
                var sound = new MediaPlayer();
                sound.Open(new Uri(System.Environment.CurrentDirectory + "/Resources/Shoot1.wav", UriKind.Absolute));
                Application.Current.Dispatcher.BeginInvoke(new Action(() => sound.Play()));
                
                double y = ship.Y + 10;
                double x = ship.X + 50;
                
                //make the bullets
                Wandering_Bullet b_cos = new Wandering_Bullet(x, y, pattern.Sin);
                Wandering_Bullet b_sin = new Wandering_Bullet(x, y, pattern.Sindown);
                gameCtrl.player_fire.Add(b_cos);
                gameCtrl.player_fire.Add(b_sin);

                //make the images
                Image img_cos = new Image() { Source = new BitmapImage(new Uri("images/" + "P_bullet.png", UriKind.Relative)) };
                Image img_sin = new Image() { Source = new BitmapImage(new Uri("images/" + "P_bullet.png", UriKind.Relative)) };
                img_sin.Width = 20;
                img_cos.Width = 20;

                //link the images and bullets together
                Icon i_cos = new Icon() { i = img_cos, e = b_cos };
                Icon i_sin = new Icon() { i = img_sin, e = b_sin };
                i_cos.update();
                i_sin.update();

                //add images to the world
                WorldCanvas.Children.Add(img_cos);
                WorldCanvas.Children.Add(img_sin);
                icons.Add(i_cos);
                icons.Add(i_sin);

            }
        }

        // Update World each tick
        public void Timer_Tick(object sender, EventArgs e)
        {
            gameCtrl.gameLevelTimer += 0.01;
            gameCtrl.player.powerUpCounter += 0.01;
            gameCtrl.spawnPowerUpTimer += 0.01;

            List<Icon> dead = new List<Icon>();            
            gameCtrl.player.UpdatePosition();                      // Update the Player Positions
            List<Entity> fired = gameCtrl.UpdateWorld();           // Update the Model. fired: list of ships that fired 
            

            CheckGameStatus();
            SpawnPowerUp();
            if (!gameCtrl.BossIsSpawned)
                SpawnEntities();              // Spawn Entities
                     
            

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
                if (ic.e is Boss)
                {
                    gameCtrl.gameResult = GameResult.Won;
                }
                icons.Remove(ic);
                
            }
            
            labelScore.Content = "Score: " + gameCtrl.score;
            labelLives.Content = "Lives: " + String.Concat(Enumerable.Repeat("< ", gameCtrl.player.Lives));
            labelBombs.Content = "Bombs: " + gameCtrl.player.Bombs;
            labelLevel.Content = gameCtrl.level.ToString().Replace("Level_","LEVEL ");

            if (gameCtrl.player.isInvincible)
                imgInvic.Source = new BitmapImage(new Uri("Images/PowerUp/star.png", UriKind.Relative));
            else
                imgInvic.Source = null;

            if (gameCtrl.player.triple)
                imgTriShot.Source = new BitmapImage(new Uri("Images/PowerUp/bullets.png", UriKind.Relative));
            else
                imgTriShot.Source = null;

            if (gameCtrl.player.rapid_fire)
                imgRapidShot.Source = new BitmapImage(new Uri("Images/PowerUp/bullets2.png", UriKind.Relative));
            else
                imgRapidShot.Source = null;
            
            if (gameCtrl.player.wanderingbullets)
                imgHex.Source = new BitmapImage(new Uri("Images/PowerUp/bullets3.png", UriKind.Relative));
            else
                imgHex.Source = null;

            if (gameCtrl.player.speed != 5)
                imgSpeed.Source = new BitmapImage(new Uri("Images/PowerUp/power.png", UriKind.Relative));
            else
                imgSpeed.Source = null;
        }

        // Spawns a powerup and adds it to the canvas
        private void SpawnPowerUp()
        {
            if(gameCtrl.spawnPowerUpTimer > 5)
            {

                Entity newEntity = Levels.Level_returnPowerUp();
                gameCtrl.current_Enemies.Add(newEntity);
                string pngName = "";
                switch((newEntity as Powerup).type)
                {
                    case PowerUp.Invincible:
                        pngName = "star.png";
                        break;                    
                    case PowerUp.ExtraLife:
                        pngName = "life.png";
                        break;
                    case PowerUp.ExtraBomb:
                        pngName = "shield.png";
                        break;
                    case PowerUp.ExtraSpeed:
                        pngName = "power.png";
                        break;
                    case PowerUp.RapidFire:
                        pngName = "bullets2.png";
                        break;
                    case PowerUp.TripleShot:
                        pngName = "bullets.png";
                        break;
                    case PowerUp.Helix:
                        pngName = "bullets3.png";
                        break;

                }


                Image img = new Image() { Source = new BitmapImage(new Uri("Images/PowerUp/" + pngName, UriKind.Relative)) };
                WorldCanvas.Children.Add(img);
                img.Width = newEntity.hitbox.Width;
                img.Height = newEntity.hitbox.Height; 

                Canvas.SetLeft(img, 0);
                Canvas.SetTop(img, 0);
                icons.Add(new Icon() { i = img, e = gameCtrl.current_Enemies[gameCtrl.current_Enemies.Count - 1] });
                gameCtrl.spawnPowerUpTimer = 0;
            }
        }

        // Updates the GameControllers Status based on timer
        // And ends game on Win or Lose
        private void CheckGameStatus()
        {

            if (gameCtrl.gameResult != GameResult.Running)
            {

                gameCtrl.score += gameCtrl.player.Bombs * 250;
                gameCtrl.score += gameCtrl.player.Lives * 300;
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
                gameMusic.Stop();
                AddScoreWindow addScoreWindow = new AddScoreWindow(gameCtrl); 
                addScoreWindow.Show();
                this.Close();

            }
            else if (gameCtrl.gameLevelTimer > 40)
            {
                gameCtrl.level = Level.Boss;
            }
            else if (gameCtrl.gameLevelTimer > 20 && gameCtrl.gameLevelTimer < 35)
            {
                gameCtrl.level = Level.Level_2;
            }
            else if(gameCtrl.gameLevelTimer >= 15 && gameCtrl.gameLevelTimer <= 20 ||
                    gameCtrl.gameLevelTimer >= 35 && gameCtrl.gameLevelTimer <= 40 )
            {
                gameCtrl.level = Level.Transition;
            }
            

            if (BossIsSpawned)
            {
                foreach(Entity ent in gameCtrl.current_Enemies)
                {
                    if (ent is Boss)
                        pbar_gamestatus.Value = ((ent.health * 15) / 30) + 0.1;    
                }
                
            }
            else if(gameCtrl.level == Level.Level_1)
            {
                
                pbar_gamestatus.Value = gameCtrl.gameLevelTimer;
            }
            else if (gameCtrl.level == Level.Level_2)
            {
                pbar_gamestatus.Value = gameCtrl.gameLevelTimer - 20;
            }

            else if (gameCtrl.level == Level.Transition)
            {
            }
        }


        // Spawns an Entity or Boss and adds it to canvas
        private void SpawnEntities()
        {

            if (spawnCounter > 25 && !BossIsSpawned)
            {
                spawnCounter = 0;
                Entity newEntity = Levels.Level_reuturnEntity(gameCtrl.difficulty, gameCtrl.level, gameCtrl.winWidth, gameCtrl.winHeight);
                if (newEntity == null)
                    return;
                gameCtrl.current_Enemies.Add(newEntity); // Add to Model
                
                
                string pngName = "";

                if (newEntity is Asteroid)
                {
                    pngName = "asteroid.png";
                }
                else if (newEntity is Formation)
                {

                    if((newEntity as Formation).Flightpath == pattern.Sin)
                        pngName = "Ship 2.png";
                    else if ((newEntity as Formation).Flightpath == pattern.Cos)
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
                else if(newEntity is Boss)
                {
                    gameMusic.Stop();
                    gameMusic.Open(new Uri(System.Environment.CurrentDirectory + "/Resources/BossMusic.wav", UriKind.Absolute));
                    Application.Current.Dispatcher.BeginInvoke(new Action(() => gameMusic.Play()));

                    BossIsSpawned = true;
                    gameCtrl.BossIsSpawned = true;

                    if(newEntity is Boss_Medium)
                        pngName = "noahShip.png";
                    if (newEntity is Boss_Easy)                        
                        pngName = "robertShip.png";
                    if (newEntity is Boss_Hard)
                        pngName = "JoannaShip.png";
                }
                else if (newEntity is AI)
                {
                    pngName = "Ship 1.png";
                }

              
                Image img = new Image() { Source = new BitmapImage(new Uri("images/" + pngName, UriKind.Relative)) };
                WorldCanvas.Children.Add(img);


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

        // Detects when a button is pressed and acts on it
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
                default:
                    break;
            }
        }



        // Detects when a button is released and acts on it
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
