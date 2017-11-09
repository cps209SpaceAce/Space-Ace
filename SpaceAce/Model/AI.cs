﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum pattern { Straight, Sin, Cos, Tan };
    public class AI : Entity
    {
        private pattern Flightpath;
        public override bool Hit()
        {
            throw new NotImplementedException();
        }

        public override Point UpdatePosition() 
        {
            //TODO: move ship in staight line
            throw new NotImplementedException();
        }

        public override string Serialize() 
        {
            return "ai" + "," + loc.X + "," + loc.Y; //JOANNA: x,y only for now;
        }

        public override Entity Deserialize(string code)
        {
            return null;
        }
    }
    public class Formation : AI
    {
        public override Point UpdatePosition()
        {
            //TODO: move ship in a Pattern
            throw new NotImplementedException();
        }


        public override string Serialize()
        {
            return "formation" + "," + loc.X + "," + loc.Y; //JOANNA: x,y only for now
        }

        public override Entity Deserialize(string code)
        {
            return null;
        }
    }
    public class Tracker : AI
    {
        private Player target;
        public override Point UpdatePosition()
        {
            //TODO: Track player
            throw new NotImplementedException();
        }

        public override string Serialize()
        {
            return "tracker" + "," + loc.X + "," + loc.Y; //JOANNA: x,y only for now;
        }

        public override Entity Deserialize(string code)
        {
            return null;
        }
    }
}
