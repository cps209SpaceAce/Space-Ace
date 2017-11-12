using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum ID { Friendly, Hostile }
    public class Bullet : Entity
    {
        //JOANA: i made direction public for save and load purposes
    
        public int direction = 1; // 1:moving right | -1:moving left
        public Bullet(double X, double Y) : base(X,Y)
        {
            hitbox.Width = 5;
            hitbox.Height = 5;
        }
        //friendly means it does not hit player
        // id determines direction of travel
        public ID id;

        public override bool Hit()
        {
            alive = false;
            return true;
        }

        public override void UpdatePosition()
        {
            //TODO: move bullet in correct direction
            X += 15 * direction;
            hitbox.X = Convert.ToInt32(X);
            if (X < 0 || X >1200)
                alive = false;
            //throw new NotImplementedException();

        }

        public override string Serialize()
        {
            return "bullet" + "," + X + "," + Y; //JOANNA: x,y only for now
        }

        public static Bullet Deserialize(string code)
        {
            return null;
        }
    }
    //public class Tracking : Bullet
    //{
    //    private Entity target;
    //    public override Point UpdatePosition()
    //    {
    //        //TODO: Towards target
    //        throw new NotImplementedException();
    //    }
    //    //TODO: add tracking
    //}
}
