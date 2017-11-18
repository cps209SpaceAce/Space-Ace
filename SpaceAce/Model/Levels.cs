﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Model
{

    class Levels
    {
    public int spawn_X = 600;    

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
            return new Asteroid(1200, 350);
        }
    }
}
