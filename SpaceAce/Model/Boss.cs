using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum State { Start, Mid, End }
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
        public Boss(double X, double Y, int health) : base(X, Y)
        {
            this.health = health;
            max = health;
            actionTimer = 0;
            this.hitbox = new Rectangle(Convert.ToInt32(X), Convert.ToInt32(Y), 500, 300);
            
            //this.player = player;
        }

        public void RecieveTrackerData(double X, double Y)
        {
            p_x = X;
            p_y = Y;
        }

        public override bool Hit()
        {
            health--;
            if (health == 0)
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
            return "boss" + "," + X + "," + Y; //JOANNA: x,y only for now; //JOANNA: X,Y coords only for now.
        }

        public static Boss Deserialize(string code)
        {
            return null;
        }
    }



    public class Boss_Easy:Boss
    {
        
        
        public Boss_Easy(double X, double Y, int health) : base(X, Y,health)
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
            return "boss,easy_Boss" + "," + X + "," + Y + "," + health; //JOANNA: x,y only for now; //JOANNA: X,Y coords only for now.
        }
    }

    public class Diff_Yet_To_be_Determined_Boss : Boss //Jo's Boss, WIP
    {
        public Diff_Yet_To_be_Determined_Boss(double X, double Y, int health) : base(X, Y, health)
        {
        }


        public override void UpdatePosition()
        {
            if (cooldown > 0)
                cooldown--;
            //TODO: movement logic for boss
            actionTimer += 0.01;
            if (X > 750)
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
                    if (health > (max / .75f))
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
            return "boss" + "," + X + "," + Y + "," + health; //JOANNA: x,y only for now; //JOANNA: X,Y coords only for now.
        }
    }
}
