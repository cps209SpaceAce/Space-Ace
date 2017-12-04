using System;
using System.Windows;

/// <header>
/// AI enemy class and its children
/// </header>

namespace Model
{
    //Patterns in which certain types of AI can fly in
    public enum pattern { Straight, Sin, Cos, Sindown };

    //Most basic enemy type that flighs straight and shoots
    public class AI : Entity
    {
        public int fireCoolDown = 50;
        public pattern Flightpath;      

        //Constructor that sets X and Y coordinates and flight pattern
        public AI(double X, double Y, pattern flightpath) : base(X, Y)
        {
        }

        //Method for when AI gets hit
        public override bool Hit()
        {
            alive = false;
            return true;
        }

        //Method that moves AI accross the screen and fires bullets
        public override void UpdatePosition()
        {
            X = (X - (0.5 * speed));
            hitbox.X = Convert.ToInt32(X);
            if (random.Next(0, 1000) == 42)
            {
                FiredABullet = true;
                fireCoolDown = 50;
            }
            else
            {
                FiredABullet = false;
            }
            if (X < 0)
                alive = false;
        }

        //Serialization method that converts all necessary values into a string
        public override string Serialize()
        {
            return "ai" + "," + X + "," + Y + "," + Flightpath;
        }
    }
    
    //AI type that flies in different patterns
    public class Formation : AI
    {
        public double original_Y; //Keeping track of original Y coordinate

        public Formation(double X, double Y, pattern f) : base(X, Y, f)
        {
            original_Y = Y;
            Flightpath = f;
        }

        //Moves Formation according to FlightPath pattern (either Sin or Cos)
        public override void UpdatePosition()
        {
            switch (Flightpath)
            {
                case pattern.Sin:
                    X = (X - (1 * speed));
                    Y = (50 * Math.Sin(0.01 * X)) + original_Y;
                    break;
                case pattern.Cos:
                    X = (X - (0.5 * speed));
                    Y = (200 * Math.Cos(0.01 * X)) + original_Y;
                    break;

            }
            if (random.Next(0, 1000) == 42)
            {
                FiredABullet = true;
                fireCoolDown = 50;
            }
            else
            {
                FiredABullet = false;
            }
            hitbox.X = Convert.ToInt32(X);
            hitbox.Y = Convert.ToInt32(Y);
            if (X < 0)
                alive = false;
        }

        //Serialization method that converts all necessary values into a string
        public override string Serialize()
        {
            return "formation" + "," + X + "," + Y + "," + Flightpath;
        }
    }

    //Tracker enemy that stays at certain X position and tracks player's Y position while
    //shooting at specified rate
    public class Tracker : AI
    {
        double pX;              //Player's X position
        double pY;              //Player's Y position
        double stopPosition;    //X position to stop at and trak player's Y position

        //Constructor to set X,Y. FlightPath always Straight
        public Tracker(double X, double Y, pattern flightPath) : base(X, Y, flightPath)
        {
        }

        //Recieves player's current position and X position to stop at
        public void RecieveTrackerData(double playerX, double playerY, double stopPosition)
        {
            pX = playerX;
            pY = playerY;
            this.stopPosition = stopPosition;
        }

        //Starts by moving to designated stopPosition, then
        //updates position by tracking player's Y position
        public override void UpdatePosition()
        {
            if (X <= stopPosition)
                X = stopPosition;
            else
                X = (X - (0.5 * speed));

            if (Y < pY)
                Y = (Y + (0.5 * speed));
            else if (Y > pY)
                Y = (Y - (0.5 * speed));

            if (random.Next(0, 1000) == 64)
            {
                FiredABullet = true;
                fireCoolDown = 50;
            }
            else
            {
                FiredABullet = false;
            }
            hitbox.X = Convert.ToInt32(X);
            hitbox.Y = Convert.ToInt32(Y);

        }

        //Serialization method that converts all necessary values into a string
        public override string Serialize()
        {
            return "tracker" + "," + X + "," + Y;
        }

    }

    //Mine enemy tracks player's location around screen using the Seek Steering Behaviour
    public class Mine : AI
    {
        double pX;          //Player's X position
        double pY;          //Player's Y position
        Vector velocity;    //Mine's velocity (speed and direction)
        float maxSpeed;     //Maximum speed limit

        //Constructor to set initial position (FlightPath always Straight)
        public Mine(double X, double Y, pattern flightPath) : base(X, Y, flightPath)
        {
            velocity = new Vector(1, 1);
            maxSpeed = 2.5f;
        }

        //Recieve player's current position
        public void RecieveTrackerData(double playerX, double playerY)
        {
            pX = playerX;
            pY = playerY;
        }

        //Change position according to steering algorithm and track player's location
        //Based on Steering behariors: Seek  
        public override void UpdatePosition()
        {
            Vector target = new Vector(pX, pY);
            hitbox.X = Convert.ToInt32(X);
            hitbox.Y = Convert.ToInt32(Y);

            Vector currentPos = new Vector(X, Y);
            Vector desiredVel = target - currentPos;
            desiredVel.Normalize();
            desiredVel *= maxSpeed;
            Vector steering = desiredVel - velocity;
            velocity = steering + velocity;
            X += (int)velocity.X; Y += (int)velocity.Y;

        }

        //Serialization method that converts all necessary values into a string
        public override string Serialize()
        {
            return "mine" + "," + X + "," + Y;
        }
    }
}

