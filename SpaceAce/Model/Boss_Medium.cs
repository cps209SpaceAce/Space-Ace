using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace Model
{
    public enum MState { Start, Mid, End, Attack }

    public class Boss_Medium : Boss 
    {
        public MState currentState;
        int dir = 1;
        int shootTimer = 0;
        bool shoot = false;
        bool goingBackwards = false;
        bool isEntering = true;
        int reset = 60;

        public Boss_Medium(double X, double Y, int health, double winWidth, double winHeight) : base(X, Y, health, winWidth, winHeight)
        {
            currentState = MState.Start;
        }


        public override void UpdatePosition()
        {
            if (isEntering && X > windowWidth / 2)
            {
                X = Convert.ToInt32(X - speed);
                return;
            }
            isEntering = false;

            if (cooldown > 0)
                cooldown--;
            //TODO: movement logic for boss
            actionTimer += 0.01;

            switch (currentState)
            {
                case MState.Start:
                    start();
                    if (health < (max / 2))
                    {
                        currentState = MState.Mid;
                        speed += 5;
                    }
                    break;
                case MState.Mid:
                    mid();
                    if (health < (max / .75f))
                    {
                        currentState = MState.End;
                        speed += 5;
                    }
                    break;
                case MState.End:
                    End();
                    break;

                case MState.Attack:
                    Attack();
                    break;
            }

            hitbox.X = Convert.ToInt32(X);
            hitbox.Y = Convert.ToInt32(Y);
        }

        private void start()
        {
            if (Y < 0 || Y > windowHeight - hitbox.Y)
            {
                dir *= -1;
            }
            Y = Convert.ToInt32(Y + (dir * speed));


            Shoot();

            if (random.Next(0, 1000) == 4)
              currentState = MState.Attack;
        }

        private void mid()
        {
            if(Y + hitbox.Y/2> p_y)
                Y = Convert.ToInt32(Y - speed);
            if(Y + hitbox.Y / 2 < p_y)
                Y = Convert.ToInt32(Y + speed);

            Shoot();

            if (random.Next(0, 1000) == 4)
                currentState = MState.Attack;
        }

        private void End()
        {
            if (Y > p_y)
                Y = Convert.ToInt32(Y - speed);
            else if (Y < p_y)
                Y = Convert.ToInt32(Y + speed);
            

            Shoot();

            if (random.Next(0, 1000) == 4)
                currentState = MState.Attack;
        }

        private void Attack()
        {
            if (!goingBackwards && X > 0)
            {
                X = Convert.ToInt32(X - speed - 2);
                return;
            }
            goingBackwards = true;


            if (X <= windowWidth / 2)
                X = Convert.ToInt32(X + speed + 2);
            else
            {
                goingBackwards = false;
                currentState = MState.Start;
            }
        }

        void Shoot()
        {

            if (cooldown == 0)
            {
                action = true;
                FiredABullet = true;
                bullet_y = Y + hitbox.Y/2;
                cooldown = reset;
            }
            else
                FiredABullet = false;
        }

        public override string Serialize()
        {
            return "boss,medium" + "," + X + "," + Y + "," + health + "," + currentState; //JOANNA: x,y only for now; //JOANNA: X,Y coords only for now.
        }
    }
}
