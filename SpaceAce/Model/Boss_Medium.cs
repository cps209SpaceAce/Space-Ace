using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace Model
{
    public enum MState { Start, Mid, End, EasyAttack }

    public class Boss_Medium : Boss //Jo's Boss, WIP
    {
        MState currentState;
        int dir = 1;
        int shootTimer = 0;
        bool shoot = false;
        bool goingBack = false;
        public Boss_Medium(double X, double Y, int health) : base(X, Y, health)
        {
            currentState = MState.Start;
        }


        public override void UpdatePosition()
        {
            if (cooldown > 0)
                cooldown--;

            actionTimer += 0.01;



            switch (currentState)
            {
                case MState.Start:
                    start();
                    if (health < (max / 2))
                        currentState = MState.Mid;
                    else
                        if (random.Next(0, 500) == 4)
                        currentState = MState.EasyAttack;

                    break;
                case MState.Mid:
                    mid();
                    if (health < (max / .75f))
                        currentState = MState.End;
                    break;
                case MState.End:
                    End();
                    break;

                case MState.EasyAttack:
                    Attack();
                    break;
            }

            hitbox.X = Convert.ToInt32(X);
            hitbox.Y = Convert.ToInt32(Y);
        }

        private void start() //todo: continue stat machine
        {
            if (X > windowWidth / 2 + windowWidth / 4)
                X = Convert.ToInt32(X - speed);
            else
            {
                if (Y < 0 || Y > windowHeight - hitbox.Y)
                {
                    dir *= -1;
                }
                Y = Convert.ToInt32(Y + (dir * speed));
            }


            if (!shoot && random.Next(0, 100) == 2)
                shoot = true;

            if (shoot)
            {
                FiredABullet = true;
                shootTimer++;

                if (shootTimer >= 50)
                {
                    shoot = false;
                    shootTimer = 0;
                    FiredABullet = false;
                }

            }
        }

        private void mid() {}

        private void End() { }

        private void Attack()
        {
            if (!goingBack && X > 0)
            {
                X = Convert.ToInt32(X - speed);
                return;
            }
            goingBack = true;


            if (X <= windowWidth / 2 + windowWidth / 4)
                X = Convert.ToInt32(X + speed);
            else
            {
                goingBack = false;
                currentState = MState.Start;
            }
        }


        public override string Serialize()
        {
            return "boss,medium" + "," + X + "," + Y + "," + health + "," + currentState; //JOANNA: x,y only for now; //JOANNA: X,Y coords only for now.
        }
    }
}
