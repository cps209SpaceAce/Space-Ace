using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum PlayerAction {Up,Down,Left,Right,Fire,Bomb,Powerup}
    public enum Difficulty {Easy, Medium, Hard}
    public enum levels {Level_1, Level_2, Boss }
    class GameController
    {
        private List<Entities> current_Enemies = new List<Entities>();
        private Entities[,] enemie_Que;
        private Player player;
        private List<Bullet> player_fire = new List<Bullet>();
        private levels level;
        private GameData world;
        private Difficulty difficulty;
        private double base_Speed;

        public GameController()
        {
            //TODO: load level/save data from GameData
            //TODO: get enemies for level from Levels
        }

        public void UpdateWorld()
        {
            //loop through entites and detect collision
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
