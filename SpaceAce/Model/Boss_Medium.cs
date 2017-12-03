using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Model
{
    public class Boss_Medium : Boss // Noah's Boss
    {
        int reset = 50;

        public Boss_Medium(double X, double Y, int health, double winWidth, double winHeight) : base(X, Y, health, winWidth, winHeight)
        {
            state = State.Start;

        }



        public override void UpdatePosition()
        {
            if (cooldown > 0)
                cooldown--;
            //TODO: movement logic for boss
            actionTimer += 0.01;
            if (X > 750)
            {
                X = Convert.ToInt32(X - speed);
                return;
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
                    Mid();
                    if (health < (max / .75))
                    {
                        state = State.End;
                        reset = 25;
                    }
                    break;
                case State.End:
                    Mid();
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

        private void Mid()
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

        //Serialization method that converts all necessary save values into a string
        public override string Serialize()
        {
            return "boss,medium" + "," + X + "," + Y + "," + health + "," + state;
        }
    }
}
