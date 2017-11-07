using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    enum PlayerAction {Up,Down,Left,Right,Fire,Bomb,Powerup}
    class GameController
    {
        private List<Entities> enemies = new List<Entities>();
        private Entities[,] que;
        private Player player;
        private int level;
        private GameData world;

        public GameController()
        {
            //TODO: load level/save data from GameData
            //TODO: get enemies for level from Levels
        }

        public void Save()
        {
            // TODO: save game state to GameData and save that to disk
        }
        public void Load()
        {
            // TODO: load a state form GameData
        }

        public void updatePlayer(PlayerAction action)
        {
            //TODO: call correct method on player based on action
        }
    }
}
