using System;

/// <header>
/// Hard Boss found in Boss level Hard Mode
/// </header>
namespace Model
{
    //Hard boss that moves in infinity sign pattern and is found in Boss level of Hard mode

    public class Boss_Hard : Boss
    {
        int reset = 50;
        bool startflag = true;
        double og_X;
        double og_Y;
        public double x_axis = 0;
        public Direction dir = Direction.Left;

        //constructor to set up inital state
        public Boss_Hard(double X, double Y, int health, double winWidth, double winHeight) : base(X, Y, health, winWidth, winHeight)
        {
            // state is always start
            state = State.Start;
            og_X = 924;
            og_Y = 300;
            hitbox.Width = 200;
            hitbox.Height = 200;
        }


        //controls movment and combat logic
        public override void UpdatePosition()
        {
            if (cooldown > 0)
                cooldown--;
            actionTimer += 0.01;
            //moves the boss to starting position
            if (X > 924 && startflag)
            {
                X = Convert.ToInt32(X - (1 * speed));
            }
            if (X <= 924)
                startflag = false;

            if (!startflag)
            {
                //switches  movment pattern
                switch (dir)
                {
                    case Direction.Left:
                        x_axis -= 10;
                        Y = (300 * Math.Sin(x_axis / 147)) + og_Y;
                        X = og_X + x_axis;
                        if (X <= 0)
                            dir = Direction.Right;
                        break;
                    case Direction.Right:
                        x_axis += 10;
                        Y = (-300 * Math.Sin(x_axis / 147)) + og_Y;
                        X = og_X + x_axis;

                        if (X >= 924)
                            dir = Direction.Left;
                        break;

                }

                //firing logic
                if (cooldown == 0) 
                {
                    //fires a targeted slanted shot at the player
                    action = true;
                    fired_slanted_targeted_shot = true;
                    cooldown = reset;
                }
                else
                    fired_slanted_targeted_shot = false;

            }

            hitbox.X = Convert.ToInt32(X);
            hitbox.Y = Convert.ToInt32(Y);
        }

        //Serialization method that converts all necessary save values into a string

        public override string Serialize()
        {
            return "boss,hard" + "," + X + "," + Y + "," + health + "," + state + "," + dir + "," + x_axis; //JOANNA: x,y only for now; //JOANNA: X,Y coords only for now.
        }
    }
}
