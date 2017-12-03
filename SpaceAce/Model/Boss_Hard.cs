using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace Model
{
    
    public class Boss_Hard : Boss
    {
        int reset = 50;
        bool startflag = true;
        double og_X;
        double og_Y;
        public double x_axis = 0;
        public Direction dir = Direction.Left;

        public Boss_Hard(double X, double Y, int health, double winWidth, double winHeight) : base(X, Y, health, winWidth, winHeight)
        {
            state = State.Start;
            og_X = 924;
            og_Y = 300;
            hitbox.Width = 200;
            hitbox.Height = 200;
        }



        public override void UpdatePosition()
        {
            if (cooldown > 0)
                cooldown--;
            //TODO: movement logic for boss
            actionTimer += 0.01;
            if (X > 924 && startflag)
            {
                X = Convert.ToInt32(X - (1 * speed));
            }
            if (X <= 924)
                startflag = false;

            if (!startflag)
            {
                switch (dir)
                {
                    case Direction.Left:
                        x_axis -= 10;
                        Y = (300 * Math.Sin(x_axis / 147)) + og_Y;
                        X = og_X + x_axis;
                        if (X <= 0)
                            dir = Direction.Right;
                        break;
                    case Direction.Right:
                        x_axis += 10;
                        Y = (-300 * Math.Sin(x_axis / 147)) + og_Y;
                        X = og_X + x_axis;

                        if (X >= 924)
                            dir = Direction.Left;
                        break;

                }

                if (cooldown == 0)
                {
                    action = true;
                    fired_slanted_targeted_shot = true;
                    cooldown = reset;
                }
                else
                    fired_slanted_targeted_shot = false;

            }
            // after x == 950 ... change y V^

            // action == 1 
            //bossShoot(x,y,type)
            hitbox.X = Convert.ToInt32(X);
            hitbox.Y = Convert.ToInt32(Y);
        }

  

     



        public override string Serialize()
        {
            return "boss,hard" + "," + X + "," + Y + "," + health + "," + state + "," + dir + "," + x_axis; //JOANNA: x,y only for now; //JOANNA: X,Y coords only for now.
        }
    }
}
