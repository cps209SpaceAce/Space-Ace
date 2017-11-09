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

        public Entity[,] Level1()
        {
            //TODO Create list of enemies for level 1
            AI Bob = new AI() { loc = new Point(1500, 500), Flightpath = pattern.Straight };
            Entity[,] output = new Entity[10, 4] 
                {   
                    { new AI(){loc = new Point(1500,10), Flightpath = pattern.Straight}, new AI(){loc = new Point(1500,20), Flightpath = pattern.Straight }, Bob , new Asteroid(new Point(1500, GameController.random.Next(0,700) )) }, 
                    { new AI(){loc = new Point(1500,100), Flightpath = pattern.Straight}, new AI(){loc = new Point(1500,20), Flightpath = pattern.Straight }, Bob, new Asteroid(new Point(1500, GameController.random.Next(0,700) )) },                    
                    { new AI(){loc = new Point(1500,500), Flightpath = pattern.Straight}, new AI(){loc = new Point(1500,20), Flightpath = pattern.Straight }, Bob, new Asteroid(new Point(1500, GameController.random.Next(0,700) )) },
                    { new AI(){loc = new Point(1450,500), Flightpath = pattern.Straight}, new AI(){loc = new Point(1500,20), Flightpath = pattern.Straight }, Bob, new Asteroid(new Point(1500, GameController.random.Next(0,700) )) },
                    { new AI(){loc = new Point(1500,450), Flightpath = pattern.Straight}, new AI(){loc = new Point(1500,20), Flightpath = pattern.Straight }, Bob, new Asteroid(new Point(1500, GameController.random.Next(0,700) )) },
                    { new AI(){loc = new Point(1500,550), Flightpath = pattern.Straight}, new AI(){loc = new Point(1500,20), Flightpath = pattern.Straight }, Bob, new Asteroid(new Point(1500, GameController.random.Next(0,700) )) },
                    { new AI(){loc = new Point(1500,100), Flightpath = pattern.Straight}, new AI(){loc = new Point(1500,20), Flightpath = pattern.Straight }, Bob, new Asteroid(new Point(1500, GameController.random.Next(0,700) )) },
                    { new AI(){loc = new Point(1500,100), Flightpath = pattern.Straight}, new AI(){loc = new Point(1500,20), Flightpath = pattern.Straight }, Bob, new Asteroid(new Point(1500, GameController.random.Next(0,700) )) },
                    { new AI(){loc = new Point(1500,100), Flightpath = pattern.Straight}, new AI(){loc = new Point(1500,20), Flightpath = pattern.Straight }, Bob, new Asteroid(new Point(1500, GameController.random.Next(0,700) )) },
                    { new AI(){loc = new Point(1500,100), Flightpath = pattern.Straight}, new AI(){loc = new Point(1500,20), Flightpath = pattern.Straight }, Bob, new Asteroid(new Point(1500, GameController.random.Next(0,700) )) }
                };
            List<List<Entity>> oupt = new List<List<Entity>>() { };


            return null;
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

    }
}
