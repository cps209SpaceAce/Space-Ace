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
    public class BossTests
    {
        [TestMethod]
        public void Boss_Hit_Easy_Success()
        {
            Boss_Easy boss = new Boss_Easy(900, 80, 12, 2000, 2000);

            Assert.IsFalse(boss.Hit());

            boss.X = 100;
            boss.startup = false;
            Assert.IsFalse(boss.Hit());
            Assert.IsTrue(boss.health == 11);

            boss.health = 1;
            Assert.IsTrue(boss.Hit());
            Assert.IsFalse(boss.alive);
        }

        [TestMethod]
        public void Boss_Hit_Medium_Success()
        {
            Boss_Medium boss = new Boss_Medium(900, 80, 12, 2000, 2000);

            Assert.IsFalse(boss.Hit());

            boss.X = 100;
            boss.startup = false;
            Assert.IsFalse(boss.Hit());
            Assert.IsTrue(boss.health == 11);

            boss.health = 1;
            Assert.IsTrue(boss.Hit());
            Assert.IsFalse(boss.alive);
        }

        [TestMethod]
        public void Boss_Hit_Hard_Success()
        {
            Boss_Hard boss = new Boss_Hard(900, 80, 12, 2000, 2000);

            Assert.IsFalse(boss.Hit());

            boss.X = 100;
            boss.startup = false;
            Assert.IsFalse(boss.Hit());
            Assert.IsTrue(boss.health == 11);

            boss.health = 1;
            Assert.IsTrue(boss.Hit());
            Assert.IsFalse(boss.alive);
        }

        [TestMethod]
        public void Boss_UpdatePosition_Easy_IsEntering_Skip()
        {
            Boss_Easy boss = new Boss_Easy(3000, 80, 12, 2000, 2000);
            boss.UpdatePosition();

            Assert.IsTrue(boss.X == 2996);
            Assert.IsTrue(boss.Y == 80);
        }

        [TestMethod]
        public void Boss_UpdatePosition_Easy_StartState_Success()
        {
            Boss_Easy boss = new Boss_Easy(100, 80, 12, 2000, 2000);
            boss.isEntering = false;

            boss.UpdatePosition();

            Assert.IsTrue(boss.currentState == MState.Start);
            Assert.IsTrue(boss.hitbox.X == Convert.ToInt32(boss.X));
            Assert.IsTrue(boss.hitbox.Y == Convert.ToInt32(boss.Y));

        }

        [TestMethod]
        public void Boss_UpdatePosition_Medium_IsEntering_Skip()
        {
            Boss_Medium boss = new Boss_Medium(900, 80, 12, 2000, 2000);
            boss.UpdatePosition();

            Assert.IsTrue(boss.X == 896);
            Assert.IsTrue(boss.Y == 80);
        }

        [TestMethod]
        public void Boss_UpdatePosition_Medium_Success()
        {
            Assert.Fail();

        }

        [TestMethod]
        public void Boss_UpdatePosition_Hard_IsEntering_Skip()
        {
            Boss_Hard boss = new Boss_Hard(3000, 80, 12, 2000, 2000);
            boss.UpdatePosition();

            Assert.IsTrue(boss.X == 2996);
            Assert.IsTrue(boss.Y == 80);
        }

        [TestMethod]
        public void Boss_UpdatePosition_Medium_StartState_Success()
        {
            Boss_Medium boss = new Boss_Medium(100, 80, 12, 2000, 2000);
            
            boss.UpdatePosition();

            Assert.IsTrue(boss.state == State.Start);
            Assert.IsTrue(boss.hitbox.X == boss.X);
            Assert.IsTrue(boss.hitbox.Y == boss.Y);

        }

        [TestMethod]
        public void Boss_UpdatePosition_Hard_Success()
        {
            Boss_Hard boss = new Boss_Hard(3000, 80, 12, 2000, 2000);
            boss.UpdatePosition();

            Assert.IsTrue(boss.state == State.Start);
            Assert.IsTrue(boss.hitbox.X == boss.X);
            Assert.IsTrue(boss.hitbox.Y == boss.Y);
            Assert.IsFalse(boss.fired_slanted_targeted_shot);

            boss = new Boss_Hard(100, 80, 12, 2000, 2000); ;
            boss.UpdatePosition();

            Assert.IsTrue(boss.X > 100);
            Assert.IsTrue(boss.Y > 80);
            Assert.IsTrue(boss.dir == Direction.Left);

            
            boss.UpdatePosition();

            Assert.IsTrue(boss.dir == Direction.Right);

            boss.UpdatePosition();

            Assert.IsTrue(boss.dir == Direction.Left);

            boss.cooldown = 1;
            boss.UpdatePosition();

            Assert.IsTrue(boss.action);
            Assert.IsTrue(boss.fired_slanted_targeted_shot);
            Assert.IsTrue(boss.cooldown == 50);


        }


    }
}
