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
        public int lives { get; set; }
        public int bombs { get; set; }
        public powerup powerup { get; set; }

        private GameController game;

        public Player(int lives, int bombs,GameController flags,int health, Point location, int speed):base(health,location,speed)
        {
            this.lives = lives;
            this.bombs = bombs;
            game = flags;
            //TODO: setup player
        }

        public Bullet Fire()
        {
            //TODO: create a bullet object (with id = friendly) at a point infront of the player
            // with a heading towards th right
            return null;
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
            loc.Y = Convert.ToInt32(loc.Y - (1 * speed));
            
            //TODO: move player up
        }

        public void Down()
        {
            loc.Y = Convert.ToInt32(loc.Y + (1 * speed));
           
            //TODO: move player down
        }

        public void Left()
        {
            loc.X = Convert.ToInt32(loc.X - (1 * speed));
            
            //TODO: move player left
        }

        public void Right()
        {
            loc.X = Convert.ToInt32(loc.X + (1 * speed));
            //TODO: move player right
        }

        //handles when the player is idle
        public override Point UpdatePosition()
        {
            if (game.up)
            {
                Up();
            }
            if (game.down)
            {
                Down();
            }
            if (game.left)
            {
                Left();
            }
            if (game.right)
            {
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
            return loc;
        }
        public override string Serialize()
        {
            return loc.X + "," + loc.Y + "," + powerup + "," + lives + "," + bombs;
        }

        public override Entity Deserialize(string code)
        {
            return null;
        }
    }
}
