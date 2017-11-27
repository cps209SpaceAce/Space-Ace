using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum Direction { Left, Right, Up, Down }
    public enum State { Start, Mid, End }
    //public enum BossAction {Fire, Wall }
    public enum MedimBossState { Start, Mid, End, Attack, Retreat }
    public class Boss : Entity, ISerialiable
    {
        public bool targeted_slant_shot = false;
        public bool action = false;
        public bool wall = false; // attack for boss creates a wall of small asteroids
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



    

    




    
}
