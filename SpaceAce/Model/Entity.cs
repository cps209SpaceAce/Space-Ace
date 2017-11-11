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
         

        public double speed;
        //health auto set to 1
        public int health;
        public bool FiredABullet = false;

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
                    result = new Bullet(Convert.ToInt32(des[1]), Convert.ToInt32(des[2]));
                    (result as Bullet).direction = -1;
                    return result;
                }
                else if (des[0] == "asteroid")
                {
                    result = new Asteroid(Convert.ToInt32(des[2]), Convert.ToInt32(des[3]));
                    (result as Asteroid).health = Convert.ToInt32(des[1]);
                    return result;
                }
                else if(des[0] == "formation")
                {
                }
                else if(des[0] == "boss")
                {
                }
                else if(des[0] == "powerup")
                {
                }
                else if (des[0] == "ai")
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

                    result = new AI(Convert.ToInt32(des[1]), Convert.ToInt32(des[2]), flight);
                    return result;
                }
                else
                {
                    throw new Exception("Enemy type Unknown.");
                }

            }
            else if(type == "player")
            {
                result = new Player(Convert.ToInt32(des[0]), Convert.ToInt32(des[1]), Convert.ToInt32(des[3]), Convert.ToInt32(des[4]), game);
                if (des[2] == "Power")
                    (result as Player).powerup = powerup.Power;
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
        public Powerup(double X, double Y, string name) :base(X,Y) { }
        public override void UpdatePosition()
        {
            throw new NotImplementedException();
        }
        public override bool Hit()
        {
            throw new NotImplementedException();
        }

        public override string Serialize()
        {
            return "powerup" + "," + X + "," + Y; 
            //please provide a way to detect the powerup's type
        }

        public static Powerup Deserialize(string code)
        {
            return null;
        }
    }
    

    public class Asteroid : Entity
    {
        public Asteroid(double X, double Y) : base(X,Y)
        { }
        public override bool Hit()
        { 
            //Asteroid can't be destroyed
            return false;
        }
        public override void UpdatePosition()
        {
            //TODO: add movment logic: make it move in a straight line
            X = Convert.ToInt32(X - (1 * speed));
            hitbox.X = Convert.ToInt32(X);
            
            // Why are we returning a point?

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
