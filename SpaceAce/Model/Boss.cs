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
        public Boss(Point location, int health) : base(location)
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
            if (loc.X > 950)
            {
                loc.X = Convert.ToInt32(loc.X - (1 * speed));
            }
            
        }

        public override string Serialize()
        {
            return "boss" + "," + loc.X + "," + loc.Y; 

        public override Entity Deserialize(string code)
        {
            return null;
        }
    }
}
