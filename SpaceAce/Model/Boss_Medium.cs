using System;

/// <summary>
/// Medium Boss found in Boss level Medium Mode
/// </summary>

namespace Model
{
    //Logic for the medium boss
    public class Boss_Medium : Boss 
    {
        int reset = 50;

        //constructor that sets the starting state of boss
        public Boss_Medium(double X, double Y, int health, double winWidth, double winHeight) : base(X, Y, health, winWidth, winHeight)
        {
            //set the combat level state
            state = State.Start;

        }

        //tracks the player up and down and controls fire logic
        public override void UpdatePosition()
        {
            
            if (cooldown > 0)
                cooldown--;
            actionTimer += 0.01;

            //positions the boss to starting location 
            if (X > 750)
            {
                X = Convert.ToInt32(X - speed);
                return;
            }

            //state machine to control boss stages
            switch (state)
            {
                case State.Start:
                    //first stage, slow attacks
                    //attacks get faster as you approch mid state
                    start();
                    if (health < (max / 2))
                    {
                        state = State.Mid;
                        reset = 50;
                    }
                    break;
                case State.Mid:
                    //attack slowdown, but wall attack is included
                    
                    Mid();
                    if (health < (max / .75))
                    {
                        
                        state = State.End;
                        reset = 25; //increases attack rate
                    }
                    break;
                case State.End:
                    //final stage
                    Mid();
                    break;

            }
            //updates hit boxes
            hitbox.X = Convert.ToInt32(X);
            hitbox.Y = Convert.ToInt32(Y);
        }


        //first stage firing logic
        private void start()
        {
            
            if (health < (max / 4 * 3) && state == State.Start)
            {
                reset = 25; //increase attack speed
            }
           //fire at the players current position
            if (Y < (p_y - 100))
                Y = (Y + (0.5 * speed));
            else if (Y > (p_y - 100))
                Y = (Y - (0.5 * speed));
            if (cooldown == 0) //cooldown for firing
            {
                action = true; //flag to tell gamecontroller that boss fired
                fired_slanted_targeted_shot = true;
                cooldown = reset;
            }
            else
                FiredABullet = false;
        }

        //controls mid and end stage combat logic
        private void Mid()
        {
            //action timer for wall of asteroid attack
            if (actionTimer > 0.75)
            {
                actionTimer = 0;
                wall = true;
                action = true;
            }
            else
                wall = false;
            //continue firing pattern from first stage
            start();
        }

        //Serialization method that converts all necessary save values into a string
        public override string Serialize()
        {
            return "boss,medium" + "," + X + "," + Y + "," + health + "," + state;
        }
    }
}
