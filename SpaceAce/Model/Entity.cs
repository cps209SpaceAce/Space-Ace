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
         

        public double speed;
        //health auto set to 1
        public int health;
        public bool FiredABullet = false;

        public Rectangle hitbox;
        public double X;
        public double Y;

        public Entity(double X, double Y)
        {
            this.health = 1;
            this.X = X;
            this.Y = Y;
            this.speed = 4;
            this.hitbox = new Rectangle(Convert.ToInt32(X),Convert.ToInt32(Y),50,50);
        }
        
        //return true if destroyed else return false
        public abstract bool Hit();

        public abstract void UpdatePosition();

        public abstract string Serialize();

        //implement a factory method deserialize for every entity
    }

    public class Powerup:Entity
    {
        public Powerup(double X, double Y, string name) :base(X,Y) { }
        public override void UpdatePosition()
        {
            throw new NotImplementedException();
        }
        public override bool Hit()
        {
            throw new NotImplementedException();
        }

        public override string Serialize()
        {
            return "powerup" + "," + X + "," + Y; 
            //please provide a way to detect the powerup's type
        }

        public static Powerup Deserialize(string code)
        {
            return null;
        }
    }
    

    public class Asteroid : Entity
    {
        public Asteroid(double X, double Y) : base(X,Y)
        { }
        public override bool Hit()
        { 
            //Asteroid can't be destroyed
            return false;
        }
        public override void UpdatePosition()
        {
            //TODO: add movment logic: make it move in a straight line
            X = Convert.ToInt32(X - (1 * speed));
            // Why are we returning a point?
            
        }

        public override string Serialize()
        {
            return "asteroid" + "," + health + "," + X + "," + Y;
        }

        public static Entity Deserialize(string code)
        {
            return null;
        }
    }

   


   


}
