using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

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

        //pre-game setup
        public Level level;
        public Difficulty difficulty;

        public double base_Speed;
        public int score;

        public GameController(Difficulty passDiff)
        {
            player = new Player(50, 350, 3, 3, this);// Flags?
            //TODO: load level/save data from GameData
            // OR
            //TODO: get enemies for level from Level
            enemie_Que = Levels.Level1();
            difficulty = passDiff;

        }

        //written by Joanna, i need a blank slate for testing, 
        //no need to use this for anything else though
        public GameController()
        {
            player = new Player(50, 350, 3, 3, this);
            enemie_Que = new List<Entity>();
        }

        public void UpdateWorld()// Each tick of timer will call this.
        {

            // Update each entity

            foreach (Entity ent in current_Enemies)
            {
                if (ent != null)
                    ent.UpdatePosition();
            }


            foreach (Entity playerBullet in player_fire)
            {
                if (playerBullet != null)
                    playerBullet.UpdatePosition();
            }


            player.UpdatePosition();//actionMove, other


            // loop through entites and detect collision
            // DetectColl(); 

            // Check if death
        }

        public void DetectColl()
        {
            // loop through
            // Entities vs. player
            foreach (Entity enemy in current_Enemies)
            {
                if (enemy.hitbox.IntersectsWith(player.hitbox))
                {
                    player.Hit();
                }
            }
            // Player Bullets vs. entities
            foreach (Entity bullet in player_fire)
            {
                foreach (Entity enemy in current_Enemies)
                {
                    if (bullet.hitbox.IntersectsWith(enemy.hitbox))
                    {
                        enemy.Hit();
                    }
                }
            }
        }

        //-----------  Load - Save  ------------//

        public void Save(string fileName)
        {
            File.WriteAllText(fileName, String.Empty);

            using (StreamWriter writer = new StreamWriter(fileName))
            {

                if (current_Enemies != null && current_Enemies.Count > 0)
                {
                    writer.WriteLine("[enemies]");
                    for (int i = 0; i < current_Enemies.Count; ++i)
                        writer.WriteLine(current_Enemies[i].Serialize());
                    writer.WriteLine("[end]");
                }

                if (enemie_Que != null && enemie_Que.Count > 0)
                {
                    writer.WriteLine("[queuedEnemies]");
                    for (int i = 0; i < enemie_Que.Count; i++)
                        writer.WriteLine(enemie_Que[i].Serialize());
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

                writer.WriteLine("[defaults]");
                writer.WriteLine(level + "," + score + "," + base_Speed);
                writer.WriteLine("[end]");

            }
        }
        public void Load(string fileName)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    string startLine = reader.ReadLine();

                    if (startLine == "[enemies]")
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

                    else if (startLine == "[queuedEnemies]")
                    {
                        List<Entity> list = new List<Entity>();

                        string line = reader.ReadLine();
                        while (line != "[end]")
                        {
                            list.Add(Entity.Deserialize(line, "enemy", this));
                            line = reader.ReadLine();
                        }

                        enemie_Que = list;
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

                    else if (startLine == "[defaults]")
                    {
                        //2,10,1337
                        string[] res = reader.ReadLine().Split(',');

                        if (res[0] == "Level_1")
                            level = Level.Level_1;
                        else if (res[0] == "Level_2")
                            level = Level.Level_2;
                        else if (res[0] == "Boss")
                            level = Level.Boss;

                        score = Convert.ToInt32(res[1]);
                        base_Speed = Convert.ToInt32(res[2]);
                    }
                }
            }
        }

    }
}
