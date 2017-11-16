using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum pattern { Straight, Sin, Cos, Tan };
    public class AI : Entity
    {
        public int fireCoolDown = 50;
        
        
        
        public AI(double X, double Y, pattern flightpath) : base(X,Y)
        {
        }
        
        public override bool Hit()
        {
            alive = false;
            return true;
        }

        public override void UpdatePosition() 
        {
            X = (X - (0.5 * speed));
            hitbox.X = Convert.ToInt32(X);
            if (random.Next(0,1000) == 42)
            {
                FiredABullet = true; //for testing
                fireCoolDown = 50;
            }
            else
            {
                FiredABullet = false;
            }
            if (X < 0)
                alive = false;
        }

        public override string Serialize() {
            return "ai" + "," + X + "," + Y + "," + Flightpath;
        }
    }
    public class Formation : AI
    {
        public double original_Y;

        public Formation(double X, double Y, pattern f) : base(X,Y,f)
        {
            this.original_Y = Y;
            this.Flightpath = f;
        }

        public override void UpdatePosition()
        {
            //TODO: move ship in a Pattern
            switch (this.Flightpath)
            {
                case pattern.Sin:
                    X = (X - (1 * speed));
                    Y = (50 * Math.Sin(0.01 * X)) + original_Y;
                    break;
                case pattern.Cos:
                    X = (X - (0.5 * speed));
                    Y = (200 * Math.Cos(0.01 * X)) + original_Y;
                    break;
                case pattern.Tan:
                    break;

            }
            if (random.Next(0, 1000) == 42)
            {
                FiredABullet = true; //for testing
                fireCoolDown = 50;
            }
            else
            {
                FiredABullet = false;
            }
            hitbox.X = Convert.ToInt32(X);
            hitbox.Y = Convert.ToInt32(Y);
            if (X < 0)
                alive = false;
        }


        public override string Serialize()
        {
            return "formation" + "," + X + "," + Y + "," + Flightpath;
        }
    }
    public class Tracker : AI
    {
        private Player target;

        public Tracker(double X, double Y, pattern flightPath) : base( X,Y, flightPath)
        {
        }

        public override void UpdatePosition()
        {
            //TODO: Track player
            throw new NotImplementedException();
            if (X < 0)
                alive = false;
        }

        public override string Serialize()
        {
            return "tracker" + "," + X + "," + Y; //JOANNA: x,y only for now;
        }
    }
}
