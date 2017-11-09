using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Boss : Entity, ISerialiable
    {
        public Boss(int health, Point location, int speed) : base(health, location, speed)
        {
        }

        public override bool Hit()
        {
            throw new NotImplementedException();
        }

        public override Point UpdatePosition()
        {
            //TODO: movement logic for boss
            throw new NotImplementedException();
        }

        public override string Serialize()
        {
            return "boss" + "," + loc.X + "," + loc.Y; //JOANNA: x,y only for now; //JOANNA: X,Y coords only for now.
        }

        public override Entity Deserialize(string code)
        {
            return null;
        }
    }
}
