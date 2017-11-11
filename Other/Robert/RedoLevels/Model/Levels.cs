using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Model
{
    class Levels
    {

        // This is a level class
        public static Entity returnCurrentLevelEntity(Difficulty currentDiff)
        {
            Entity output = null;
            switch (currentDiff)
            {
                case Difficulty.Easy:
                    switch (GameController.random.Next(0, 2))
                    {
                        case 0:
                            output = new AI(1200, GameController.random.Next(0, 700), pattern.Straight);
                            break;
                        case 1:
                            output = new AI(1200, GameController.random.Next(0, 700), pattern.Straight);
                            break;
                    }
                    break;
                case Difficulty.Medium:
                    
                    switch (GameController.random.Next(0, 1))
                    {
                        case 0:
                            output = new Asteroid(1200, GameController.random.Next(100, 600));
                            break;
                        case 1:
                            output = new Asteroid(1200, GameController.random.Next(100, 600));
                            break;       
                    }
                    break;
                case Difficulty.Hard:
                    break;
            }
            return output;
        }
        
    }
}
