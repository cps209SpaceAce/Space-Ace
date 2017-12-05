using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;
using System.Windows;

namespace Model
{

    // game level difficulty state
    public enum Difficulty
    {
        Easy, Medium, Hard
    }
    //game level state
    public enum Level
    {
        Level_1, Level_2, Boss, Transition
    }

    // saving 
    interface ISerialiable
    {
        string Serialize();
    }

    //end state of game
    public enum GameResult
    {
        Running, Won, Lost
    }


    //class to control all game logic
    public class GameController
    {
        //player actions
        public static Random random = new Random();
        public bool fired = false;
        public bool bomb = false;
        public bool up = false;
        public bool down = false;
        public bool left = false;
        public bool right = false;


        //lists of moving objects
        public List<Entity> current_Enemies = new List<Entity>();
        public List<Entity> enemie_Que = new List<Entity>();
        public Player player;
        public List<Bullet> player_fire = new List<Bullet>();
        public GameResult gameResult;
        //pre-game setup
        public Level level;
        public Difficulty difficulty;
        public double base_Speed;
        public int score;
        public double gameLevelTimer;
        public double spawnPowerUpTimer;
        public bool BossIsSpawned = false;

        //window information
        public double winWidth;
        public double winHeight;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="passDiff"> game difficulty</param>
        /// <param name="windowWidth"> size of window</param>
        /// <param name="windowHeight">size of window</param>
        /// <param name="isCheating"> is player invincible</param>
        /// <param name="shipIMG"> image of player ship</param>
        public GameController(Difficulty passDiff, double windowWidth, double windowHeight, bool isCheating, string shipIMG)
        {
            player = new Player(50, 350, 3, 3, this, shipIMG);
            player.cheating = isCheating;
            difficulty = passDiff;

            winWidth = windowWidth;
            winHeight = windowHeight;
        }


        //basic setup constructor
        public GameController()
        {
            player = new Player(50, 350, 3, 3, this, "ship.png");
        }


        //moves every object on the screen
        public List<Entity> UpdateWorld()// Each tick of timer will call this.
        {
            List<Entity> ships_that_fired = new List<Entity>(); //does not include player
            List<Entity> leftscreen = new List<Entity>(); // list to keep track  of  when entitys leave the playable screen
            // Update each entity

            foreach (Entity ent in current_Enemies)
            {
                if (ent != null)
                {
                    if (ent is Tracker)
                        (ent as Tracker).RecieveTrackerData(player.X, player.Y, winWidth / 2 + winWidth / 4);

                    if (ent is Mine)
                        (ent as Mine).RecieveTrackerData(player.X, player.Y);
                    if (ent is Boss)
                        (ent as Boss).RecieveTrackerData(player.X, player.Y);

                    ent.UpdatePosition();

                    if (ent is Boss)
                    {
                        if ((ent as Boss).action)
                        {
                            (ent as Boss).action = false;
                            ships_that_fired.Add(ent);
                        }
                    }

                    else
                    {
                        if (ent.FiredABullet)
                            ships_that_fired.Add(ent);
                        if (!ent.alive)
                            leftscreen.Add(ent);
                    }
                }
            }

            //update player bullet position
            foreach (Entity playerBullet in player_fire)
            {
                if (playerBullet != null)
                    playerBullet.UpdatePosition();
                if (!playerBullet.alive)
                    leftscreen.Add(playerBullet);
            }


            player.UpdatePosition();//actionMove, other

            foreach (Entity ship in leftscreen)
            {
                ship.alive = false;
                current_Enemies.Remove(ship);
            }
            // loop through entites and detect collision
            DetectColl();

            
            return ships_that_fired;
        }

        /// <summary>
        /// checks if any object has collided with the player or player bullets
        /// </summary>
        public void DetectColl()
        {
            List<Entity> dead_Badguy = new List<Entity>();
            List<Entity> dead_playerBullet = new List<Entity>();
            //loop through
            //Entities vs. player
            foreach (Entity enemy in current_Enemies)
            {
                if (enemy.hitbox.IntersectsWith(player.hitbox))
                {
                    if (player.HitPlayer(enemy))
                    {
                        var sound = new MediaPlayer();
                        sound.Open(new Uri(System.Environment.CurrentDirectory.Substring(0, System.Environment.CurrentDirectory.Length - 9) + "Resources\\hurtplayer.wav", UriKind.Absolute));
                        Application.Current.Dispatcher.BeginInvoke(new Action(() => sound.Play()));
                    }
                    if (!(enemy is Boss))
                    {
                        if (enemy.Hit())
                            dead_Badguy.Add(enemy);
                    }
                    if (enemy is Powerup)
                    {
                        player.powerup = (enemy as Powerup).type; // Added by Jo

                        //if ((enemy as Powerup).type == PowerUp.ExtraLife ||
                        //    (enemy as Powerup).type == PowerUp.ExtraBomb )
                        player.Activate_Powerup();

                        dead_Badguy.Add(enemy);
                    }
                }
            }
            //Player Bullets vs. entities
            foreach (Entity bullet in player_fire)
            {
                foreach (Entity enemy in current_Enemies)
                {
                    if (bullet.hitbox.IntersectsWith(enemy.hitbox))
                    {
                        dead_playerBullet.Add(bullet);
                        bullet.Hit();
                        if (enemy.Hit())
                        {
                            var sound = new MediaPlayer();
                            sound.Open(new Uri(System.Environment.CurrentDirectory.Substring(0, System.Environment.CurrentDirectory.Length - 9) + "Resources\\damage.wav", UriKind.Absolute));
                            Application.Current.Dispatcher.BeginInvoke(new Action(() => sound.Play()));

                            dead_Badguy.Add(enemy);
                            score += 50 * 1;
                            if (enemy is Powerup)
                            {
                                player.powerup = (enemy as Powerup).type;

                                player.Activate_Powerup();
                            }
                        }

                    }
                }
            }
            foreach (Entity e in dead_Badguy)
                current_Enemies.Remove(e);

            foreach (Bullet b in dead_playerBullet)
                player_fire.Remove(b);
        }

        //------------- Bomb logic -------------//
        /// <summary>
        /// Destroys all enemys
        /// also resets the current enemies
        /// does only one damge to boss
        /// </summary>
        public void Bomb()
        {
            Entity b = null;
            var sound = new MediaPlayer();
            sound.Open(new Uri(System.Environment.CurrentDirectory.Substring(0, System.Environment.CurrentDirectory.Length - 9) + "Resources\\bomb.wav", UriKind.Absolute));
            Application.Current.Dispatcher.BeginInvoke(new Action(() => sound.Play()));

            foreach (Entity e in current_Enemies)
            {

                this.score += 50;
                e.alive = false;
                if (e is Boss)
                {
                    e.alive = true;
                    if (!e.Hit())
                        b = e;
                }
            }


            current_Enemies = new List<Entity>();
            if (b != null)
                current_Enemies.Add(b);
        }

        //-----------  Load - Save  ------------//

        //Save method that takes in a file name and saves the current state of the game (Defaults,
        //Player, Enemies, etc...) into specified file.
        public void Save(string fileName)
        {
            File.WriteAllText(fileName, String.Empty);

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine("[defaults]");
                writer.WriteLine(level + "," + score + "," + base_Speed + "," + gameLevelTimer + "," + BossIsSpawned); //add timers
                writer.WriteLine("[end]");

                if (player != null)
                {
                    writer.WriteLine("[player]");
                    writer.WriteLine(player.Serialize());
                    writer.WriteLine("[end]");
                }

                if (player_fire != null && player_fire.Count > 0)
                {
                    writer.WriteLine("[playerBullets]");
                    for (int i = 0; i < player_fire.Count; ++i)
                        writer.WriteLine(player_fire[i].Serialize());
                    writer.WriteLine("[end]");
                }

                if (current_Enemies != null && current_Enemies.Count > 0)
                {
                    writer.WriteLine("[enemies]");
                    for (int i = 0; i < current_Enemies.Count; ++i)
                        writer.WriteLine(current_Enemies[i].Serialize());
                    writer.WriteLine("[end]");
                }


            }
        }

        //Method that loads game state from specified file name. 
        //(Defaults, Player, Enemies, etc...)
        public void Load(string fileName)
        {
            enemie_Que.Clear();
            current_Enemies.Clear();
            player_fire.Clear();

            if (!File.Exists(fileName))
            {
                using (File.Create(fileName))
                    return;
            }


            using (StreamReader reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    string startLine = reader.ReadLine();
                    if (startLine == "[defaults]")
                    {
                        string[] res = reader.ReadLine().Split(',');

                        if (res[0] == "Level_1")
                            level = Level.Level_1;
                        else if (res[0] == "Level_2")
                            level = Level.Level_2;
                        else if (res[0] == "Boss")
                            level = Level.Boss;

                        score = Convert.ToInt32(res[1]);
                        base_Speed = Convert.ToDouble(res[2]);
                        gameLevelTimer = Convert.ToDouble(res[3]);

                        BossIsSpawned = Convert.ToBoolean(res[4]);
                        reader.ReadLine(); 
                    }
                    else if (startLine == "[player]")
                    {
                        string line = reader.ReadLine();
                        player = Entity.Deserialize(line, "player", this) as Player;
                        reader.ReadLine(); 
                    }

                    else if (startLine == "[playerBullets]")
                    {
                        List<Bullet> list = new List<Bullet>();

                        string line = reader.ReadLine();
                        while (line != "[end]")
                        {
                            list.Add(Entity.Deserialize(line, "playerBullet", this) as Bullet);
                            line = reader.ReadLine();
                        }

                        player_fire = list;
                    }
                    else if (startLine == "[enemies]")
                    {
                        List<Entity> list = new List<Entity>();

                        string line = reader.ReadLine();
                        while (line != "[end]")
                        {
                            list.Add(Entity.Deserialize(line, "enemy", this));
                            line = reader.ReadLine();
                        }

                        current_Enemies = list;
                    }
                    else throw new Exception("Enemy type Unknown.");

                }
            }
        }
    }
}
