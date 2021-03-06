﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Model
{
    /// <summary>
    /// Class to hold all information related to the game at a point in time. For saving and loading
    /// </summary>
    class GameData
    {
        public Player Player { get; set; }
        public int Level { get; set; }
        public List<Entity> Enemies { get; set; }
        public Entity[,] Schedule { get; set; }
        public int Score { get; set; }

        public void Save()
        {
            //TODO: save info to disk
        }

        public void Load()
        {
            //TODO: load gamedata from disk
        }


    }
}
