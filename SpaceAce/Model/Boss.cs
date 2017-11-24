﻿using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Boss : Entity, ISerialiable
    {

        private double actionTimer;
        public Boss(double X, double Y, int health) : base(X,Y)
        {
            actionTimer = 0;
        }

        public override bool Hit()
        {
            throw new NotImplementedException();
        }

        public override void UpdatePosition()
        {
            //TODO: movement logic for boss
            actionTimer += 0.01;
            if (X > 950)
            {
                X = Convert.ToInt32(X - (1 * speed));
            }
            // after x == 950 ... change y V^

                // action == 1 
                //bossShoot(x,y,type)
            
        }


        public override string Serialize()
        {
            return "boss" + "," + X + "," + Y + "," + health; //JOANNA: x,y only for now; //JOANNA: X,Y coords only for now.
        }
    }

    public class Suzan : Boss 
        //Just like how the game spawn asteroids and enemies, what if the game spawns
        //only one of each type of boss
        // - Jo
    {

        private double actionTimer;
        public string name = "suzan";
        public Suzan(double X, double Y, int health) : base(X, Y, health)
        {
            actionTimer = 0;
        }

        public override bool Hit()
        {
            throw new NotImplementedException();
        }

        public override void UpdatePosition()
        {
        }


        public override string Serialize()
        {
            return "boss" + "," + name + "," + X + "," + Y + "," + health ; //JOANNA: x,y only for now; //JOANNA: X,Y coords only for now.
        }
    }
}
