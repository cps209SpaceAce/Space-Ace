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
        public Bullet(double X, double Y) : base(X, Y)
        {
            hitbox.Width = 20;
            hitbox.Height = 20;
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
            if (this.id == ID.Friendly)
            {
                X += 15 * direction;
            }
            else
            {
                X += 7 * direction;
            }

            hitbox.X = Convert.ToInt32(X);
            if (X < 0 || X > 1200)
                alive = false;

        }

        public override string Serialize()
        {
            return "bullet,normal" + "," + X + "," + Y; //JOANNA: x,y only for now
        }
    }

    public class Slanted_Bullet : Bullet
    {
        public double slope; // segested values: -1 to go up | 1 to go down
        public Slanted_Bullet(double X, double Y, double slope) : base(X, Y)
        {
            this.slope = slope;
        }


        public override void UpdatePosition()
        {
            if (this.id == ID.Friendly)
            {
                X += 15 * direction;
                Y += 5 * slope;
            }
            else
            {
                X += 15 * direction;
                Y += 15 * slope;
            }

            hitbox.X = Convert.ToInt32(X);
            hitbox.Y = Convert.ToInt32(Y);
            if (X < 0 || X > 1200)
                alive = false;
        }
        public override string Serialize()
        {
            return "bullet,slanted" + "," + X + "," + Y + "," + slope; //JOANNA: x,y only for now
        }
    }

    public class Wandering_Bullet:Bullet //Working: Noah Mansfield
    {
        public pattern path;
        public double original_Y;
        public double x_axis = 0;
        public double original_X;
     
        public Wandering_Bullet(double X, double Y, pattern path) : base(X, Y)
        {
            this.path = path;
            original_Y = Y;
            original_X = X;
            
            
        }
        public override void UpdatePosition()
        {
            switch (this.path) //copyed from Robert's formation class
            {
                case pattern.Sin:
                    x_axis = (x_axis + (15 * direction));
                    Y = ((50 * Math.Sin(0.01 * x_axis)) + original_Y);
                    X = original_X + x_axis;
                    break;
                case pattern.Sindown:
                    x_axis = (x_axis + (15 * direction));
                    Y = ((-50 * Math.Sin(0.01 * x_axis)) + original_Y);
                    X = original_X + x_axis;
                    break;
                

            }
            hitbox.X = Convert.ToInt32(X);
            hitbox.Y = Convert.ToInt32(Y);
            if (X < 0 || X > 1200)
                alive = false;
        }

        //Serialization method that converts all necessary values into a string
        public override string Serialize()
        {
            return "bullet,wandering" + "," + X + "," + Y + "," + original_X + "," +original_Y + "," + path + "," + x_axis; //JOANNA: x,y only for now
        }
    }
}
