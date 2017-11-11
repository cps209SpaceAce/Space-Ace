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
        public pattern Flightpath;

        
        public AI(double X, double Y, pattern flightpath) : base(X,Y)
        {
        }
        
        public override bool Hit()
        {
            throw new NotImplementedException();
        }

        public override void UpdatePosition() 
        {
            X = (X - (0.5 * speed));
         
        }

        public override string Serialize() 
        {
            return "ai" + "," + X + "," + Y; //JOANNA: x,y only for now;
        }

        public override Entity Deserialize(string code)
        {
            return null;
        }
    }
    public class Formation : AI
    {
        public double original_Y;

        public Formation(double X, double Y, pattern f) : base(X,Y,f)
        {
            this.original_Y = Y;
        }

        public override void UpdatePosition()
        {
            //TODO: move ship in a Pattern
            switch (this.Flightpath)
            {
                case pattern.Sin:
                    X = Convert.ToInt32(X - (1 * speed));
                    Y = Convert.ToInt32(Math.Sin(X)) + original_Y;
                    break;
                case pattern.Cos:
                    X = Convert.ToInt32(X - (1 * speed));
                    Y = Convert.ToInt32(Math.Cos(X)) + original_Y;
                    break;
                case pattern.Tan:
                    break;
            }
            
        }


        public override string Serialize()
        {
            return "formation" + "," + X + "," + Y; //JOANNA: x,y only for now
        }

        public override Entity Deserialize(string code)
        {
            return null;
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
        }

        public override string Serialize()
        {
            return "tracker" + "," + X + "," + Y; //JOANNA: x,y only for now;
        }

        public override Entity Deserialize(string code)
        {
            return null;
        }
    }
}
