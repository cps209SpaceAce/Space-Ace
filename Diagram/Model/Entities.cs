using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Model
{
    abstract public class Entities
    {
        private Point loc;
        private double speed;
        //health auto set to 1
        private int health;
        public Entities(){  }
        private Rectangle hitbox;
        //return true if destroyed else return false
        public abstract bool Hit();

        public abstract Point UpdatePosition();
    }

    public class Powerup:Entities
    {
        public override Point UpdatePosition()
        {
            throw new NotImplementedException();
        }
        public override bool Hit()
        {
            throw new NotImplementedException();
        }
    }
    

    public class Asteroid : Entities
    {
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
