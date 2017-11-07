using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum ID {Friendly, Hostile }
    public class Bullet : Entity
    {
        //friendly means it does not hit player
        // id determines direction of travel
        public ID id;

        public override bool Hit()
        {
            throw new NotImplementedException();
        }

        public override Point UpdatePosition()
        {
            //TODO: move bullet in correct direction
            throw new NotImplementedException();
        }
    }
    public class Tracking : Bullet
    {
        private Entity target;
        public override Point UpdatePosition()
        {
            //TODO: Towards target
            throw new NotImplementedException();
        }
        //TODO: add tracking
    }
}
