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
    public class EntityTests
    {
        [TestMethod]
        public void Hit_AI_Success()
        {
            AI test = new AI(10, 10, pattern.Straight);

            Assert.IsTrue(test.Hit());
            Assert.IsTrue(test.alive == false);
        }

        [TestMethod]
        public void Hit_Bullet_Success()
        {
            Bullet test = new Bullet(10, 10);

            Assert.IsTrue(test.Hit());
            Assert.IsTrue(test.alive == false);
        }

        [TestMethod]
        public void Hit_Asteroid_Success()
        {
            Asteroid test = new Asteroid(10, 10);

            Assert.IsFalse(test.Hit());
        }

        [TestMethod]
        public void Hit_Formation_Success()
        {
            Formation test = new Formation(10, 10, pattern.Straight);

            Assert.IsTrue(test.Hit());
            Assert.IsTrue(test.alive == false);
        }

        [TestMethod]
        public void Hit_Mine_Success()
        {
            Mine test = new Mine(10, 10, pattern.Straight);

            Assert.IsTrue(test.Hit());
            Assert.IsTrue(test.alive == false);
        }

        [TestMethod]
        public void Tracker_Hit_Success()
        {
            Tracker test = new Tracker(10, 10, pattern.Straight);

            Assert.IsTrue(test.Hit());
            Assert.IsTrue(test.alive == false);
        }

        [TestMethod]
        public void Hit_Player_Success()
        {
            GameController g = new GameController();
            Player test = new Player(10, 10, 3, 5, g);

            test.cheating = true;
            Assert.IsFalse(test.Hit());

            test.cheating = false;
            test.isInvinsible = true;
            Assert.IsFalse(test.Hit());

            test.isInvinsible = false;
            Assert.IsTrue(test.Hit());
            Assert.IsTrue(test.lives == 2);
            Assert.IsTrue(test.HitCoolDown == 300);

            test.lives = 0;

            Assert.IsTrue(test.Hit());
            Assert.IsTrue(g.gameResult == GameResult.Lost);
        }

        //*********************************************************************update position
        [TestMethod]
        public void UpdatePosition_Player_Success()
        {
            GameController game = new GameController();
            game.winHeight = 200;
            game.winWidth = 200;
            Player test = new Player(20, 20, 3, 5, game);

            test.cooldown = 20;
            test.bombCooldown = 20;
            test.HitCoolDown = 20;

            test.isPoweredUp = true;
            test.powerUpCounter = 0;

            game.up = true;
            game.down = true;
            game.left = true;
            game.right = true;

            game.fired = true;
            game.bomb = true;

            test.UpdatePosition();

            Assert.IsTrue(test.cooldown == 19);
            Assert.IsTrue(test.HitCoolDown == 19);
            Assert.IsTrue(test.bombCooldown == 19);
            Assert.IsTrue(test.powerUpCounter == 1);

            Assert.IsTrue(test.X == 20);
            Assert.IsTrue(test.Y == 20);

        }

        [TestMethod]
        public void UpdatePosition_AI_Success()
        {
            AI test = new AI(0, 0, pattern.Straight);
            test.UpdatePosition();

            Assert.IsTrue(test.X != 0);
            Assert.IsTrue(test.Y == 0);
        }

        [TestMethod]
        public void UpdatePosition_Formation_Success()
        {
            Formation test = new Formation(0, 0, pattern.Sin);
            test.UpdatePosition();
            test.UpdatePosition();
            test.UpdatePosition();

            Assert.IsTrue(test.X != 0);
            Assert.IsTrue(test.Y != 0);
        }

        [TestMethod]
        public void UpdatePosition_Mine_Success()
        {
            Mine test = new Mine(0, 0, pattern.Straight);
            test.RecieveTrackerData(40, 40);

            test.UpdatePosition();

            Assert.IsTrue(test.X != 0);
            Assert.IsTrue(test.Y != 0);
        }

        [TestMethod]
        public void UpdatePosition_Tracker_Success()
        {
            Tracker test = new Tracker(0, 0, pattern.Straight);
            test.RecieveTrackerData(60, 60, 300);

            test.UpdatePosition();


            Assert.IsTrue(test.X != 0);
            Assert.IsTrue(test.Y != 0);
        }

        [TestMethod]
        public void UpdatePosition_Bullet_Success()
        {
            Bullet test = new Bullet(0, 0);
            test.UpdatePosition();

            Assert.IsTrue(test.X != 0);
            Assert.IsTrue(test.Y == 0);
        }

        [TestMethod]
        public void UpdatePosition_Asteroid_Success()
        {
            Asteroid test = new Asteroid(0, 0);
            test.UpdatePosition();

            Assert.IsTrue(test.X != 0);
            Assert.IsTrue(test.Y == 0);
        }

        [TestMethod]
        public void UpdatePosition_PowerUp_Success()
        {
            Powerup test = new Powerup(0, 0, PowerUp.TripleShot);
            test.UpdatePosition();

            Assert.IsTrue(test.X != 0);
            Assert.IsTrue(test.Y == 0);
        }
    }
}
