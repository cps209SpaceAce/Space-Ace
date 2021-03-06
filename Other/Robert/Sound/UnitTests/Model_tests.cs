﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

using Model;

namespace SpaceAce.UnitTests
{
    [TestClass]
    public class Model_tests
    {
        [TestMethod]
        public void Make_NewGame()
        {
        GameController gc = new GameController(Difficulty.Easy, 768, 1024, false, "ship_image");//Level.Level_1,Difficulty.Easy

        Assert.IsTrue(gc.difficulty == Difficulty.Easy);
        Assert.IsTrue(gc.level == Level.Level_1);
        Assert.IsTrue(gc.enemie_Que != null);
        Assert.IsTrue(gc.player.lives == 3);
        Assert.IsTrue(gc.player.bombs == 3);
        //Assert.IsTrue(gc.base_Speed == 1);
        }

        [TestMethod]
        public void Invoke_updateWorld() { }

        [TestMethod]
        public void Test_Load() { }

        [TestMethod]
        public void Test_save() { }
        
        [TestMethod]
        public void Test_updateplayer() { }

        [TestMethod]
        public void Test_collision() { }


    

        [TestMethod]
        public void Update_AddScore_Pass()
        {
            HighScoreManager Test = new HighScoreManager();
            HighScore newScore = new HighScore("Bob", Level.Level_1, Difficulty.Easy, 9001, "ship.png");

            Test.Update(newScore);
            Assert.IsTrue(Test.highScores.Contains(newScore));

        }

        [TestMethod]
        public void Save_ToFile_Pass()
        {
            HighScoreManager Test = new HighScoreManager();
            Test.highScores.Add(new HighScore("Bob", Level.Level_1, Difficulty.Easy, 9001, "ship.png"));
            //Test.Save();
            Assert.IsTrue(File.Exists("JSON.txt"));

        }

        [TestMethod]
        public void Load_FromFile_Pass()
        {
            HighScoreManager Test = new HighScoreManager();
            HighScore BobTest = new HighScore("Bob", Level.Level_1, Difficulty.Easy, 9001, "ship.png");
            Test.highScores.Add(BobTest);
            Test.Save();
            Test.highScores.Clear();
            Test.Load();
            Assert.IsTrue(Test.highScores[Test.highScores.Count-1].Name      == "Bob");
            Assert.IsTrue(Test.highScores[Test.highScores.Count - 1].Level     == Level.Level_1 );
            Assert.IsTrue(Test.highScores[Test.highScores.Count - 1].Diff      == Difficulty.Easy );
            Assert.IsTrue(Test.highScores[Test.highScores.Count - 1].Score     == 9001 );
            Assert.IsTrue(Test.highScores[Test.highScores.Count - 1].ShipImage == "ship.png" );

            

        }

        // Robert: Add more saves and test Load
        //         Add better test for Save
        //         Need more Model Tests


            [TestMethod]
            public void  HighScore_ToString()
            {
            HighScore h = new HighScore();
            h.Name = "ha";
            Assert.IsTrue(h.ToString() == " ha          Level 1     Easy                0");

            h = new HighScore("Jojo", Level.Level_2, Difficulty.Hard, 90,  "nothing.jpg");
            Assert.IsTrue(h.ToString() == " Jojo        Level 2     Hard               90");

            h.Diff = Difficulty.Medium;
            h.Level = Level.Boss;
            Assert.IsTrue(h.ToString() == " Jojo        Boss        Medium             90");

            h = new HighScore("", Level.Level_2, Difficulty.Hard, 90, "nothing.jpg");
            Assert.IsTrue(h.Name == "Unknown");
        }
    }
}

