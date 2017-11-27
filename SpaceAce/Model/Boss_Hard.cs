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
        double x_axis = 0;
        Direction dir = Direction.Left;

        public Boss_Hard(double X, double Y, int health) : base(X, Y, health)
        {
            state = State.Start;
            og_X = X;
            og_Y = Y;
            hitbox.Width = 100;
            hitbox.Height = 100;
        }



        public override void UpdatePosition()
        {
            if (cooldown > 0)
                cooldown--;
            //TODO: movement logic for boss
            actionTimer += 0.01;
            if (X > 750 && startflag)//Jo: use the windowHeight/windowWidth variables ^
            {
                X = Convert.ToInt32(X - (1 * speed));
               
            }
            if (X <= 750)
                startflag = false;


            switch (dir)
            {
                case Direction.Left:
                    x_axis = (x_axis + (-10));
                    Y = (6*(50 * Math.Sin(0.01 * x_axis)) + og_Y);
                    X = og_X + x_axis;
                    if (X == 0)
                        dir = Direction.Up;
                    break;
                case Direction.Right:
                    x_axis = (x_axis + (10));
                    Y = (6*(50 * Math.Sin(0.01 * x_axis)) * (-1) + og_Y);
                    X = og_X + x_axis;

                    if (X == 900)
                        dir = Direction.Down;
                    break;
                case Direction.Up:
                    Y -= 2;
                    if (Y < 0)
                        dir = Direction.Right;
                    break;
                case Direction.Down:
                    Y += 2;
                    if (Y > 600)
                        dir = Direction.Left;
                    break;
            }

            switch (state)
            {
                case State.Start:
                    start();
                    if (health < (max / 2))
                    {
                        state = State.Mid;
                        reset = 50;
                    }
                    break;
                case State.Mid:
                    mid();
                    if (health < (max / .75))
                    {
                        state = State.End;
                        reset = 25;
                    }
                    break;
                case State.End:
                    mid();
                    break;

            }
            // after x == 950 ... change y V^

            // action == 1 
            //bossShoot(x,y,type)
            hitbox.X = Convert.ToInt32(X);
            hitbox.Y = Convert.ToInt32(Y);
        }

        private void start()
        {
          
        }

        private void mid()
        {
          
        }




        public override string Serialize()
        {
            return "boss,easy" + "," + X + "," + Y + "," + health; //JOANNA: x,y only for now; //JOANNA: X,Y coords only for now.
        }
    }
}
