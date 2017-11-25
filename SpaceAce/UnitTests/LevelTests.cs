using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

namespace SpaceAce.UnitTests
{
    [TestClass]
    public class LevelTests
    {
        [TestMethod]
        public void Level_returnPowerUp()
        {
            Assert.IsTrue(Levels.Level_returnPowerUp() is Powerup);
        }

        [TestMethod]
        public void Level_returnEntity_Lv1_Easy()
        {
            Entity result = Levels.Level_reuturnEntity(Difficulty.Easy, Level.Level_1);
            Assert.IsTrue(result is AI || result is Asteroid);
        }

        [TestMethod]
        public void Level_returnEntity_Lv2_Easy()
        {
            Entity result = Levels.Level_reuturnEntity(Difficulty.Easy, Level.Level_2);
            Assert.IsTrue(result is AI || result is Asteroid || result is Formation);
        }

        [TestMethod]
        public void Level_returnEntity_Lv1_Medium()
        {
            Entity result = Levels.Level_reuturnEntity(Difficulty.Medium, Level.Level_1);
            Assert.IsTrue(result is Asteroid || result is Formation);
        }

        [TestMethod]
        public void Level_returnEntity_Lv2_Medium()
        {
            Entity result = Levels.Level_reuturnEntity(Difficulty.Medium, Level.Level_2);
            Assert.IsTrue(result is Asteroid || result is Formation || result is Tracker);
        }

        [TestMethod]
        public void Level_returnEntity_Lv1_Hard()
        {
            Entity result = Levels.Level_reuturnEntity(Difficulty.Hard, Level.Level_1);
            Assert.IsTrue(result is Asteroid || result is Formation || result is Tracker);
        }

        [TestMethod]
        public void Level_returnEntity_Lv2_Hard()
        {
            Entity result = Levels.Level_reuturnEntity(Difficulty.Hard, Level.Level_2);
            Assert.IsTrue(result is AI || result is Mine || result is Tracker);
        }

        [TestMethod]
        public void Level_returnEntity_Boss_Hard()
        {
            Entity result = Levels.Level_reuturnEntity(Difficulty.Hard, Level.Boss);
            Assert.IsTrue(result is Boss);
        }

        [TestMethod]
        public void Level_returnEntity_Boss_Medium()
        {
            Entity result = Levels.Level_reuturnEntity(Difficulty.Medium, Level.Boss);
            Assert.IsTrue(result is Boss);
        }

        [TestMethod]
        public void Level_returnEntity_Boss_Easy()
        {
            Entity result = Levels.Level_reuturnEntity(Difficulty.Easy, Level.Boss);
            Assert.IsTrue(result is Boss);
        }
    }
}
