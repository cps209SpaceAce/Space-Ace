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
            test.health = 2;
            Assert.IsFalse(test.Hit());
            Assert.IsTrue(test.Hit());
            Assert.IsTrue(test.alive == false);

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
        public void Hit_Tracker_Success()
        {
            Tracker test = new Tracker(10, 10, pattern.Straight);

            Assert.IsTrue(test.Hit());
            Assert.IsTrue(test.alive == false);
        }

        [TestMethod]
        public void Hit_Powerup_Success()
        {
            Powerup test = new Powerup(10, 10, PowerUp.ExtraBomb);

            Assert.IsTrue(test.Hit());
            Assert.IsTrue(test.alive == false);
        }

        [TestMethod]
        public void Hit_Player_Success()
        {
            GameController g = new GameController();
            Player test = new Player(10, 10, 3, 5, g, "ship_image");

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
            Player test = new Player(20, 20, 3, 5, game, "player1.png");

            test.cooldown = 20;
            test.bombCooldown = 20;
            test.HitCoolDown = 20;

            test.UpdatePosition();
            Assert.IsTrue(test.cooldown == 19);
            Assert.IsTrue(test.HitCoolDown == 19);
            Assert.IsTrue(test.bombCooldown == 19);
            Assert.IsTrue(test.powerUpCounter == 0);

            game.up = true;
            test.UpdatePosition();
            Assert.IsTrue(test.X == 20);
            Assert.IsTrue(test.Y < 20);
            game.up = false;

            game.down = true;
            test.UpdatePosition();
            Assert.IsTrue(test.X == 20);
            Assert.IsTrue(test.Y == 20);
            game.down = false;

            game.left = true;
            test.UpdatePosition();
            Assert.IsTrue(test.X < 20);
            Assert.IsTrue(test.Y == 20);
            game.left = false;

            game.right = true;
            test.UpdatePosition();
            Assert.IsTrue(test.X == 20);
            Assert.IsTrue(test.Y == 20);
            game.right = false;

            game.fired = true;
            game.bomb = true;
            test.isPoweredUp = true;
            test.powerUpCounter = 0;

            test.UpdatePosition();
        }

        [TestMethod]
        public void Activate_PowerUp_RapidFire_Success()
        {
            Player test = new Player(10, 10, 3, 5, new GameController(), "player1.png");
            test.powerup = PowerUp.RapidFire;

            test.Activate_powerup();

            Assert.IsTrue(test.powerup == PowerUp.Empty);
            Assert.IsTrue(test.isPoweredUp == true);
            Assert.IsTrue(test.powerUpCounter == 0);
            Assert.IsTrue(test.rapid_fire == true);

        }

        [TestMethod]
        public void Activate_PowerUp_ExtraBomb_Success()
        {
            Player test = new Player(10, 10, 3, 5, new GameController(), "player1.png");
            test.powerup = PowerUp.ExtraBomb;

            test.Activate_powerup();

            Assert.IsTrue(test.powerup == PowerUp.Empty);
            Assert.IsTrue(test.isPoweredUp == false);
            Assert.IsTrue(test.powerUpCounter == 0);
            Assert.IsTrue(test.bombs == 6);

        }

        [TestMethod]
        public void Activate_PowerUp_ExtraLife_Success()
        {
            Player test = new Player(10, 10, 3, 5, new GameController(), "player1.png");
            test.powerup = PowerUp.ExtraLife;

            test.Activate_powerup();

            Assert.IsTrue(test.powerup == PowerUp.Empty);
            Assert.IsTrue(test.isPoweredUp == false);
            Assert.IsTrue(test.powerUpCounter == 0);
            Assert.IsTrue(test.lives == 4);
        }

        [TestMethod]
        public void Activate_PowerUp_Triple_Success()
        {
            Player test = new Player(10, 10, 3, 5, new GameController(), "player1.png");
            test.powerup = PowerUp.TripleShot;

            test.Activate_powerup();

            Assert.IsTrue(test.powerup == PowerUp.Empty);
            Assert.IsTrue(test.isPoweredUp == true);
            Assert.IsTrue(test.powerUpCounter == 0);
            Assert.IsTrue(test.triple == true);
        }

        [TestMethod]
        public void Activate_PowerUp_Empty_Success()
        {
            Player test = new Player(10, 10, 3, 5, new GameController(), "player1.png");
            test.powerup = PowerUp.Empty;

            test.Activate_powerup();

            Assert.IsTrue(test.powerup == PowerUp.Empty);
            Assert.IsTrue(test.isPoweredUp == false);
            Assert.IsTrue(test.powerUpCounter == 0);
        }

        [TestMethod]
        public void Activate_PowerUp_ExtraSpeed_Success()
        {
            Player test = new Player(10, 10, 3, 5, new GameController(), "player1.png");
            test.powerup = PowerUp.ExtraSpeed;

            test.Activate_powerup();

            Assert.IsTrue(test.powerup == PowerUp.Empty);
            Assert.IsTrue(test.isPoweredUp == true);
            Assert.IsTrue(test.powerUpCounter == 0);
            Assert.IsTrue(test.speed == 10);
        }

        [TestMethod]
        public void Activate_PowerUp_Invincible_Success()
        {
            Player test = new Player(10, 10, 3, 5, new GameController(), "player1.png");
            test.powerup = PowerUp.Invincible;

            test.Activate_powerup();

            Assert.IsTrue(test.powerup == PowerUp.Empty);
            Assert.IsTrue(test.isPoweredUp == true);
            Assert.IsTrue(test.powerUpCounter == 0);
            Assert.IsTrue(test.isInvinsible == true);
        }

        [TestMethod]
        public void Deactivate_Powerup_Success()
        {
            Player test = new Player(10, 10, 3, 5, new GameController(), "player1.png");
            test.powerUpCounter = 400;
            test.rapid_fire = true;
            test.isInvinsible = true;
            test.isPoweredUp = true;
            test.triple = true;
            test.speed = 90;

            test.powerUpCounter = 5000;
            test.UpdatePosition();

            Assert.IsTrue(test.powerUpCounter == 0);
            Assert.IsTrue(test.isInvinsible == false);
            Assert.IsTrue(test.isPoweredUp == false);
            Assert.IsTrue(test.rapid_fire == false);
            Assert.IsTrue(test.triple == false);
            Assert.IsTrue(test.speed == 5);
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
        public void UpdatePosition_Formation_Sin_Success()
        {
            Formation test = new Formation(0, 0, pattern.Sin);
            test.UpdatePosition();
            test.UpdatePosition();
            test.UpdatePosition();

            Assert.IsTrue(test.X != 0);
            Assert.IsTrue(test.Y != 0);
        }

        [TestMethod]
        public void UpdatePosition_Formation_Cos_Success()
        {
            Formation test = new Formation(0, 0, pattern.Cos);
            test.UpdatePosition();
            test.UpdatePosition();
            test.UpdatePosition();

            Assert.IsTrue(test.X != 0);
            Assert.IsTrue(test.Y != 0);
        }

        [TestMethod]
        public void UpdatePosition_Formation_Tan_Success()
        {
            Formation test = new Formation(0, 0, pattern.Tan);
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
        public void Tracker_BeforeStop_Success()
        {
            Tracker test = new Tracker(9000, 90, pattern.Straight);
            test.RecieveTrackerData(60, 60, 300);

            test.UpdatePosition();

            Assert.IsTrue(test.X < 9000);
            Assert.IsTrue(test.Y < 90);
        }

        [TestMethod]
        public void UpdatePosition_Bullet_Friendly_Success()
        {
            Bullet test = new Bullet(20, 20);
            test.UpdatePosition();

            Assert.IsTrue(test.X > 20);
            Assert.IsTrue(test.Y == 20);
        }

        [TestMethod]
        public void UpdatePosition_Bullet_Hostile_Success()
        {
            Bullet test = new Bullet(20, 20);
            test.id = ID.Hostile;
            test.UpdatePosition();

            Assert.IsTrue(test.X > 20);
            Assert.IsTrue(test.Y == 20);
        }

        [TestMethod]
        public void UpdatePosition_Bullet_OutofBounds_Despawn()
        {
            Bullet test = new Bullet(20, 20);
            test.id = ID.Hostile;

            test.X = 1201;
            test.UpdatePosition();

            Assert.IsTrue(test.alive == false);

            test.alive = true;

            test.X = -2000;
            test.UpdatePosition();

            Assert.IsTrue(test.alive == false);

        }

        [TestMethod]
        public void UpdatePosition_SlantedBullet_Friendly_Success()
        {
            Slanted_Bullet test = new Slanted_Bullet(20, 20, 50);
            test.UpdatePosition();

            Assert.IsTrue(test.X > 20);
            Assert.IsTrue(test.Y > 20);
        }

        [TestMethod]
        public void UpdatePosition_SlantedBullet_Hostile_Success()
        {
            Slanted_Bullet test = new Slanted_Bullet(20, 20, 50);
            test.id = ID.Hostile;
            test.UpdatePosition();

            Assert.IsTrue(test.X > 20);
            Assert.IsTrue(test.Y > 20);
        }

        [TestMethod]
        public void UpdatePosition_SlantedBullet_OutofBounds_Despawn()
        {
            Slanted_Bullet test = new Slanted_Bullet(20, 20, 50);
            test.id = ID.Hostile;

            test.X = 1201;
            test.UpdatePosition();

            Assert.IsTrue(test.alive == false);

            test.alive = true;

            test.X = -2000;
            test.UpdatePosition();

            Assert.IsTrue(test.alive == false);

        }

        [TestMethod]
        public void UpdatePosition_WanderingBullet_Sin_Success()
        {
            Wandering_Bullet test = new Wandering_Bullet(20, 20, pattern.Sin);
            test.UpdatePosition();

            Assert.IsTrue(test.X > 20);
            Assert.IsTrue(test.Y > 20);
        }

        [TestMethod]
        public void UpdatePosition_WanderingBullet_SinDown_Success()
        {
            Wandering_Bullet test = new Wandering_Bullet(20, 20, pattern.Sindown);
            test.UpdatePosition();

            Assert.IsTrue(test.X > 20);
            Assert.IsTrue(test.Y < 20);
        }

        [TestMethod]
        public void UpdatePosition_WanderingBullet_Tan_Success()
        {
            Wandering_Bullet test = new Wandering_Bullet(20, 20, pattern.Tan);
            test.UpdatePosition();

            Assert.IsTrue(test.X > 20);
            Assert.IsTrue(test.Y > 20);
        }

        [TestMethod]
        public void UpdatePosition_WanderingBullet_OutofBounds_Despawn()
        {
            Wandering_Bullet test = new Wandering_Bullet(9000, 20, pattern.Sin);
            test.id = ID.Hostile;
            test.UpdatePosition();

            Assert.IsTrue(test.alive == false);

            test.alive = true;

            test = new Wandering_Bullet(-9000, 20, pattern.Sin);
            test.UpdatePosition();

            Assert.IsTrue(test.alive == false);

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


        [TestMethod]
        public void Player_DropBomb_Success()
        {
            List<Entity> enemies = new List<Entity>();
            enemies.Add(new AI(20, 20, pattern.Cos));
            enemies.Add(new AI(20, 20, pattern.Cos));
            enemies.Add(new AI(20, 20, pattern.Cos));
            enemies.Add(new AI(20, 20, pattern.Cos));

            GameController game = new GameController();
            game.current_Enemies = enemies;

            Player test = new Player(10, 10, 4, 5, game, "player1.png");
            test.DropBomb();

            Assert.IsTrue(test.bombs == 4);
            Assert.IsTrue(test.bombCooldown == 50);

            Assert.IsTrue(game.current_Enemies.Count == 0);
            Assert.IsTrue(game.score == 50 * 4);
        }

        [TestMethod]
        public void Player_HitPlayer_Success()
        {
            GameController g = new GameController();
            Player p = new Player(10, 10, 1, 5, g, "player1.png");

            Assert.IsFalse(p.HitPlayer(new Powerup(10, 10, PowerUp.ExtraBomb)));

            p.isInvinsible = true;
            Assert.IsFalse(p.HitPlayer(new Bullet(10,10)));
            p.isInvinsible = false;

            p.cheating = true;
            Assert.IsFalse(p.HitPlayer(new Bullet(10, 10)));
            p.cheating = false;

            Assert.IsTrue(p.HitPlayer(new Bullet(10, 10)));
            Assert.IsTrue(p.HitCoolDown == 300);
            Assert.IsTrue(p.lives == 0);
            Assert.IsTrue(g.gameResult == GameResult.Lost);

        }

        [TestMethod]
        public void Player_Fire_Success()
        {
            Player p = new Player(10, 10, 3, 4, new GameController(), "player1.png");
            p.Fire();

            Assert.IsTrue(p.FiredABullet == true);
            Assert.IsTrue(p.cooldown == 50);

            p.rapid_fire = true;
            p.cooldown = 2;
            p.Fire();

            Assert.IsTrue(p.FiredABullet == true);
            Assert.IsTrue(p.cooldown == 50);
        }

        [TestMethod]
        public void UpdateWorld_GC_Success()
        {
            GameController ctrl = new GameController();
            ctrl.player.X = 100;
            ctrl.player.Y = 100;
            ctrl.player.lives = 5;
            ctrl.player.bombs = 5;

            List<Entity> en = new List<Entity>();
            en.Add(new AI(10, 10, pattern.Straight));
            en.Add(new Mine(50, 10, pattern.Straight));
            en.Add(new Tracker(70, 10, pattern.Straight));
            en.Add(new Powerup(100, 100, PowerUp.RapidFire));
            en.Add(new Powerup(100, 100, PowerUp.ExtraLife));
            en.Add(new Powerup(100, 100, PowerUp.ExtraBomb));

            en.Add(new Powerup(200, 200, PowerUp.RapidFire));
            en.Add(new Powerup(200, 200, PowerUp.ExtraLife));
            en.Add(new Powerup(200, 200, PowerUp.ExtraBomb));

            AI shot = new AI(60, 60, pattern.Straight);
            shot.FiredABullet = true;
            en.Add(shot);

            Formation dead = new Formation(20, 20, pattern.Sin);
            dead.alive = false;
            en.Add(dead);

            en.Add(new AI(100, 100, pattern.Straight));//intersects with player

            ctrl.current_Enemies = en;

            List<Bullet> b = new List<Bullet>();
            b.Add(new Bullet(2, 9));
            b.Add(new Bullet(5, 9));
            b.Add(new Bullet(7, 9));
            b.Add(new Bullet(60, 60)); //shot enemy
            b.Add(new Bullet(200, 200));//shot powerup
            Bullet deadbullet = new Bullet(20, 20);
            deadbullet.alive = false;
            b.Add(deadbullet);


            ctrl.player_fire = b;

            ctrl.UpdateWorld();

            Assert.IsTrue(ctrl.current_Enemies.Count == 2);
            Assert.IsTrue(ctrl.player_fire.Count == 0);
            Assert.IsTrue(ctrl.player.lives == 6);
            Assert.IsTrue(ctrl.player.bombs == 7);

        }

        //insanity ensues past this point as i try to test RNGs, 
        //feel free to delete them if they're too wrong
        //just write a comment saying: Jo.. just no.
        [TestMethod]
        public void FireBullet_AI_Success()
        {
            AI test = new AI(1000, 1000, pattern.Straight);
            while (!test.FiredABullet)
                test.UpdatePosition();

            Assert.IsTrue(test.FiredABullet);
            Assert.IsTrue(test.fireCoolDown == 50);
        }
        [TestMethod]
        public void FiredBullet_Formation_Success()
        {
            Formation test = new Formation(1000, 1000, pattern.Straight);
            while (!test.FiredABullet)
                test.UpdatePosition();

            Assert.IsTrue(test.FiredABullet);
            Assert.IsTrue(test.fireCoolDown == 50);
        }
        [TestMethod]
        public void FiredBullet_Tracker_Success()
        {
            Tracker test = new Tracker(1000, 1000, pattern.Straight);
            test.RecieveTrackerData(0, 0, 300);
            while (!test.FiredABullet)
                test.UpdatePosition();

            Assert.IsTrue(test.FiredABullet);
            Assert.IsTrue(test.fireCoolDown == 50);
        }


    }
}
