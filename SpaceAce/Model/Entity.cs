using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Model
{
    abstract public class Entity: ISerialiable
    {
        public Point loc; //JOANNA: turned this to public cause private makes it unavailavble to its own children.
                          //if you really want it private, please use 'protected'

        public double speed;
        //health auto set to 1
        public int health;

        public Rectangle hitbox;

        public Entity(Point location)
        {
            this.health = 1;
            this.loc = location;
            this.speed = 1;
            this.hitbox = new Rectangle(loc.X,loc.Y,50,50);
        }
        
        //return true if destroyed else return false
        public abstract bool Hit();

        public abstract Point UpdatePosition();

        public abstract string Serialize();

        public abstract Entity Deserialize(string code);
    }

    public class Powerup:Entity
    {
        public Powerup( Point loc, string name) :base(loc) { }
        public override Point UpdatePosition()
        {
            throw new NotImplementedException();
        }
        public override bool Hit()
        {
            throw new NotImplementedException();
        }

        public override string Serialize()
        {
            return "powerup" + "," + loc.X + "," + loc.Y; 
            //please provide a way to detect the powerup's type
        }

        public override Entity Deserialize(string code)
        {
            return null;
        }
    }
    

    public class Asteroid : Entity
    {
        public Asteroid(Point loc) : base(loc)
        { }
        public override bool Hit()
        { 
            //Asteroid can't be destroyed
            return false;
        }
        public override Point UpdatePosition()
        {
            //TODO: add movment logic: make it move in a straight line
            loc.X = Convert.ToInt32(loc.X - (1 * speed));
            // Why are we returning a point?
            return loc;
        }

        public override string Serialize()
        {
            return "asteroid" + "," + loc.X + "," + loc.Y; ;
        }

        public override Entity Deserialize(string code)
        {
            return null;
        }
    }

   


   


}
