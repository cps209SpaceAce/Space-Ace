﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Model
{
    public enum PlayerAction {Up,Down,Left,Right,Fire,Bomb,Powerup}
    public enum Difficulty {Easy, Medium, Hard}
    public enum levels {Level_1, Level_2, Boss }
    class GameController
    {
        public List<Entity> current_Enemies = new List<Entity>();
        public Entity[,] enemie_Que;
        public Player player;
        public List<Bullet> player_fire = new List<Bullet>();
        public levels level;
        public GameData world;
        public Difficulty difficulty;
        public double base_Speed;

        public GameController()
        {
            //TODO: load level/save data from GameData
            //TODO: get enemies for level from Levels
        }

        public void UpdateWorld()
        {
            //loop through entites and detect collision
        }

        public void Save(string fileName)
        {
            //TODO: run for loop and collect all strings generated from each object in world
            //format of string: TypeOfEntity,instance variable1, instance variable2, instance variable3, instance variable4 ... so on
            //                  TypeOfEntity,instance variable1, instance variable2, instance variable3, instance variable4 ... so on

            //TODO: Write each line to file, leave GameController class at end             
        }
        public GameController Load(string fileName)
        {
            //TODO: make check if file exists, if no file exists, create world with default settings

            //TODO: read from file and create entities with the specifications found in the file

            //Returns new GameController class with all the specifications in the file
            return new GameController();
        }

        public void updatePlayer(PlayerAction action)
        {
            //TODO: call correct method on player based on action
        }
    }
}
