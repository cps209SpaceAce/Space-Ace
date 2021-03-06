﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace SpaceAce.HighScore
{
    
    [TestClass]
    class HighScoreManagerTest
    {

        [TestMethod]
        public void Update_AddScore_Pass()
        {
            HighScoreManager Test = new HighScoreManager();
            HighScore newScore = new HighScore()
            {
                Name = "Bob",
                Level = Level.Level_1,
                Diff = Difficulty.Easy,
                Score = 9001,
                shipImage = "ship.png"
            };

            Test.Update(newScore);
            Assert.IsTrue(Test.highScores.Contains(newScore));
            
        }

        [TestMethod]
        public void Load_FromFile_Pass()
        {
            HighScoreManager Test = new HighScoreManager();
            string loadString = File.ReadAllText(Environment.CurrentDirectory + @"\JSON.txt");

            List<HighScore> loadHighScores = new JavaScriptSerializer().Deserialize<List<HighScore>>(loadString);
            Test.highScores = loadHighScores;


        }

        [TestMethod]
        public void Save_ToFile_Pass()
        {
            HighScoreManager Test = new HighScoreManager();
            string json = new JavaScriptSerializer().Serialize(Test.highScores);

            File.WriteAllText(Environment.CurrentDirectory + @"\JSON.txt", json);

        }
    }
}

