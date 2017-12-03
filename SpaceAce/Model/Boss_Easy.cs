using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

/// <summary>
/// Easy Boss found in Boss level Easy Mode
/// Code byy Joanna Al Madanat
/// </summary>
/// 
namespace Model
{
    public enum MState { Start, Mid, Attack } //State machine specific for Easy boss

    public class Boss_Easy : Boss
    {
        public MState currentState;         //Easy boss's current state
        public int dir = 1;                 //Boss's direction of movement (1 = down, -1 = up)
        public bool goingBackwards = false; //Movement of boss (X++ if true, X-- if false)
        public bool isEntering = true;      //States if boss is not yet in desired starting position
        int reset = 60;                     //Rate of bullet fire

        //Constructor taking in 
        //X position, Y position, initil health, Window height, and window width
        public Boss_Easy(double X, double Y, int health, double winWidth, double winHeight) : base(X, Y, health, winWidth, winHeight)
        {
            currentState = MState.Start;
        }

        //Updates the boss's position based on current state according to MState state mahine
        public override void UpdatePosition()
        {
            if (isEntering && X > windowWidth / 2)
            {
                X = Convert.ToInt32(X - speed);
                return;
            }
            isEntering = false;

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
                    Mid();
                    break;

                case MState.Attack:
                    Attack();
                    break;
            }

            hitbox.X = Convert.ToInt32(X);
            hitbox.Y = Convert.ToInt32(Y);
        }

        //At MState.Start, move up and down game screen while shooting at an average rate
        //Randomly set MState to Attack
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

        //At MState.Mid, track player's position and shoot at faster average rate
        //Randomly set MState to Attack
        private void Mid()
        {
            if (Y < (p_y - 100))
                Y = (Y + (0.5 * speed));
            else if (Y > (p_y - 100))
                Y = (Y - (0.5 * speed));

            Shoot();

            if (random.Next(0, 1000) == 4)
                currentState = MState.Attack;
        }

        //At MState.Attack, move forward until reaching zero then returning to previous position
        //and state
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

                if (health < (max / 2))
                    currentState = MState.Mid;
                else
                    currentState = MState.Start;
            }
        }

        //Shoot bullets at countdown rate
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

        //Serialization method that converts all necessary save values into a string
        public override string Serialize()
        {
            return "boss,easy" + "," + X + "," + Y + "," + health + "," + currentState + "," + isEntering + "," + goingBackwards + "," + dir; //JOANNA: x,y only for now; //JOANNA: X,Y coords only for now.
        }
    }
}
