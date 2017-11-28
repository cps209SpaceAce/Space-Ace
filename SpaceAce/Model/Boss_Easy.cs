using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Model
{
    public class Boss_Easy : Boss // Noah's Boss
    {
        int reset = 50;


        public Boss_Easy(double X, double Y, int health, double winWidth, double winHeight) : base(X, Y, health, winWidth, winHeight)
        {
            state = State.Start;

        }



        public override void UpdatePosition()
        {
            if (cooldown > 0)
                cooldown--;
            //TODO: movement logic for boss
            actionTimer += 0.01;
            if (X > 750)//Jo: use the windowHeight/windowWidth variables ^
            {
                X = Convert.ToInt32(X - (1 * speed));

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
            if (health < (max / 4 * 3) && state == State.Start)
            {
                reset = 25;
            }


            if (Y < (p_y - 100))
                Y = (Y + (0.5 * speed));
            else if (Y > (p_y - 100))
                Y = (Y - (0.5 * speed));
            if (cooldown == 0)
            {
                action = true;
                fired_slanted_targeted_shot = true;
                //wbullet_y = p_y;
                cooldown = reset;
            }
            else
                FiredABullet = false;

        }

        private void mid()
        {
            if (actionTimer > 0.75)
            {
                actionTimer = 0;
                wall = true;
                action = true;
            }
            else
                wall = false;
            start();
        }




        public override string Serialize()
        {
            return "boss,easy" + "," + X + "," + Y + "," + health + "," + state; //JOANNA: x,y only for now; //JOANNA: X,Y coords only for now.
        }
    }
}
