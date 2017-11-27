using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Model;
namespace Model
{

    class Levels
    {

        public static Entity Level_returnPowerUp()
        {
            Powerup output = null;
            int spawn_X = 1000;

            switch (GameController.random.Next(2, 3))
            {
                case 0:
                    output = new Powerup(spawn_X, GameController.random.Next(100, 600), PowerUp.Invincible);
                    break;
                case 1:
                    output = new Powerup(spawn_X, GameController.random.Next(100, 600), PowerUp.ExtraSpeed);
                    break;
                case 2:
                    output = new Powerup(spawn_X, GameController.random.Next(100, 600), PowerUp.ExtraLife);
                    break;
            }

            return output;
        }


        // This is a level class
        public static Entity Level_reuturnEntity(Difficulty currentDiff, Level level)
        {
            Entity output = null;

            if (level == Level.Level_1)
            {
                return returnLevel_1(currentDiff);
            }
            else if (level == Level.Level_2)
            {
                return returnLevel_2(currentDiff);
            }
            else if (level == Level.Boss)
            {
                return returnLevel_Boss(currentDiff);
            }

            return output;
        }



        public static Entity returnLevel_1(Difficulty currentDiff)
        {
            Entity output = null;
            int spawn_X = 1000;



            switch (currentDiff)
            {
                case Difficulty.Easy:
                    switch (GameController.random.Next(0, 3))
                    {
                        case 0:
                        case 1:
                            output = new Asteroid(spawn_X, GameController.random.Next(100, 600));
                            break;
                        case 2:
                            output = new AI(spawn_X, GameController.random.Next(0, 700), pattern.Straight);
                            break;
                    }
                    break;
                case Difficulty.Medium:

                    switch (GameController.random.Next(0, 3))
                    {
                        case 0:
                        case 1:
                            output = new Asteroid(spawn_X, GameController.random.Next(100, 600));
                            break;
                        case 2:
                            output = new Formation(spawn_X, GameController.random.Next(100, 600), pattern.Sin);
                            break;
                    }
                    break;
                case Difficulty.Hard:
                    switch (GameController.random.Next(0, 3))
                    {
                        case 0:
                            output = new Asteroid(spawn_X, GameController.random.Next(100, 600));
                            break;
                        case 1:
                            output = new Tracker(spawn_X, GameController.random.Next(100, 600), pattern.Straight);
                            break;
                        case 2:
                            output = new Formation(spawn_X, GameController.random.Next(100, 600), pattern.Cos);
                            break;
                    }
                    break;
            }
            return output;
        }



        public static Entity returnLevel_2(Difficulty currentDiff)
        {
            Entity output = null;
            int spawn_X = 1000;

            switch (currentDiff)
            {
                case Difficulty.Easy:
                    switch (GameController.random.Next(0, 3))
                    {
                        case 0:
                            output = new Asteroid(spawn_X, GameController.random.Next(100, 600));
                            break;
                        case 1:
                            output = new Formation(spawn_X, GameController.random.Next(100, 600), pattern.Sin);
                            break;
                        case 2:
                            output = new AI(spawn_X, GameController.random.Next(0, 700), pattern.Straight);
                            break;
                    }
                    break;
                case Difficulty.Medium:

                    switch (GameController.random.Next(0, 3))
                    {
                        case 0:
                            output = new Asteroid(spawn_X, GameController.random.Next(100, 600));
                            break;
                        case 1:
                            output = new Tracker(spawn_X, GameController.random.Next(100, 600), pattern.Straight);
                            break;
                        case 2:
                            output = new Formation(spawn_X, GameController.random.Next(100, 600), pattern.Cos);
                            break;
                    }
                    break;
                case Difficulty.Hard:
                    switch (GameController.random.Next(0, 3))
                    {
                        case 0:
                            output = new Tracker(spawn_X, GameController.random.Next(100, 600), pattern.Straight);
                            break;
                        case 1:
                            output = new Mine(spawn_X, GameController.random.Next(100, 600), pattern.Straight);
                            break;
                        case 2:
                            output = new AI(spawn_X, GameController.random.Next(0, 700), pattern.Straight);
                            break;
                    }
                    break;
            }
            return output;

        }

        public static Entity returnLevel_Boss(Difficulty currentDiff)
        {
            switch (currentDiff)
            {
                case Difficulty.Easy:
                    return new Boss_Easy(1200, 200, 30);
                case Difficulty.Medium:
                    return new Boss_Medium(1200, 200, 30);//TODO: add Boss_Medium
                case Difficulty.Hard:
                    return new Boss_Hard(1200, 300, 30);// TODO: add Boss_Hard
            }
            return new Boss(1200, 300, 30);
        }
    }

}
