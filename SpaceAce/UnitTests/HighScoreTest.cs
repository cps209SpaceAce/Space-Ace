using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Model
{

    [TestClass]
    class HighScoreManagerTest
    {

        [TestMethod]
        public void Update_AddScore_Pass()
        {
            HighScoreManager Test = new HighScoreManager();
            HighScore newScore = new HighScore("Bob", Level.Level_1, Difficulty.Easy, 9001, "ship.png");

            Test.Update(newScore);
            Assert.IsTrue(Test.highScores.Contains(newScore));
            
        }

        [TestMethod]
        public void Load_FromFile_Pass()
        {
            HighScoreManager Test = new HighScoreManager();
            Test.Load();
            // Need a test


        }

        [TestMethod]
        public void Save_ToFile_Pass()
        {
            HighScoreManager Test = new HighScoreManager();
            Test.Save();
            // Need a test
        }
    }
}

