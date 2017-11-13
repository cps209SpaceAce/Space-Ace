using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Drawing;
using Model;

namespace SpaceAce.UnitTests
{

    [TestClass]
    public class LoadSaveTests
    {


        [TestMethod]
        public void Save_CurrentEnemies_Success()
        {
            GameController ctrl = new GameController();
            List<Entity> enemies = new List<Entity>();
            enemies.Add(new Asteroid(30, 20));
            enemies.Add(new Asteroid(10, 10));

            ctrl.current_Enemies = enemies;

            ctrl.Save("TestSave.txt");


            using (StreamReader reader = new StreamReader("TestSave.txt"))
            {

                Assert.IsTrue(reader.ReadLine() == "[enemies]");
                Assert.IsTrue(reader.ReadLine() == "asteroid,1,30,20");
                Assert.IsTrue(reader.ReadLine() == "asteroid,1,10,10");
                Assert.IsTrue(reader.ReadLine() == "[end]");
            }
        }

        [TestMethod]
        public void Save_QueEnemies_Success()
        {
            GameController ctrl = new GameController();
            List<Entity> queEnemies = new List<Entity>();
            queEnemies.Add(new Asteroid(30, 20));
            queEnemies.Add(new Asteroid(10, 10));


            ctrl.enemie_Que = queEnemies;

            ctrl.Save("TestSave.txt");


            using (StreamReader reader = new StreamReader("TestSave.txt"))
            {
                Assert.IsTrue(reader.ReadLine() == "[queuedEnemies]");
                Assert.IsTrue(reader.ReadLine() == "asteroid,1,30,20");
                Assert.IsTrue(reader.ReadLine() == "asteroid,1,10,10");
                Assert.IsTrue(reader.ReadLine() == "[end]");
            }
        }


        [TestMethod]
        public void Save_PlayerBullets_Success()
        {
            GameController ctrl = new GameController();
            List<Bullet> playerBullets = new List<Bullet>();
            playerBullets.Add(new Bullet(30, 20));
            playerBullets.Add(new Bullet(5, 6));
            playerBullets.Add(new Bullet(7, 5));

            ctrl.player_fire = playerBullets;

            ctrl.Save("TestSave.txt");


            using (StreamReader reader = new StreamReader("TestSave.txt"))
            {
                Assert.IsTrue(reader.ReadLine() == "[player]");
                Assert.IsTrue(reader.ReadLine() == "50,350,Power,3,3");
                Assert.IsTrue(reader.ReadLine() == "[end]");

                Assert.IsTrue(reader.ReadLine() == "[playerBullets]");
                Assert.IsTrue(reader.ReadLine() == "bullet,30,20");
                Assert.IsTrue(reader.ReadLine() == "bullet,5,6");
                Assert.IsTrue(reader.ReadLine() == "bullet,7,5");
                Assert.IsTrue(reader.ReadLine() == "[end]");
            }
        }

        [TestMethod]
        public void Save_PlayerData_Success()
        {
            GameController ctrl = new GameController();
            ctrl.player = new Player(40, 50, 100, 60, ctrl);

            ctrl.Save("TestSave.txt");


            using (StreamReader reader = new StreamReader("TestSave.txt"))
            {

                Assert.IsTrue(reader.ReadLine() == "[player]");
                Assert.IsTrue(reader.ReadLine() == "40,50,Power,100,60");
                Assert.IsTrue(reader.ReadLine() == "[end]");
            }
        }


        [TestMethod]
        public void Save_GameControllerData_Success()
        {
            GameController ctrl = new GameController();
            ctrl.level = Level.Level_1;
            ctrl.base_Speed = 100;
            ctrl.score = 9001;

            ctrl.Save("TestSave.txt");

            using (StreamReader reader = new StreamReader("TestSave.txt"))
            {
                Assert.IsTrue(reader.ReadLine() == "[player]");
                Assert.IsTrue(reader.ReadLine() == "50,350,Power,3,3");
                Assert.IsTrue(reader.ReadLine() == "[end]");

                Assert.IsTrue(reader.ReadLine() == "[defaults]");
                Assert.IsTrue(reader.ReadLine() == "Level_1,9001,100");
                Assert.IsTrue(reader.ReadLine() == "[end]");

            }

        }

        [TestMethod]
        public void Save_Defaults_Success()
        {
            GameController ctrl = new GameController();

            ctrl.Save("TestSave.txt");


            using (StreamReader reader = new StreamReader("TestSave.txt"))
            {
                Assert.IsTrue(reader.ReadLine() == "[player]");
                Assert.IsTrue(reader.ReadLine() == "50,350,Power,3,3");
                Assert.IsTrue(reader.ReadLine() == "[end]");

                Assert.IsTrue(reader.ReadLine() == "[defaults]");
                Assert.IsTrue(reader.ReadLine() == "Level_1,0,0");
                Assert.IsTrue(reader.ReadLine() == "[end]");
            }
        }

        //******************************************************************************* LOAD VV

        [TestMethod]
        public void Load_Player_Success()
        {
            GameController ctrl = new GameController();


            using (StreamWriter writer = new StreamWriter("TestLoad.txt"))
            {

                writer.WriteLine("[player]");
                writer.WriteLine("40,30,Power,4,8");
                writer.WriteLine("[end]");
            }

            ctrl.Load("TestLoad.txt");

            Assert.IsTrue(ctrl.player.X == 40);
            Assert.IsTrue(ctrl.player.Y == 30);
            Assert.IsTrue(ctrl.player.powerup == powerup.Power);
            Assert.IsTrue(ctrl.player.lives == 4);
            Assert.IsTrue(ctrl.player.bombs == 8);

        }

        [TestMethod]
        public void Load_Enemies_Success()
        {
            GameController ctrl = new GameController();

            using (StreamWriter writer = new StreamWriter("TestLoad.txt"))
            {

                writer.WriteLine("[enemies]");
                writer.WriteLine("asteroid,100,40,50");
                writer.WriteLine("asteroid,100,30,20");
                writer.WriteLine("[end]");
            }
            ctrl.Load("TestLoad.txt");

            Assert.IsTrue(ctrl.current_Enemies.Count == 2);
            Assert.IsTrue(ctrl.current_Enemies[0].health == 100);
            Assert.IsTrue(ctrl.current_Enemies[0].X == 40);
            Assert.IsTrue(ctrl.current_Enemies[0].Y == 50);

        }

        [TestMethod]
        public void Load_QuedEnemies_Success()
        {
            GameController ctrl = new GameController();
            using (StreamWriter writer = new StreamWriter("TestLoad.txt"))
            {
                writer.WriteLine("[queuedEnemies]");
                writer.WriteLine("asteroid,100,40,50");
                writer.WriteLine("asteroid,100,30,20");
                writer.WriteLine("[end]");
            }
            ctrl.Load("TestLoad.txt");

            Assert.IsTrue(ctrl.enemie_Que.Count == 2);
            Assert.IsTrue(ctrl.enemie_Que[0].health == 100);
            Assert.IsTrue(ctrl.enemie_Que[0].X == 40);
            Assert.IsTrue(ctrl.enemie_Que[0].Y == 50);

        }

        [TestMethod]
        public void Load_PlayerBullets_Success()
        {
            GameController ctrl = new GameController();

            using (StreamWriter writer = new StreamWriter("TestLoad.txt"))
            {
                writer.WriteLine("[playerBullets]");
                writer.WriteLine("bullet,40,50");
                writer.WriteLine("bullet,30,20");
                writer.WriteLine("bullet,10,5");
                writer.WriteLine("bullet,3,2");
                writer.WriteLine("[end]");
            }
            ctrl.Load("TestLoad.txt");

            Assert.IsTrue(ctrl.player_fire.Count == 4);
            Assert.IsTrue(ctrl.player_fire[0].X == 40);
            Assert.IsTrue(ctrl.player_fire[0].Y == 50);

            Assert.IsTrue(ctrl.player_fire[1].X == 30);
            Assert.IsTrue(ctrl.player_fire[1].Y == 20);

        }

        [TestMethod]
        public void Load_Defaults_Success()
        {
            GameController ctrl = new GameController();

            using (StreamWriter writer = new StreamWriter("TestLoad.txt"))
            {
                writer.WriteLine("[defaults]");
                writer.WriteLine("Level_2,10,1337");
                writer.WriteLine("[end]");
            }
            ctrl.Load("TestLoad.txt");

            Assert.IsTrue(ctrl.base_Speed == 1337);
            Assert.IsTrue(ctrl.score == 10);
            Assert.IsTrue(ctrl.level == Level.Level_2);

        }

        [TestMethod]
        public void Load_FullGame_Success()
        {
            GameController ctrl = new GameController();

            using (StreamWriter writer = new StreamWriter("TestLoad.txt"))
            {
                writer.WriteLine("[enemies]");
                writer.WriteLine("asteroid,1,30,20");
                writer.WriteLine("ai,200,600,Cos");
                writer.WriteLine("ai,20,20,Cos");
                writer.WriteLine("ai,80,80,Cos");
                writer.WriteLine("[end]");

                writer.WriteLine("[queuedEnemies]");
                writer.WriteLine("ai,100,200,Cos");
                writer.WriteLine("ai,23,30,Cos");
                writer.WriteLine("asteroid,1,30,30");
                writer.WriteLine("[end]");

                writer.WriteLine("[player]");
                writer.WriteLine("40,30,Power,4,8");
                writer.WriteLine("[end]");

                writer.WriteLine("[defaults]");
                writer.WriteLine("Level_2,10,1337");
                writer.WriteLine("[end]");
            }

            ctrl.Load("TestLoad.txt");

            Assert.IsTrue(ctrl.base_Speed == 1337);
            Assert.IsTrue(ctrl.score == 10);
            Assert.IsTrue(ctrl.level == Level.Level_2);
            Assert.IsTrue(ctrl.player.X == 40);
            Assert.IsTrue(ctrl.player.Y == 30);
            Assert.IsTrue(ctrl.player.powerup == powerup.Power);
            Assert.IsTrue(ctrl.player.lives == 4);
            Assert.IsTrue(ctrl.player.bombs == 8);

            Assert.IsTrue(ctrl.enemie_Que.Count == 3);
            Assert.IsTrue(ctrl.enemie_Que[0].X == 100);
            Assert.IsTrue(ctrl.enemie_Que[0].Y == 200);

            Assert.IsTrue(ctrl.current_Enemies.Count == 4);
            Assert.IsTrue(ctrl.current_Enemies[0].X == 30);
            Assert.IsTrue(ctrl.current_Enemies[0].Y == 20);

        }
    }

}
