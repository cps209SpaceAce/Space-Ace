using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Model;

/// <header>
/// Unit tests for the Model namespace
/// </header>

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
        Assert.IsTrue(gc.player.Lives == 3);
        Assert.IsTrue(gc.player.Bombs == 3);
        //Assert.IsTrue(gc.base_Speed == 1);
        }

        [TestMethod]
        public void Invoke_updateWorld() {
            Assert.Fail();
        }

        [TestMethod]
        public void Test_Load() {
            Assert.Fail();
        }

        [TestMethod]
        public void Test_save() {
            Assert.Fail();
        }

        [TestMethod]
        public void Test_updateplayer() {
            Assert.Fail();
        }

        [TestMethod]
        public void Test_collision() {
            Assert.Fail();
        }




        // HIGHSCORE TESTS
        // VS could not find the file so i moved it here.

        [TestMethod]
        public void Update_AddScore_Pass()
        {
            HighScoreManager Test = new HighScoreManager();
            HighScore newScore = new HighScore("Bob", Level.Level_1, Difficulty.Easy, 9001, "ship.png");

            Test.Update(newScore);
            Assert.IsTrue(Test.highScores.Contains(newScore));

            File.Delete(Environment.CurrentDirectory + @"/JSON.txt");
            
        }

        [TestMethod]
        public void Save_ToFile_Pass()
        {
            HighScoreManager Test = new HighScoreManager();
            Test.highScores.Add(new HighScore("Bob", Level.Level_1, Difficulty.Easy, 9001, "ship.png"));
            Assert.IsTrue(File.Exists("JSON.txt"));

            File.Delete(Environment.CurrentDirectory + @"/JSON.txt");
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

            //Delete File
            File.Delete(Environment.CurrentDirectory + @"/JSON.txt");
        }
      
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

