using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace Model
{
    public enum PlayerAction
    {
        Up, Down, Left, Right, Fire, Bomb, Powerup
    }
    public enum Difficulty
    {
        Easy, Medium, Hard
    }
    public enum Level
    {
        Level_1, Level_2, Boss
    }

    interface ISerialiable
    {
        string Serialize();
    }
    public enum GameResult
    {
        Running, Won, Lost
    }

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
        // add a boss var
        public List<Entity> current_Enemies = new List<Entity>();
        public List<Entity> enemie_Que = new List<Entity>();
        public Player player;
        public List<Bullet> player_fire = new List<Bullet>();
        public GameResult gameResult;
        //pre-game setup
        public Level level;
        public Difficulty difficulty;
        public double base_Speed; //does anyone use basespeed anymore?
        public int score;
        public double gameLevelTimer;
        public double spawnPowerUpTimer;


        //window information
        public double winWidth;
        public double winHeight;
        public SoundManager soundPlayer;

        public GameController(Difficulty passDiff, double windowWidth, double windowHeight, bool isCheating, string shipIMG)
        {
            player = new Player(50, 350, 3, 3, this, shipIMG);// Flags?
            player.cheating = isCheating;
            //TODO: load level/save data from GameData
            // OR
            //TODO: get enemies for level from Level
            difficulty = passDiff;

            winWidth = windowWidth;
            winHeight = windowHeight;
            soundPlayer = new SoundManager();
        }

        //written by Joanna, i need a blank slate for testing, 
        //no need to use this for anything else though
        public GameController()
        {
            player = new Player(50, 350, 3, 3, this,"ship_image");
        }



        public List<Entity> UpdateWorld()// Each tick of timer will call this.
        {
            List<Entity> ships_that_fired = new List<Entity>(); //does not include player
            List<Entity> leftscreen = new List<Entity>(); // list to keep track  of  when entitys leave the playable screen
            // Update each entity

            foreach (Entity ent in current_Enemies)
            {
                if (ent != null)
                {
                    //added by Joanna
                    if (ent is Tracker)
                        (ent as Tracker).RecieveTrackerData(player.X, player.Y, winWidth / 2 + winWidth / 4); //the 4th quarter of the window

                    if (ent is Mine)
                        (ent as Mine).RecieveTrackerData(player.X, player.Y); //the 4th quarter of the window
                    if (ent is Boss)
                        (ent as Boss).RecieveTrackerData(player.X, player.Y); //the 4th quarter of the window

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

            // Check if death
            return ships_that_fired;
        }

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

                        soundPlayer.PlayNoise(SoundType.HurtPlayer);
                    }
                    if (!(enemy is Boss))
                    {
                        if (enemy.Hit())
                            dead_Badguy.Add(enemy);
                    }
                    if (enemy is Powerup)
                    {
                        player.powerup = (enemy as Powerup).type; // Added by Jo

                        if ((enemy as Powerup).type == PowerUp.ExtraLife ||
                            (enemy as Powerup).type == PowerUp.ExtraBomb )
                            player.Activate_powerup();

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
                            soundPlayer.PlayNoise(SoundType.HurtEnemy);
                            dead_Badguy.Add(enemy);
                            score += 50 * 1; // TODO: Based on DIff
                            if (enemy is Powerup)
                            {
                                player.powerup = (enemy as Powerup).type; // Added by Jo // copyed Noah
                                if ((enemy as Powerup).type == PowerUp.ExtraLife)
                                    player.Activate_powerup();
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
        /// Destroys all enemys: bombing asteroids is currently broken
        /// also resets the current enemies
        /// </summary>
        public void Bomb()
        {

            Entity b = null;
            soundPlayer.PlayNoise(SoundType.Bomb);
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

        public void Save(string fileName)
        {
            File.WriteAllText(fileName, String.Empty);

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine("[defaults]");
                writer.WriteLine(level + "," + score + "," + base_Speed + "," + gameLevelTimer); //add timers
                writer.WriteLine("[end]");

                if (current_Enemies != null && current_Enemies.Count > 0)
                {
                    writer.WriteLine("[enemies]");
                    for (int i = 0; i < current_Enemies.Count; ++i)
                        writer.WriteLine(current_Enemies[i].Serialize());
                    writer.WriteLine("[end]");
                }

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

            }
        }
        public void Load(string fileName)
        {
            enemie_Que.Clear();
            current_Enemies.Clear();
            player_fire.Clear();

            if (!File.Exists(fileName)) {
                using (File.Create(fileName)) ;
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

                        else if (startLine == "[player]")
                        {
                            string line = reader.ReadLine();
                            player = Entity.Deserialize(line, "player", this) as Player;
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
                    }
                }
        }
    }
}
