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

        public List<Entity> Level1()
        {
            //TODO Create list of enemies for level 1
            AI A  = new AI(new Point(1500, 250), pattern.Straight);
            AI B = new AI(new Point(1500, 400), pattern.Straight);
            AI C    = new AI(new Point(1500, 650), pattern.Straight);

            //
            Difficulty test = Difficulty.Easy;
            List<Entity> output = null;
            switch (test)
            {
                case Difficulty.Easy:
                    output = new List<Entity>()
                    {
                        A,B,C,new Asteroid(new Point(GameController.random.Next(0, 700))),null,null,
                        A,B,C,new Asteroid(new Point(GameController.random.Next(0, 700))),null,null,
                        A,B,C,new Asteroid(new Point(GameController.random.Next(0, 700))),null,null,
                        A,B,C,new Asteroid(new Point(GameController.random.Next(0, 700))),null,null,
                        A,B,C,new Asteroid(new Point(GameController.random.Next(0, 700))),null,null,
                        A,B,C,new Asteroid(new Point(GameController.random.Next(0, 700))),null,null
                    };
                    break;
                case Difficulty.Medium:
                    for(int i = 0; i < 60; ++i)
                    {
                        Entity add = null;
                        switch (GameController.random.Next(0,5))
                        {
                            case 0:
                                add = new AI(new Point(GameController.random.Next(0, 700)),pattern.Straight);
                                break;
                            case 1:
                                add = new Asteroid(new Point(GameController.random.Next(100, 600)));
                                break;
                            case 2:
                                add = A;
                                break;
                            case 3:
                                add = B;
                                break;
                            case 4:
                                add = C;
                                break;
                        }
                        output.Add(add);
                    }
                    
                    break;
                case Difficulty.Hard:
                    break;
            }
            
            

            return output;
        }
        public Entity[,] Level2()
        {
            //TODO Create list of enemies for level 2
            return null;
        }
        public Entity[,] Level3()
        {
            //TODO Create list of enemies for level 3
            return null;
        }

        //Entity[,] output = new Entity[10, 4]
        //        {

        //            { new AI(){loc = new Point(1500,10), Flightpath = pattern.Straight}, new AI(){loc = new Point(1500,20), Flightpath = pattern.Straight }, Bob , new Asteroid(new Point(1500, GameController.random.Next(0,700) )) },
        //            { new AI(){loc = new Point(1500,100), Flightpath = pattern.Straight}, new AI(){loc = new Point(1500,20), Flightpath = pattern.Straight }, Bob, new Asteroid(new Point(1500, GameController.random.Next(0,700) )) },
        //            { new AI(){loc = new Point(1500,500), Flightpath = pattern.Straight}, new AI(){loc = new Point(1500,20), Flightpath = pattern.Straight }, Bob, new Asteroid(new Point(1500, GameController.random.Next(0,700) )) },
        //            { new AI(){loc = new Point(1450,500), Flightpath = pattern.Straight}, new AI(){loc = new Point(1500,20), Flightpath = pattern.Straight }, Bob, new Asteroid(new Point(1500, GameController.random.Next(0,700) )) },
        //            { new AI(){loc = new Point(1500,450), Flightpath = pattern.Straight}, new AI(){loc = new Point(1500,20), Flightpath = pattern.Straight }, Bob, new Asteroid(new Point(1500, GameController.random.Next(0,700) )) },
        //            { new AI(){loc = new Point(1500,550), Flightpath = pattern.Straight}, new AI(){loc = new Point(1500,20), Flightpath = pattern.Straight }, Bob, new Asteroid(new Point(1500, GameController.random.Next(0,700) )) },
        //            { new AI(){loc = new Point(1500,100), Flightpath = pattern.Straight}, new AI(){loc = new Point(1500,20), Flightpath = pattern.Straight }, Bob, new Asteroid(new Point(1500, GameController.random.Next(0,700) )) },
        //            { new AI(){loc = new Point(1500,100), Flightpath = pattern.Straight}, new AI(){loc = new Point(1500,20), Flightpath = pattern.Straight }, Bob, new Asteroid(new Point(1500, GameController.random.Next(0,700) )) },
        //            { new AI(){loc = new Point(1500,100), Flightpath = pattern.Straight}, new AI(){loc = new Point(1500,20), Flightpath = pattern.Straight }, Bob, new Asteroid(new Point(1500, GameController.random.Next(0,700) )) },
        //            { new AI(){loc = new Point(1500,100), Flightpath = pattern.Straight}, new AI(){loc = new Point(1500,20), Flightpath = pattern.Straight }, Bob, new Asteroid(new Point(1500, GameController.random.Next(0,700) )) }
        //        };


    }
}
