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
        public void Save_Enemies_Success()
        {
            GameController ctrl = new GameController();
            List<Entity> enemies = new List<Entity>();
            Asteroid a = new Asteroid(30, 20);
            a.health = 1;
            enemies.Add(a);
            enemies.Add(new Formation(10, 10, pattern.Sindown));
            enemies.Add(new Mine(10, 10, pattern.Straight));
            enemies.Add(new Tracker(10, 10, pattern.Straight));
            enemies.Add(new Powerup(10, 10, PowerUp.TripleShot));
            enemies.Add(new AI(10, 10, pattern.Straight));
            enemies.Add(new Slanted_Bullet(10, 10, 20));
            enemies.Add(new Wandering_Bullet(10, 10, pattern.Straight));

            ctrl.current_Enemies = enemies;

            ctrl.Save("TestSave.txt");


            using (StreamReader reader = new StreamReader("TestSave.txt"))
            {
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();

                Assert.IsTrue(reader.ReadLine() == "[enemies]");
                Assert.IsTrue(reader.ReadLine() == "asteroid,1,30,20");
                Assert.IsTrue(reader.ReadLine() == "formation,10,10,Sindown");
                Assert.IsTrue(reader.ReadLine() == "mine,10,10");
                Assert.IsTrue(reader.ReadLine() == "tracker,10,10");
                Assert.IsTrue(reader.ReadLine() == "powerup,10,10,TripleShot");
                Assert.IsTrue(reader.ReadLine() == "ai,10,10,Straight");
                Assert.IsTrue(reader.ReadLine() == "bullet,slanted,10,10,20");
                Assert.IsTrue(reader.ReadLine() == "bullet,wandering,10,10,10,10,Straight,0");
                Assert.IsTrue(reader.ReadLine() == "[end]");
            }
        }

        [TestMethod]
        public void Save_Enemies_NoEnemies_EOF()
        {
            GameController ctrl = new GameController();
            List<Entity> enemies = new List<Entity>();

            ctrl.current_Enemies = enemies;

            ctrl.Save("TestSave.txt");

            using (StreamReader reader = new StreamReader("TestSave.txt"))
            {
                Assert.IsTrue(reader.ReadLine() == "[defaults]");
                Assert.IsTrue(reader.ReadLine() == "Level_1,0,0,0,False");
                Assert.IsTrue(reader.ReadLine() == "[end]");
                Assert.IsTrue(reader.ReadLine() == "[player]");
                Assert.IsTrue(reader.ReadLine() == "50,350,Empty,3,3,False,0,False,ship.png,False,False,False,False,False");
                Assert.IsTrue(reader.ReadLine() == "[end]");

                Assert.IsTrue(reader.EndOfStream);
            }
        }

        [TestMethod]
        public void Save_Enemies_NullList_Skip()
        {
            GameController ctrl = new GameController();
            ctrl.current_Enemies = null;

            ctrl.Save("TestSave.txt");

            using (StreamReader reader = new StreamReader("TestSave.txt"))
            {
                Assert.IsTrue(reader.ReadLine() == "[defaults]");
                Assert.IsTrue(reader.ReadLine() == "Level_1,0,0,0,False");
                Assert.IsTrue(reader.ReadLine() == "[end]");
                Assert.IsTrue(reader.ReadLine() == "[player]");
                Assert.IsTrue(reader.ReadLine() == "50,350,Empty,3,3,False,0,False,ship.png,False,False,False,False,False");
                Assert.IsTrue(reader.ReadLine() == "[end]");

                Assert.IsTrue(reader.EndOfStream);
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
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();


                Assert.IsTrue(reader.ReadLine() == "[playerBullets]");
                Assert.IsTrue(reader.ReadLine() == "bullet,normal,30,20");
                Assert.IsTrue(reader.ReadLine() == "bullet,normal,5,6");
                Assert.IsTrue(reader.ReadLine() == "bullet,normal,7,5");
                Assert.IsTrue(reader.ReadLine() == "[end]");
            }
        }

        [TestMethod]
        public void Save_PlayerBullets_NoBullets_EOF()
        {
            GameController ctrl = new GameController();
            List<Bullet> playerBullets = new List<Bullet>();

            ctrl.player_fire = playerBullets;

            ctrl.Save("TestSave.txt");


            using (StreamReader reader = new StreamReader("TestSave.txt"))
            {
                Assert.IsTrue(reader.ReadLine() == "[defaults]");
                Assert.IsTrue(reader.ReadLine() == "Level_1,0,0,0,False");
                Assert.IsTrue(reader.ReadLine() == "[end]");
                Assert.IsTrue(reader.ReadLine() == "[player]");
                Assert.IsTrue(reader.ReadLine() == "50,350,Empty,3,3,False,0,False,ship.png,False,False,False,False,False");
                Assert.IsTrue(reader.ReadLine() == "[end]");

                Assert.IsTrue(reader.EndOfStream);
            }
        }

        [TestMethod]
        public void Save_PlayerBullets_NullList_Exception()
        {
            GameController ctrl = new GameController();

            ctrl.player_fire = null;

            using (StreamReader reader = new StreamReader("TestSave.txt"))
            {
                Assert.IsTrue(reader.ReadLine() == "[defaults]");
                Assert.IsTrue(reader.ReadLine() == "Level_1,0,0,0,False");
                Assert.IsTrue(reader.ReadLine() == "[end]");
                Assert.IsTrue(reader.ReadLine() == "[player]");
                Assert.IsTrue(reader.ReadLine() == "50,350,Empty,3,3,False,0,False,ship.png,False,False,False,False,False");
                Assert.IsTrue(reader.ReadLine() == "[end]");

                Assert.IsTrue(reader.EndOfStream);
            }

        }


        [TestMethod]
        public void Save_PlayerData_Success()
        {
            GameController ctrl = new GameController();
            ctrl.player = new Player(40, 50, 100, 60, ctrl, "player1.png");
            ctrl.player.powerUpCounter = 3.44;
            ctrl.player.isPoweredUp = true;


            ctrl.Save("TestSave.txt");


            using (StreamReader reader = new StreamReader("TestSave.txt"))
            {
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();

                Assert.IsTrue(reader.ReadLine() == "[player]");
                Assert.IsTrue(reader.ReadLine() == "40,50,Empty,100,60,True,3.44,False,player1.png,False,False,False,False,False");
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
            ctrl.gameLevelTimer = 99.9;

            ctrl.Save("TestSave.txt");

            using (StreamReader reader = new StreamReader("TestSave.txt"))
            {
                Assert.IsTrue(reader.ReadLine() == "[defaults]");
                Assert.IsTrue(reader.ReadLine() == "Level_1,9001,100,99.9,False");
                Assert.IsTrue(reader.ReadLine() == "[end]");

                Assert.IsTrue(reader.ReadLine() == "[player]");
                Assert.IsTrue(reader.ReadLine() == "50,350,Empty,3,3,False,0,False,ship.png,False,False,False,False,False");
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

                Assert.IsTrue(reader.ReadLine() == "[defaults]");
                Assert.IsTrue(reader.ReadLine() == "Level_1,0,0,0,False");
                Assert.IsTrue(reader.ReadLine() == "[end]");

                Assert.IsTrue(reader.ReadLine() == "[player]");
                Assert.IsTrue(reader.ReadLine() == "50,350,Empty,3,3,False,0,False,ship.png,False,False,False,False,False");
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
                writer.WriteLine("40,30,ExtraSpeed,4,8,False,2.34,True,ship.png,False,False,True,False,True");
                writer.WriteLine("[end]");
            }

            ctrl.Load("TestLoad.txt");

            Assert.IsTrue(ctrl.player.X == 40);
            Assert.IsTrue(ctrl.player.Y == 30);
            Assert.IsTrue(ctrl.player.cheating == true);
            Assert.IsTrue(ctrl.player.powerup == PowerUp.ExtraSpeed);
            Assert.IsTrue(ctrl.player.Lives == 4);
            Assert.IsTrue(ctrl.player.Bombs == 8);
            Assert.IsTrue(ctrl.player.extraSpeed == true);
            Assert.IsTrue(ctrl.player.isInvincible == true);


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
                writer.WriteLine("bullet,slated,20,20,20");
                writer.WriteLine("bullet,normal,20,20");


                writer.WriteLine("[end]");
            }
            ctrl.Load("TestLoad.txt");

            Assert.IsTrue(ctrl.current_Enemies.Count == 4);
            Assert.IsTrue(ctrl.current_Enemies[0].health == 100);
            Assert.IsTrue(ctrl.current_Enemies[0].X == 40);
            Assert.IsTrue(ctrl.current_Enemies[0].Y == 50);

        }


        [TestMethod]
        public void Load_PlayerBullets_Success()
        {
            GameController ctrl = new GameController();

            using (StreamWriter writer = new StreamWriter("TestLoad.txt"))
            {
                writer.WriteLine("[playerBullets]");
                writer.WriteLine("bullet,normal,40,50");
                writer.WriteLine("bullet,normal,30,20");
                writer.WriteLine("bullet,slanted,90,90,9.7");
                writer.WriteLine("bullet,wandering,100,100,40,40,Cos,8");
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
                writer.WriteLine("Boss,1337,10,92.1,True");
                writer.WriteLine("[end]");
            }
            ctrl.Load("TestLoad.txt");

            Assert.IsTrue(ctrl.score == 1337);
            Assert.IsTrue(ctrl.base_Speed == 10);
            Assert.IsTrue(ctrl.gameLevelTimer == 92.1);
            Assert.IsTrue(ctrl.level == Level.Boss);
            Assert.IsTrue(ctrl.BossIsSpawned == true);


        }

        [TestMethod]
        public void Load_FullGame_Success()
        {
            GameController ctrl = new GameController();

            using (StreamWriter writer = new StreamWriter("TestLoad.txt"))
            {
                writer.WriteLine("[defaults]");
                writer.WriteLine("Level_1,1337,10,92.1,False");
                writer.WriteLine("[end]");

                writer.WriteLine("[player]");
                writer.WriteLine("40,30,RapidFire,4,8,True,1.6,False,ship.png,False,False,True,True,True");
                writer.WriteLine("[end]");

                writer.WriteLine("[playerBullets]");
                writer.WriteLine("bullet,normal,80,80");
                writer.WriteLine("bullet,slanted,90,90,9.7");
                writer.WriteLine("bullet,wandering,100,100,40,40,Sin,8");
                writer.WriteLine("[end]");

                writer.WriteLine("[enemies]");
                writer.WriteLine("asteroid,1,30,20");
                writer.WriteLine("ai,200,600,Cos");
                writer.WriteLine("bullet,normal,20,20");
                writer.WriteLine("bullet,slanted,20,20,90");

                writer.WriteLine("formation,80,80,Cos");
                writer.WriteLine("ai,80,80,Cos");
                writer.WriteLine("tracker,80,80");
                writer.WriteLine("powerup,80,80,RapidFire");
                writer.WriteLine("mine,80,80");
                writer.WriteLine("[end]");



            }

            ctrl.Load("TestLoad.txt");

            Assert.IsTrue(ctrl.base_Speed == 10);
            Assert.IsTrue(ctrl.gameLevelTimer == 92.1);
            Assert.IsTrue(ctrl.score == 1337);
            Assert.IsTrue(ctrl.level == Level.Level_1);
            Assert.IsTrue(ctrl.BossIsSpawned == false);

            Assert.IsTrue(ctrl.player.X == 40);
            Assert.IsTrue(ctrl.player.Y == 30);
            Assert.IsTrue(ctrl.player.powerup == PowerUp.RapidFire);
            Assert.IsTrue(ctrl.player.Lives == 4);
            Assert.IsTrue(ctrl.player.Bombs == 8);
            Assert.IsTrue(ctrl.player.isPoweredUp == true);
            Assert.IsTrue(ctrl.player.triple == false);
            Assert.IsTrue(ctrl.player.extraSpeed == true);
            Assert.IsTrue(ctrl.player.wanderingbullets == false);
            Assert.IsTrue(ctrl.player.rapid_fire == true);


            Assert.IsTrue(ctrl.current_Enemies.Count == 9);
            Assert.IsTrue(ctrl.current_Enemies[0].X == 30);
            Assert.IsTrue(ctrl.current_Enemies[0].Y == 20);

            Assert.IsTrue(ctrl.player_fire.Count == 3);
            Assert.IsTrue(ctrl.player_fire[0].X == 80);
            Assert.IsTrue(ctrl.player_fire[0].Y == 80);

        }


        [TestMethod]
        public void Load_TriplePowerup_Success()
        {
            GameController ctrl = new GameController();

            using (StreamWriter writer = new StreamWriter("TestLoad.txt"))
            {
                writer.WriteLine("[defaults]");
                writer.WriteLine("Level_1,1337,10,92.1,False");
                writer.WriteLine("[end]");

                writer.WriteLine("[player]");
                writer.WriteLine("40,30,Empty,4,8,True,1.6,False,ship.png,True,False,True,False,True");
                writer.WriteLine("[end]");

            }

            ctrl.Load("TestLoad.txt");

            
            Assert.IsTrue(ctrl.player.isPoweredUp == true);
            Assert.IsTrue(ctrl.player.triple == true);
            Assert.IsTrue(ctrl.player.extraSpeed == true);
            Assert.IsTrue(ctrl.player.wanderingbullets == false);
            Assert.IsTrue(ctrl.player.rapid_fire == false);

        }

        [TestMethod]
        public void Load_HelixPowerup_Success()
        {
            GameController ctrl = new GameController();

            using (StreamWriter writer = new StreamWriter("TestLoad.txt"))
            {
                writer.WriteLine("[defaults]");
                writer.WriteLine("Level_1,1337,10,92.1,False");
                writer.WriteLine("[end]");

                writer.WriteLine("[player]");
                writer.WriteLine("40,30,Empty,4,8,True,1.6,False,ship.png,False,True,False,False,False");
                writer.WriteLine("[end]");

            }

            ctrl.Load("TestLoad.txt");


            Assert.IsTrue(ctrl.player.isPoweredUp == true);
            Assert.IsTrue(ctrl.player.triple == false);
            Assert.IsTrue(ctrl.player.extraSpeed == false);
            Assert.IsTrue(ctrl.player.wanderingbullets == true);
            Assert.IsTrue(ctrl.player.rapid_fire == false);

        }

        [TestMethod]
        public void Load_UnknownEntity_Excepttion()
        {
            GameController ctrl = new GameController();

            using (StreamWriter writer = new StreamWriter("TestLoad.txt"))
            {
                writer.WriteLine("[defaults]");
                writer.WriteLine("Level_2,1337,10,92.1,False");
                writer.WriteLine("[end]");
                writer.WriteLine("[player]");
                writer.WriteLine("40,30,RapidFire,4,8,True,1.6,False,ship.png,False,False,False,False,False");
                writer.WriteLine("[end]");

                writer.WriteLine("[enemies]");
                writer.WriteLine("unknown,1,30,20");
                writer.WriteLine("[end]");
            }

            try
            {
                ctrl.Load("TestLoad.txt");
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message == "Enemy type Unknown.");
            }

        }

        [TestMethod]
        public void Load_NoFile()
        {
            File.Delete("TestLoad.txt");
            GameController ctrl = new GameController();
            ctrl.Load("TestLoad.txt");

            Assert.IsTrue(File.Exists("TestLoad.txt"));
        }

        [TestMethod]
        public void Save_Boss_Easy_Success()
        {
            GameController ctrl = new GameController();
            ctrl.level = Level.Boss;
            ctrl.BossIsSpawned = true;
            ctrl.current_Enemies.Add(new Boss_Easy(100,20,12,2000,2000));

            ctrl.Save("TestSave.txt");

            using (StreamReader reader = new StreamReader("TestSave.txt"))
            {
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();

                Assert.IsTrue(reader.ReadLine() == "[enemies]");
                Assert.IsTrue(reader.ReadLine() == "boss,easy,100,20,12,Start,True,False,1");
                Assert.IsTrue(reader.ReadLine() == "[end]");
            }
        }

        [TestMethod]
        public void Save_Boss_Medium_Success()
        {
            GameController ctrl = new GameController();
            ctrl.level = Level.Boss;
            ctrl.BossIsSpawned = true;
            ctrl.current_Enemies.Add(new Boss_Medium(100, 20, 12, 2000, 2000));

            ctrl.Save("TestSave.txt");

            using (StreamReader reader = new StreamReader("TestSave.txt"))
            {
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();

                Assert.IsTrue(reader.ReadLine() == "[enemies]");
                Assert.IsTrue(reader.ReadLine() == "boss,medium,100,20,12,Start");
                Assert.IsTrue(reader.ReadLine() == "[end]");
            }
        }

        [TestMethod]
        public void Save_Boss_Hard_Success()
        {
            GameController ctrl = new GameController();
            ctrl.level = Level.Boss;
            ctrl.BossIsSpawned = true;
            ctrl.current_Enemies.Add(new Boss_Hard(100, 20, 12, 2000, 2000));

            ctrl.Save("TestSave.txt");

            using (StreamReader reader = new StreamReader("TestSave.txt"))
            {
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();

                Assert.IsTrue(reader.ReadLine() == "[enemies]");
                Assert.IsTrue(reader.ReadLine() == "boss,hard,100,20,12,Start,Left,0");
                Assert.IsTrue(reader.ReadLine() == "[end]");
            }
        }

        [TestMethod]
        public void Load_Boss_Easy_Success()
        {
            GameController ctrl = new GameController();

            using (StreamWriter writer = new StreamWriter("TestLoad.txt"))
            {
                writer.WriteLine("[defaults]");
                writer.WriteLine("Boss,1337,10,92.1,True");
                writer.WriteLine("[end]");
                writer.WriteLine("[player]");
                writer.WriteLine("40,30,RapidFire,4,8,True,1.6,False,ship.png,False,False,False,False,False");
                writer.WriteLine("[end]");

                writer.WriteLine("[enemies]");
                writer.WriteLine("boss,easy,100,20,12,Start,True,False,1");
                writer.WriteLine("[end]");
            }

            ctrl.Load("TestLoad.txt");

            Assert.IsTrue(ctrl.BossIsSpawned);
            Assert.IsTrue(ctrl.level == Level.Boss);
            Assert.IsTrue(ctrl.current_Enemies[0] is Boss_Easy);
            Assert.IsTrue((ctrl.current_Enemies[0] as Boss_Easy).X == 100);
            Assert.IsTrue((ctrl.current_Enemies[0] as Boss_Easy).Y == 20);
            Assert.IsTrue((ctrl.current_Enemies[0] as Boss_Easy).health == 12);
            Assert.IsTrue((ctrl.current_Enemies[0] as Boss_Easy).currentState == MState.Start);
            Assert.IsTrue((ctrl.current_Enemies[0] as Boss_Easy).isEntering);
            Assert.IsFalse((ctrl.current_Enemies[0] as Boss_Easy).goingBackwards);
            Assert.IsTrue((ctrl.current_Enemies[0] as Boss_Easy).dir == 1);


        }

        [TestMethod]
        public void Load_Boss_Medium_Success()
        {
            GameController ctrl = new GameController();

            using (StreamWriter writer = new StreamWriter("TestLoad.txt"))
            {
                writer.WriteLine("[defaults]");
                writer.WriteLine("Boss,1337,10,92.1,True");
                writer.WriteLine("[end]");
                writer.WriteLine("[player]");
                writer.WriteLine("40,30,RapidFire,4,8,True,1.6,False,ship.png,False,False,False,False,False");
                writer.WriteLine("[end]");

                writer.WriteLine("[enemies]");
                writer.WriteLine("boss,medium,100,20,12,Start");
                writer.WriteLine("[end]");
            }

            ctrl.Load("TestLoad.txt");

            Assert.IsTrue(ctrl.BossIsSpawned);
            Assert.IsTrue(ctrl.level == Level.Boss);
            Assert.IsTrue((ctrl.current_Enemies[0] as Boss_Medium).X == 100);
            Assert.IsTrue((ctrl.current_Enemies[0] as Boss_Medium).Y == 20);
            Assert.IsTrue(ctrl.current_Enemies[0] is Boss_Medium);
            Assert.IsTrue((ctrl.current_Enemies[0] as Boss_Medium).health == 12);
            Assert.IsTrue((ctrl.current_Enemies[0] as Boss_Medium).state == State.Start);
        }

        [TestMethod]
        public void Load_Boss_Hard_Success()
        {
            GameController ctrl = new GameController();

            using (StreamWriter writer = new StreamWriter("TestLoad.txt"))
            {
                writer.WriteLine("[defaults]");
                writer.WriteLine("Boss,1337,10,92.1,True");
                writer.WriteLine("[end]");
                writer.WriteLine("[player]");
                writer.WriteLine("40,30,RapidFire,4,8,True,1.6,False,ship.png,False,False,False,False,False");
                writer.WriteLine("[end]");

                writer.WriteLine("[enemies]");
                writer.WriteLine("boss,hard,100,20,12,Start,Left,0");
                writer.WriteLine("[end]");
            }

            ctrl.Load("TestLoad.txt");

            Assert.IsTrue(ctrl.BossIsSpawned);
            Assert.IsTrue(ctrl.level == Level.Boss);
            Assert.IsTrue((ctrl.current_Enemies[0] as Boss_Hard).X == 100);
            Assert.IsTrue((ctrl.current_Enemies[0] as Boss_Hard).Y == 20);
            Assert.IsTrue(ctrl.current_Enemies[0] is Boss_Hard);
            Assert.IsTrue((ctrl.current_Enemies[0] as Boss_Hard).health == 12);
            Assert.IsTrue((ctrl.current_Enemies[0] as Boss_Hard).state == State.Start);
            Assert.IsTrue((ctrl.current_Enemies[0] as Boss_Hard).x_axis == 0);
            Assert.IsTrue((ctrl.current_Enemies[0] as Boss_Hard).dir == Direction.Left);

        }

    }

}
