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

        
        public AI(Point location, pattern flightpath) : base(location)
        {
        }
        
        public override bool Hit()
        {
            throw new NotImplementedException();
        }

        public override Point UpdatePosition() 
        {
            loc.X = Convert.ToInt32(loc.X - (1 * speed));
            return loc;
        }

        public override string Serialize() 
        {
            return "ai" + "," + loc.X + "," + loc.Y; //JOANNA: x,y only for now;
        }

        public override Entity Deserialize(string code)
        {
            return null;
        }
    }
    public class Formation : AI
    {
        public int original_Y;

        public Formation(Point location, pattern f) : base(location,f)
        {
            this.original_Y = loc.Y;
        }

        public override Point UpdatePosition()
        {
            //TODO: move ship in a Pattern
            throw new NotImplementedException();
            switch (this.Flightpath)
            {
                case pattern.Sin:
                    loc.X = Convert.ToInt32(loc.X - (1 * speed));
                    loc.Y = Convert.ToInt32(Math.Sin(loc.X)) + original_Y;
                    break;
                case pattern.Cos:
                    loc.X = Convert.ToInt32(loc.X - (1 * speed));
                    loc.Y = Convert.ToInt32(Math.Cos(loc.X)) + original_Y;
                    break;
                case pattern.Tan:
                    break;
            }
            
        }


        public override string Serialize()
        {
            return "formation" + "," + loc.X + "," + loc.Y; //JOANNA: x,y only for now
        }

        public override Entity Deserialize(string code)
        {
            return null;
        }
    }
    public class Tracker : AI
    {
        private Player target;

        public Tracker( Point location, pattern flightPath) : base( location, flightPath)
        {
        }

        public override Point UpdatePosition()
        {
            //TODO: Track player
            throw new NotImplementedException();
        }

        public override string Serialize()
        {
            return "tracker" + "," + loc.X + "," + loc.Y; //JOANNA: x,y only for now;
        }

        public override Entity Deserialize(string code)
        {
            return null;
        }
    }
}
