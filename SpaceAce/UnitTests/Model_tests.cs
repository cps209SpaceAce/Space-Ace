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
    public class Model_tests
    {
        [TestMethod]
        public void Make_NewGame()
        {
        GameController gc = new GameController();//Level.Level_1,Difficulty.Easy
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


    }
}
