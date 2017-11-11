using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum powerup {Power}
    
    public class Player: Entity

    {
        public int cooldown = 0;
        public int lives { get; set; }
        public int bombs { get; set; }
        public powerup powerup { get; set; }
        

        private GameController game;

        public Player(double X, double Y, int lives, int bombs, GameController flags):base(X,Y)
        {
            game = flags;
            this.lives = lives;
            this.bombs = bombs;
            speed = 5;
            //TODO: setup player
        }

        public void Fire()
        {
            if (cooldown == 0)
            {
                FiredABullet = true;
                cooldown = 50; // Rate of fire: bigger = slower.
            }
            //TODO: create a bullet object (with id = friendly) at a point infront of the player
            // with a heading towards th right
            
        }

        public void Activate_powerup()
        {
            //TODO: activate powerup
        }
        public void DropBomb()
        {
            //TODO: damage all badguys
        }

        public override bool Hit()
        {
            //TODO: remove one life(ship destroyed)
            //TODO: return true(ship destroyed)
            throw new NotImplementedException();
        }

        public void Up()
        {
            Y = Convert.ToInt32(Y - (1 * speed));
            
            //TODO: move player up
        }

        public void Down()
        {
            Y = Convert.ToInt32(Y + (1 * speed));
           
            //TODO: move player down
        }

        public void Left()
        {
            X = Convert.ToInt32(X - (1 * speed));
            
            //TODO: move player left
        }

        public void Right()
        {
            X = Convert.ToInt32(X + (1 * speed));
            //TODO: move player right
        }

        //handles when the player is idle
        public override void UpdatePosition()
        {
            if(cooldown > 0)
            cooldown--;

            if (game.up)
            {
                if (this.Y > 0)
                    Up();
            }
            if (game.down)
            {
                if (this.Y < 768 - 50)
                    Down();
            }
            if (game.left)
            {
                if (this.X > 0)
                    Left();
            }
            if (game.right)
            {
                if (this.X < 1024 - 50)
                    Right();
            }
            if (game.fired)
            {
                Fire();
            }
            if (game.bomb)
            {
                DropBomb();
            }
            
        }
        public override string Serialize()
        {
            return X + "," + Y + "," + powerup + "," + lives + "," + bombs;
        }

        public static Entity Deserialize(string code, GameController game)
        {
            string[] des = code.Split(',');
            Player p = new Player(new Point(Convert.ToInt32( des[0]), Convert.ToInt32(des[1])), Convert.ToInt32(des[3]), Convert.ToInt32(des[4]), game);
            //if()
            return null;
        }
    }
}
