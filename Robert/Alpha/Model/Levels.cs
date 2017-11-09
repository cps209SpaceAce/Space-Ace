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
            Entity[,] l = new Entity[,] 
            {
                new Normal() { }, new Normal() { },
                new Normal() { }, new Normal() { }

            };
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
