using System;
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
        GameController gc = new GameController(Difficulty.Easy);//Level.Level_1,Difficulty.Easy
            Assert.IsTrue(gc.difficulty == Difficulty.Easy);
        Assert.IsTrue(gc.level == Level.Level_1);
        Assert.IsTrue(gc.enemie_Que != null);
        Assert.IsTrue(gc.player.lives == 3);
        Assert.IsTrue(gc.player.bombs == 3);
        Assert.IsTrue(gc.base_Speed == 1);
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
            Test.Save();
            //var fileName = @"C:\somedirectory\somefile.txt";
            //var fileName = Environment.CurrentDirectory + @"\JSON.txt";
            Assert.IsTrue(File.Exists("JSON.txt"));
            // Need a test
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
            Assert.AreEqual(Test.highScores[0], BobTest);
            //Message: Assert.AreEqual failed. 
            // Expected:< Bob: Level 1, Easy.Score: 9001 >.
            // Actual  :< Bob: Level 1, Easy.Score: 9001 >.

            // Need a test


        }


    }
}

