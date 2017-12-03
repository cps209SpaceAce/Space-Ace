using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

/// <header>
/// Unit tests for bosses
/// </header>

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
        public void Boss_Hit_StartupTrue_Success()
        {
            Boss_Easy boss = new Boss_Easy(600, 80, 12, 2000, 2000);
            boss.startup = true;
            boss.X = 500;
            Assert.IsFalse(boss.Hit());
            Assert.IsFalse(boss.startup);
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

            boss.health = 4;
            boss.UpdatePosition();
            Assert.IsTrue(boss.currentState == MState.Mid);
        }

        [TestMethod]
        public void Boss_UpdatePosition_Easy_MidState_Success()
        {
            Boss_Easy boss = new Boss_Easy(100, 80, 12, 2000, 2000);
            boss.isEntering = false;

            boss.currentState = MState.Mid;
            boss.UpdatePosition();

            Assert.IsTrue(boss.currentState == MState.Mid);
            Assert.IsTrue(boss.hitbox.X == Convert.ToInt32(boss.X));
            Assert.IsTrue(boss.hitbox.Y == Convert.ToInt32(boss.Y));

        }

        [TestMethod]
        public void Boss_UpdatePosition_Easy_AttackState_Success()
        {
            Boss_Easy boss = new Boss_Easy(100, 80, 2, 2000, 2000);
            boss.currentState = MState.Attack;

            Assert.IsTrue(boss.currentState == MState.Attack);
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
        public void Boss_UpdatePosition_Medium_StartState_Success()
        {
            Boss_Medium boss = new Boss_Medium(100, 80, 12, 2000, 2000);
            
            boss.UpdatePosition();

            Assert.IsTrue(boss.state == State.Start);
            Assert.IsTrue(boss.hitbox.X == boss.X);
            Assert.IsTrue(boss.hitbox.Y == boss.Y);


            boss.health = 5;
            boss.UpdatePosition();
            Assert.IsTrue(boss.state == State.Mid);

        }

        [TestMethod]
        public void Boss_UpdatePosition_Medium_MidState_Success()
        {
            Boss_Medium boss = new Boss_Medium(100, 80, 12, 2000, 2000);
            boss.state = State.Mid;
            boss.health = 15;
            boss.UpdatePosition();

            Assert.IsTrue(boss.state == State.End);
            Assert.IsTrue(boss.hitbox.X == boss.X);
            Assert.IsTrue(boss.hitbox.Y == boss.Y);

        }

        [TestMethod]
        public void Boss_UpdatePosition_Medium_EndState_Success()
        {
            Boss_Medium boss = new Boss_Medium(100, 80, 2, 2000, 2000);
            boss.state = State.End;
            boss.UpdatePosition();

            Assert.IsTrue(boss.state == State.End);
            Assert.IsTrue(boss.hitbox.X == boss.X);
            Assert.IsTrue(boss.hitbox.Y == boss.Y);

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

            boss.x_axis = -1000;
            boss.UpdatePosition();

            Assert.IsTrue(boss.dir == Direction.Right);

            boss.x_axis = 0;
            boss.UpdatePosition();

            Assert.IsTrue(boss.dir == Direction.Left);

            boss.cooldown = 1;
            boss.UpdatePosition();

            Assert.IsTrue(boss.action);
            Assert.IsTrue(boss.fired_slanted_targeted_shot);
            Assert.IsTrue(boss.cooldown == 50);


        }

        [TestMethod]
        public void Boss_Easy_Start_Success()
        {
            Boss_Easy boss = new Boss_Easy(100, 100, 20, 2000, 2000);

            boss.UpdatePosition();

            Assert.IsTrue(boss.Y > 100);

            boss.Y = -1;
            boss.UpdatePosition();

            Assert.IsTrue(boss.Y < -1);
            Assert.IsTrue(boss.dir == -1);

            boss.Y = 2001;
            boss.UpdatePosition();

            Assert.IsTrue(boss.Y < 2001);
            Assert.IsTrue(boss.dir == -1);

        }

        [TestMethod]
        public void Boss_Easy_Mid_Success()
        {
            Boss_Easy boss = new Boss_Easy(100, 100, 20, 2000, 2000);

            boss.currentState = MState.Mid;

            boss.RecieveTrackerData(200, 200);
            boss.Y = 10;

            boss.UpdatePosition();

            Assert.IsTrue(boss.Y > 10);

            boss.Y = 300;

            boss.UpdatePosition();

            Assert.IsTrue(boss.Y < 300);

        }

        [TestMethod]
        public void Boss_Easy_Attack_Success()
        {
            Boss_Easy boss = new Boss_Easy(100, 100, 20, 2000, 2000);
            boss.currentState = MState.Attack;

            boss.UpdatePosition();

            Assert.IsTrue(boss.X < 100);
            Assert.IsFalse(boss.goingBackwards);

            boss.currentState = MState.Attack;

            boss.X = 0;
            boss.UpdatePosition();

            Assert.IsTrue(boss.goingBackwards);

            boss.currentState = MState.Attack;

            boss.X = 100;
            boss.UpdatePosition();

            Assert.IsTrue(boss.X > 100);

            boss.currentState = MState.Attack;

            boss.X = 1500;
            boss.UpdatePosition();

            Assert.IsFalse(boss.goingBackwards);
            Assert.IsTrue(boss.currentState == MState.Start);

            boss.currentState = MState.Attack;

            boss.health = 5;
            boss.X = 1500;
            boss.goingBackwards = true;
            boss.UpdatePosition();

            Assert.IsTrue(boss.currentState == MState.Mid);
        }

        [TestMethod]
        public void Boss_Medium_Start_Success()
        {
            Boss_Medium boss = new Boss_Medium(100, 100, 20, 2000, 2000);
            boss.state = State.Start;

            boss.RecieveTrackerData(200, 200);
            boss.Y = 10;

            boss.UpdatePosition();

            Assert.IsTrue(boss.Y > 10);

            boss.Y = 300;

            boss.UpdatePosition();

            Assert.IsTrue(boss.Y < 300);
        }

        [TestMethod]
        public void Boss_Medium_Mid_Success()
        {
            Boss_Medium boss = new Boss_Medium(100, 100, 20, 2000, 2000);

            boss.actionTimer = 0.76;
            boss.state = State.Mid;
            boss.UpdatePosition();

            Assert.IsTrue(boss.action);
            Assert.IsTrue(boss.wall);
            Assert.IsTrue(boss.actionTimer == 0);
        }

        [TestMethod]
        public void Boss_Medium_End_Success()
        {
            Boss_Medium boss = new Boss_Medium(100, 100, 20, 2000, 2000);

            boss.actionTimer = 0.76;
            boss.state = State.End;
            boss.UpdatePosition();

            Assert.IsTrue(boss.action);
            Assert.IsTrue(boss.wall);
            Assert.IsTrue(boss.actionTimer == 0);

        }
    }
}
