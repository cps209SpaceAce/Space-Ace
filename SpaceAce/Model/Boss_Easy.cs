﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
namespace Model
{
    public enum MState { Start, Mid, Attack }

    public class Boss_Easy : Boss
    {
        public MState currentState;
        public int dir = 1;
        public bool goingBackwards = false;
        public bool isEntering = true;
        int reset = 60;

        public Boss_Easy(double X, double Y, int health, double winWidth, double winHeight) : base(X, Y, health, winWidth, winHeight)
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

            //TODO: movement logic for boss
            actionTimer += 0.01;

            switch (currentState)
            {
                case MState.Start:
                    start();
                    if (health < (max / 2))
                    {
                        currentState = MState.Mid;
                        reset = 50;
                    }
                    break;
                case MState.Mid:
                    mid();
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
            if (Y < (p_y - 100))
                Y = (Y + (0.5 * speed));
            else if (Y > (p_y - 100))
                Y = (Y - (0.5 * speed));

            Shoot();

            if (random.Next(0, 1000) == 4)
                currentState = MState.Attack;
        }

        private void Attack()
        {
            FiredABullet = false;

            if (!goingBackwards && X > 0)
            {
                X = Convert.ToInt32(X - speed);
                return;
            }
            goingBackwards = true;


            if (X <= windowWidth / 2)
                X = Convert.ToInt32(X + speed);
            else
            {
                goingBackwards = false;
                currentState = MState.Start;
            }
        }

        void Shoot()
        {
            if (cooldown > 0)
                cooldown--;

            if (cooldown == 0)
            {
                action = true;
                FiredABullet = true;
                bullet_y = Y + hitbox.Y / 2;
                cooldown = reset;
            }
            else
                FiredABullet = false;

        }

        public override string Serialize()
        {
            return "boss,easy" + "," + X + "," + Y + "," + health + "," + currentState + "," + isEntering + "," + goingBackwards + "," + dir; //JOANNA: x,y only for now; //JOANNA: X,Y coords only for now.
        }
    }
}
