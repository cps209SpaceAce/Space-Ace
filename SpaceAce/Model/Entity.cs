using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Model
{
    abstract public class Entity : ISerialiable
    {

        public static Random random = new Random();
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
            this.hitbox = new Rectangle(Convert.ToInt32(X), Convert.ToInt32(Y), 50, 50);
        }

        //return true if destroyed else return false
        public abstract bool Hit();

        public abstract void UpdatePosition();

        public abstract string Serialize();

        public static Entity Deserialize(string code, string type, GameController game)
        {
            string[] des = code.Split(',');
            Entity result = null;

            if (type == "enemy")
            {
                if (des[0] == "bullet")
                {
                    if (des[1] == "normal")
                    {
                        result = new Bullet(Convert.ToDouble(des[2]), Convert.ToDouble(des[3]));
                        (result as Bullet).direction = -1;
                        return result;
                    }
                    if (des[1] == "slanted")
                    {
                        result = new Slanted_Bullet(Convert.ToDouble(des[2]), Convert.ToDouble(des[3]), Convert.ToDouble(des[4]));
                        (result as Slanted_Bullet).id = ID.Hostile;

                        return result;
                    }
                }
                else if (des[0] == "asteroid")
                {
                    result = new Asteroid(Convert.ToDouble(des[2]), Convert.ToDouble(des[3]));
                    (result as Asteroid).health = Convert.ToInt32(des[1]);
                    return result;
                }
                else if (des[0] == "formation")
                {
                    pattern flight = pattern.Straight;
                    foreach (pattern val in Enum.GetValues(typeof(pattern)))
                    {
                        if (des[3] == val.ToString())
                        {
                            flight = val;
                            break;
                        }
                    }

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
                    foreach (PowerUp val in Enum.GetValues(typeof(PowerUp)))
                    {
                        if (des[3] == val.ToString())
                        {
                            p = val;
                            break;
                        }
                    }
                    result = new Powerup(Convert.ToDouble(des[1]), Convert.ToDouble(des[2]), p);
                    return result;
                }
                else if (des[0] == "boss")
                {
                    if (des[1] == "easy")
                    {
                        result = new Boss_Easy(Convert.ToDouble(des[2]), Convert.ToDouble(des[3]), Convert.ToInt32(des[4]), game.winWidth, game.winHeight);
                        foreach (State val in Enum.GetValues(typeof(State)))
                            if (des[5] == val.ToString())
                            {
                                (result as Boss_Easy).state = val;
                                break;
                            }

                        return result;
                    }
                    else if (des[1] == "medium")
                    {
                        result = new Boss_Medium(Convert.ToDouble(des[2]), Convert.ToDouble(des[3]), Convert.ToInt32(des[4]), game.winWidth, game.winHeight);
                        foreach (MState val in Enum.GetValues(typeof(MState)))
                            if (des[5] == val.ToString())
                            {
                                (result as Boss_Medium).currentState = val;
                                break;
                            }
                        
                        (result as Boss_Medium).isEntering = Convert.ToBoolean(des[6]);
                        (result as Boss_Medium).goingBackwards = Convert.ToBoolean(des[7]);
                        (result as Boss_Medium).dir = Convert.ToInt32(des[8]);

                        return result;
                    }
                    else if (des[1] == "hard")
                    {
                        result = new Boss_Hard(Convert.ToDouble(des[2]), Convert.ToDouble(des[3]), Convert.ToInt32(des[4]), game.winWidth, game.winHeight);
                        foreach (State val in Enum.GetValues(typeof(State)))
                            if (des[5] == val.ToString())
                            {
                                (result as Boss_Hard).state = val;
                                break;
                            }
                        
                        foreach (Direction val in Enum.GetValues(typeof(Direction)))
                        {
                            if (des[6] == val.ToString())
                            {
                                (result as Boss_Hard).dir = val;
                                break;
                            }
                        }
                        return result;
                    }
                }
                else
                {
                    throw new Exception("Enemy type Unknown.");
                }

            }
            else if (type == "player")
            {

                result = new Player(Convert.ToDouble(des[0]), Convert.ToDouble(des[1]), Convert.ToInt32(des[3]), Convert.ToInt32(des[4]), game, "SHIP_IMAGE");


                foreach (PowerUp val in Enum.GetValues(typeof(PowerUp)))
                {
                    if (des[2] == val.ToString())
                    {
                        (result as Player).powerup = val;
                        break;
                    }
                }

                (result as Player).isPoweredUp = Convert.ToBoolean(des[5]);
                (result as Player).cheating = Convert.ToBoolean(des[7]);

                (result as Player).powerUpCounter = Convert.ToDouble(des[6]);
                (result as Player).image = des[8];

                (result as Player).triple = Convert.ToBoolean(des[9]);
                (result as Player).wanderingbullets = Convert.ToBoolean(des[10]);
                (result as Player).extraSpeed = Convert.ToBoolean(des[11]);
                (result as Player).rapid_fire = Convert.ToBoolean(des[12]);
                return result;

            }
            else if (type == "playerBullet")
            {
                if (des[1] == "normal")
                {
                    result = new Bullet(Convert.ToInt32(des[2]), Convert.ToInt32(des[3]));
                    (result as Bullet).direction = 1;
                    return result;
                }
                else if (des[1] == "slanted")
                {
                    result = new Slanted_Bullet(Convert.ToDouble(des[2]), Convert.ToDouble(des[3]), Convert.ToDouble(des[4]));
                    (result as Slanted_Bullet).id = ID.Friendly;

                    return result;
                }
                else if (des[1] == "wandering")
                {
                    pattern p = pattern.Straight;
                    foreach (pattern val in Enum.GetValues(typeof(pattern)))
                    {
                        if (des[6] == val.ToString())
                        {
                            p = val;
                            break;
                        }
                    }
                    result = new Wandering_Bullet(Convert.ToDouble(des[2]), Convert.ToDouble(des[3]), p);
                    (result as Wandering_Bullet).id = ID.Friendly;
                    (result as Wandering_Bullet).original_X = Convert.ToDouble(des[4]);
                    (result as Wandering_Bullet).original_Y = Convert.ToDouble(des[5]);
                    (result as Wandering_Bullet).x_axis = Convert.ToDouble(des[7]);

                    return result;
                }
            }
            return result;
        }
    }

    public class Powerup : Entity
    {
        public PowerUp type;
        public Powerup(double X, double Y, PowerUp type) : base(X, Y)
        {
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

        public Asteroid(double X, double Y) : base(X, Y)
        {
            Size s;
            int r = random.Next(50, 101);
            s = new Size(r, r);
            hitbox = new Rectangle(new Point(Convert.ToInt32(X), Convert.ToInt32(Y)), s);

            if (r < 60)
                health = 1;
            else if (r >= 60 && r < 80)
                health = 3;
            else if (r >= 80)
                health = 7;


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
            X = Convert.ToDouble(X - (1 * speed));
            hitbox.X = Convert.ToInt32(X);
            if (X < 0)
                alive = false;
        }

        public override string Serialize()
        {
            return "asteroid" + "," + health + "," + X + "," + Y;
        }
    }
}
