using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Model
{
    abstract public class Entity: ISerialiable
    {
        
        public static Random random = new Random();
        public double speed;
        //health auto set to 1
        public int health;
        public bool FiredABullet = false;
        public pattern Flightpath;
        public Rectangle hitbox;
        public double X;
        public double Y;
        public bool alive = true;
        

        public Entity(double X, double Y)
        {
            this.health = 1;
            this.X = X;
            this.Y = Y;
            this.speed = 4;
            this.hitbox = new Rectangle(Convert.ToInt32(X),Convert.ToInt32(Y),50,50);
        }
        
        //return true if destroyed else return false
        public abstract bool Hit();

        public abstract void UpdatePosition();

        public abstract string Serialize();

        public static Entity Deserialize(string code, string type, GameController game)
        {
            string[] des = code.Split(',');
            Entity result = null;

            if (type == "enemy") {
                if (des[0] == "bullet")
                {
                    result = new Bullet(Convert.ToDouble(des[1]), Convert.ToDouble(des[2]));
                    (result as Bullet).direction = -1;
                    return result;
                }
                else if (des[0] == "asteroid")
                {
                    result = new Asteroid(Convert.ToDouble(des[2]), Convert.ToDouble(des[3]));
                    (result as Asteroid).health = Convert.ToInt32(des[1]);
                    return result;
                }
                else if(des[0] == "formation")
                {
                    pattern flight = pattern.Straight;
                    if (des[3] == "Cos")
                        flight = pattern.Cos;
                    else if (des[3] == "Sin")
                        flight = pattern.Sin;
                    else if (des[3] == "Tan")
                        flight = pattern.Tan;
                    else if (des[3] == "Straight")
                        flight = pattern.Straight;

                    return new Formation(Convert.ToDouble(des[1]), Convert.ToDouble(des[2]), flight);
                }
                else if (des[0] == "ai")
                {
                    result = new AI(Convert.ToDouble(des[1]), Convert.ToDouble(des[2]), pattern.Straight);
                    return result;
                }
                else if (des[0] == "mine")
                {
                    result = new Mine(Convert.ToDouble(des[1]), Convert.ToDouble(des[2]), pattern.Straight);
                    return result;
                }
                else if (des[0] == "tracker")
                {
                    result = new Tracker(Convert.ToDouble(des[1]), Convert.ToDouble(des[2]), pattern.Straight);
                    return result;
                }
                else if (des[0] == "powerup")
                {
                    PowerUp p = PowerUp.Empty;

                    if (des[3] == "Power")
                        p = PowerUp.Power;
                    else if (des[3] == "Invinsible")
                        p = PowerUp.Invicible;
                    else if (des[3] == "ExtraLife")
                        p = PowerUp.ExtraLife;

                    result = new Powerup(Convert.ToDouble(des[1]), Convert.ToDouble(des[2]), p);

                }
                else if (des[0] == "boss")
                {
                }
                else
                {
                    throw new Exception("Enemy type Unknown.");
                }

            }
            else if(type == "player")
            {
                result = new Player(Convert.ToDouble(des[0]), Convert.ToDouble(des[1]), Convert.ToInt32(des[3]), Convert.ToInt32(des[4]), game);
                (result as Player).powerup = PowerUp.Empty;

                if (des[2] == "Power")
                    (result as Player).powerup = PowerUp.Power;
                else if (des[2] == "Invinsible")
                    (result as Player).powerup = PowerUp.Invicible;
                else if (des[3] == "ExtraLife")
                    (result as Player).powerup = PowerUp.ExtraLife;

                if (des[5] == "True")
                    (result as Player).isPoweredUp = true;
                else
                    (result as Player).isPoweredUp = false;

                (result as Player).powerUpCounter = Convert.ToDouble(des[6]);
            }
            else if(type == "playerBullet")
            {
                result = new Bullet(Convert.ToInt32(des[1]), Convert.ToInt32(des[2]));
                (result as Bullet).direction = 1;
                return result;
            }
            return result;
        }
    }

    public class Powerup:Entity
    {
        public PowerUp type;
        public Powerup(double X, double Y, PowerUp type) :base(X,Y) {
            this.type = type;
            this.hitbox = new Rectangle(Convert.ToInt32(X), Convert.ToInt32(Y), 40, 40);
        }
        public override void UpdatePosition()
        {
            X--;
            if (X < 0)
                alive = false;

            hitbox.X = Convert.ToInt32(X);
            hitbox.Y = Convert.ToInt32(Y);
        }
        public override bool Hit()
        {
            alive = false;
            return true; //does not take damage
        }

        public override string Serialize()
        {
            return "powerup" + "," + X + "," + Y + "," + type; 
        }
    }
    

    public class Asteroid : Entity
    {

        public Asteroid(double X, double Y) : base(X,Y)
        {
            Size s;
            int r = random.Next(50,101);
            s = new Size(r,r);
            hitbox = new Rectangle(new Point(Convert.ToInt32(X),Convert.ToInt32(Y)), s );   
        }
        public override bool Hit()
        { 
            return false;
        }
        public override void UpdatePosition()
        {
            X = Convert.ToDouble(X - (1 * speed));
            hitbox.X = Convert.ToInt32(X);   
            if (X < 0)
                alive = false;
        }

        public override string Serialize()
        {
            return "asteroid" + "," + health + "," + X + "," + Y;
        }

        public static Entity Deserialize(string code)
        {
            return null;
        }
    }

   


   


}
