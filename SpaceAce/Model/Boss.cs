﻿using System;
using System.Drawing;

namespace Model
{
    public enum Direction { Left, Right, Up, Down }
    public enum State { Start, Mid, End }
    abstract public class Boss : Entity, ISerialiable
    {
        public bool fired_slanted_targeted_shot;
        public bool action = false;
        public bool wall = false; // attack for boss creates a wall of small asteroids
        public double bullet_x = 750;
        public double bullet_y = 300;
        public double p_x = 0;
        public double p_y = 0;
        public string img = "ship 2.png";
        public State state;
        public int cooldown = 0;
        public double actionTimer;
        public float max;
        public bool startup = true;     //True if Boss is entering the screen

        public double windowHeight;     //Window height for upper an lower boundaries of movement
        public double windowWidth;      //Window width for left and right boundaries of movement
        public Boss(double X, double Y, int health, double winWidth, double winHeight) : base(X, Y)
        {
            this.health = health;
            max = health;
            actionTimer = 0;
            hitbox = new Rectangle(Convert.ToInt32(X), Convert.ToInt32(Y), 500, 300);

            windowHeight = winHeight;
            windowWidth = winWidth;
        }

        //Recieve player's current position
        public void RecieveTrackerData(double X, double Y)
        {
            p_x = X;
            p_y = Y;
        }

        public override bool Hit()
        {
            if (X > 750 && startup)
                return false;
            else if (startup == true)
                startup = false;

            health--;
            if (health <= 0)
            {
                alive = false;
                return true;
            }
            return false;
        }
    }
}
