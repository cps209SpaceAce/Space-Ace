using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Model
{
    abstract public class Entity
    {
        private Point loc;
        private double speed;
        //health auto set to 1
        private int health;
        public Entity(){  }
        private Rectangle hitbox;
        //return true if destroyed else return false
        public abstract bool Hit();

        public abstract Point UpdatePosition();
    }

    public class Powerup:Entity
    {
        public Powerup(Point p, string name) { }
        public override Point UpdatePosition()
        {
            throw new NotImplementedException();
        }
        public override bool Hit()
        {
            throw new NotImplementedException();
        }
    }
    

    public class Asteroid : Entity
    {
        public Asteroid(Point p)
        { }
        public override bool Hit()
        { 
            //Asteroid can't be destroyed
            return false;
        }
        public override Point UpdatePosition()
        {
            //TODO: add movment logic: make it move in a straight line
            throw new NotImplementedException();
        }
    }

   


   


}
