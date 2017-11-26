using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum State { Start, Mid, End }
    public enum MedimBossState { Start, Mid, End, Attack, Retreat }
    public class Boss : Entity, ISerialiable
    {
        public double bullet_x = 750;
        public double bullet_y = 300;
        public double p_x = 0;
        public double p_y = 0;
        public string img = "ship 2.png";
        public State state;
        public int cooldown = 0;
        protected double actionTimer;
        public float max;
        //public Player player;

        public double windowHeight;
        public double windowWidth;
        public Boss(double X, double Y, int health) : base(X, Y)
        {
            this.health = health;
            max = health;
            actionTimer = 0;
            this.hitbox = new Rectangle(Convert.ToInt32(X), Convert.ToInt32(Y), 500, 300);

            //this.player = player;
        }

        public void RecieveTrackerData(double X, double Y, double windowWidth, double windowHeight)
        {
            p_x = X;
            p_y = Y;

            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
        }

        public override bool Hit()
        {
            health--;
            if (health <= 0)
            {
                alive = false;
                return true;
            }
            return false;

        }

        public override void UpdatePosition()
        {

            //TODO: movement logic for boss
            actionTimer += 0.01;
            if (X > 750)
            {
                X = Convert.ToInt32(X - (1 * speed));

            }

            // after x == 950 ... change y V^

            // action == 1 
            //bossShoot(x,y,type)
            hitbox.X = Convert.ToInt32(X);
            hitbox.Y = Convert.ToInt32(Y);
        }


        public override string Serialize()
        {
            return "boss,base" + "," + X + "," + Y; //JOANNA: x,y only for now; //JOANNA: X,Y coords only for now.
        }
    }



    public class Boss_Easy : Boss
    {


        public Boss_Easy(double X, double Y, int health) : base(X, Y, health)
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
                    if (health > (max / 2))
                        state = State.Mid;
                    break;
                case State.Mid:
                    mid();
                    if (health > (max / .75))
                        state = State.End;
                    break;
                case State.End:
                    End();
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
            FiredABullet = true;
        }

        private void mid() { }

        private void End() { }


        public override string Serialize()
        {
            return "boss,easy" + "," + X + "," + Y + "," + health; //JOANNA: x,y only for now; //JOANNA: X,Y coords only for now.
        }
    }

    public class Boss_Medium : Boss //Jo's Boss, WIP
    {
        MedimBossState Mstate;
        int dir = 1;
        int shootTimer = 0;
        bool shoot = false;
        bool isAttacking = false;
        bool goingBack = false;
        public Boss_Medium(double X, double Y, int health) : base(X, Y, health)
        {
            Mstate = MedimBossState.Start;
        }


        public override void UpdatePosition()
        {
            if (cooldown > 0)
                cooldown--;

            actionTimer += 0.01;



            switch (Mstate)
            {
                case MedimBossState.Start:
                    start();
                    if (health < (max / 2))
                        Mstate = MedimBossState.Mid;
                    else
                        if (random.Next(0, 500) == 4)
                        Mstate = MedimBossState.Attack;

                    break;
                case MedimBossState.Mid:
                    mid();
                    if (health < (max / .75f))
                        Mstate = MedimBossState.End;
                    break;
                case MedimBossState.End:
                    End();
                    break;

                case MedimBossState.Attack:
                    Attack();
                    break;

                case MedimBossState.Retreat:
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
                //FiredABullet = true;
                shootTimer++;

                if (shootTimer >= 50)
                {
                    shoot = false;
                    shootTimer = 0;
                    FiredABullet = false;
                }

            }
        }

        private void mid() { }

        private void End() { }

        private void Attack()
        {

            if (isAttacking)//not really using a state machine here.. kinda imbarrassing.
            {
                if (!goingBack && X > 0)
                    X = Convert.ToInt32(X - speed);
                else
                    goingBack = true;

                if (goingBack)
                {
                    if (X <= windowWidth / 2 + windowWidth / 4)
                        X = Convert.ToInt32(X + speed);
                    else
                    {
                        isAttacking = false;
                        goingBack = false;
                    }

                }
            }
        }


        public override string Serialize()
        {
            return "boss,medium" + "," + X + "," + Y + "," + health; //JOANNA: x,y only for now; //JOANNA: X,Y coords only for now.
        }
    }
}
