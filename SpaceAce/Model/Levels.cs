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
            AI Bob  = new AI(new Point(1500, 250), pattern.Straight);
            AI Fred = new AI(new Point(1500, 400), pattern.Straight);
            AI G    = new AI(new Point(1500, 650), pattern.Straight);
            
            List<Entity> output = new List<Entity>() {
                Bob, Fred, G,new Asteroid(new Point(GameController.random.Next(0, 700))),
                Bob, Fred, G,new Asteroid(new Point(GameController.random.Next(0, 700))),
                Bob, Fred, G,new Asteroid(new Point(GameController.random.Next(0, 700))),
                Bob, Fred, G,new Asteroid(new Point(GameController.random.Next(0, 700))),
                Bob, Fred, G,new Asteroid(new Point(GameController.random.Next(0, 700))),
                Bob, Fred, G,new Asteroid(new Point(GameController.random.Next(0, 700)))};
            

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
