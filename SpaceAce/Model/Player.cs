using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum powerup {Power}
    public class Player:Entity
    {
        private int lives { get; set; }
        private int bombs { get; set; }
        public powerup powerup { get; set; }

        public Player(int lives, int bombs)
        {
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
        public void DropBomb(List<Entity> badguys)

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
            //TODO: move player up
        }

        public void Down()
        {
            //TODO: move player down
        }

        public void Left()
        {
            //TODO: move player left
        }

        public void Right()
        {
            //TODO: move player right
        }

        //handles when the player is idle
        override public Point UpdatePosition()
        {
            //TODO: update player position

            Point h = new Point(0, 0);//filler
            return h;
        }

    }
}
