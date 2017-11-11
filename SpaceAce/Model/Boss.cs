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
        public Boss(double X, double Y, int health) : base(X,Y)
        {
        }

        public override bool Hit()
        {
            throw new NotImplementedException();
        }

        public override void UpdatePosition()
        {
            //TODO: movement logic for boss
            throw new NotImplementedException();
            if (X > 950)
            {
                X = Convert.ToInt32(X - (1 * speed));
            }
            
        }

        public override string Serialize()
        {
            return "boss" + "," + X + "," + Y; //JOANNA: x,y only for now; //JOANNA: X,Y coords only for now.
        }

        public override Entity Deserialize(string code)
        {
            return null;
        }
    }
}
