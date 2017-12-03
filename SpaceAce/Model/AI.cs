﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum pattern { Straight, Sin, Cos, Sindown };
    public class AI : Entity
    {
        public int fireCoolDown = 50;
        public pattern Flightpath;


        public AI(double X, double Y, pattern flightpath) : base(X, Y)
        {
        }

        public override bool Hit()
        {

            alive = false;
            return true;
        }

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
    public class Formation : AI
    {
        public double original_Y;

        public Formation(double X, double Y, pattern f) : base(X, Y, f)
        {
            this.original_Y = Y;
            this.Flightpath = f;
        }

        public override void UpdatePosition()
        {
            switch (this.Flightpath)
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
    public class Tracker : AI
    {
        double pX; //player's location
        double pY;
        double stopPosition;

        public Tracker(double X, double Y, pattern flightPath) : base(X, Y, flightPath)
        {

        }

        public void RecieveTrackerData(double playerX, double playerY, double stopPosition)
        {
            pX = playerX;
            pY = playerY;
            this.stopPosition = stopPosition;
        }
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
    public class Mine : AI
    {
        double pX; //player's location
        double pY;
        Vector velocity;
        float maxSpeed;

        public Mine(double X, double Y, pattern flightPath) : base(X, Y, flightPath)
        {
            velocity = new Vector(1, 1);
            maxSpeed = 2.5f;
        }

        public void RecieveTrackerData(double playerX, double playerY)
        {
            pX = playerX;
            pY = playerY;
        }
        public override void UpdatePosition()
        {
            //Based on Steering behariors: Seek  
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

