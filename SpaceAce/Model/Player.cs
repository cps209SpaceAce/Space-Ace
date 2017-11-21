using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum PowerUp { Empty, Invincible, ExtraSpeed, ExtraLife, ExtraBomb, TripleShot, RapidFire } // TODO: pick what powerups to put here
    
    public class Player : Entity

    {
        public bool wanderingbullets =false; // flag for sin and cos bullets. currently broken 
        public bool triple = false;//flag for triple shot
        public bool rapid_fire = false; //flag to engage rapid fire mode by reducing the cooldown
        public int cooldown = 0;
        public int bombCooldown = 0;
        public int lives { get; set; }
        public int bombs { get; set; }
        public PowerUp powerup { get; set; }
        public int HitCoolDown;
        public string image = "spaceship-hi.png";
        double baseSpeed;

        public double powerUpCounter = 0;
        public bool isPoweredUp = false;
        bool isInvinsible = false;
        public bool cheating = false;


        private GameController game;

        public Player(double X, double Y, int lives, int bombs, GameController flags) : base(X, Y)
        {
            game = flags;
            this.lives = lives;
            this.bombs = bombs;
            powerup = PowerUp.Empty;
            speed = 5;
            baseSpeed = speed;
            this.hitbox = new Rectangle(Convert.ToInt32(X), Convert.ToInt32(Y), 45, 20); //custom hit box for the player
        }

        public void Fire()
        {
            if (cooldown == 0 || (rapid_fire && cooldown < 25))
            {
                FiredABullet = true;
                cooldown = 50;      // Rate of fire: bigger = slower.
            }
        }

        public void Activate_powerup()
        {
            if (powerup == PowerUp.Empty)
                return;

            isPoweredUp = true;
            SoundManager playSound = new SoundManager();
            playSound.PlayNoise(SoundType.PowerUp);

            if (powerup == PowerUp.ExtraSpeed)
                speed = baseSpeed*2;
            else if (powerup == PowerUp.Invincible)
                isInvinsible = true;
            else if (powerup == PowerUp.ExtraLife) { 
                ++lives;
                isPoweredUp = false;
            }
            else if (powerup == PowerUp.ExtraBomb)
            {
                ++bombs;
                isPoweredUp = false;
            }
            else if (powerup == PowerUp.RapidFire)
                rapid_fire = true;
            else if (powerup == PowerUp.TripleShot)
                triple = true;

            powerup = PowerUp.Empty;
        }
        void Deactivate_Powerup()
        {
            powerUpCounter = 0;
            isInvinsible = false;
            speed = baseSpeed;
            isPoweredUp = false;
            triple = false;
            rapid_fire = false;
        }
        public void DropBomb()
        {
            if (bombCooldown == 0)
            {
                bombCooldown = 50;
                if (bombs != 0)
                {
                    game.Bomb();
                    bombs--;
                }

                // TODO: Add score for kills
            }
        }

        public override bool Hit()
        {
            if (isInvinsible || cheating)
            {
                return false; 
            }

            //TODO: remove one life(ship destroyed)

            //TODO: return true(ship destroyed)
            if (HitCoolDown == 0)
            {
                lives--;
                HitCoolDown = 300;
            }

            if (lives == 0)
                game.gameResult = GameResult.Lost;

            return true;
        }

        public bool HitPlayer(Entity ent)
        {

            if (ent is Powerup)
            {
                powerup = (ent as Powerup).type;
                return false;
            }

            if (isInvinsible || cheating)
            {
                return false; //invinsibility
            }


            //TODO: remove one life(ship destroyed)

            //TODO: return true(ship destroyed)
            if (HitCoolDown == 0)
            {
                lives--;
                HitCoolDown = 300;
            }

            if (lives == 0)
                game.gameResult = GameResult.Lost;

            return true;
        }

        public void Up()
        {
            Y = Convert.ToInt32(Y - (1 * speed));
        }

        public void Down()
        {
            Y = Convert.ToInt32(Y + (1 * speed));
        }

        public void Left()
        {
            X = Convert.ToInt32(X - (1 * speed));
        }

        public void Right()
        {
            X = Convert.ToInt32(X + (1 * speed));
        }

        public override void UpdatePosition()
        {
            if (HitCoolDown > 0)
                HitCoolDown--;

            if (cooldown > 0)
                cooldown--;

            if (bombCooldown > 0)
                bombCooldown--;

            if (game.up)
            {
                if (this.Y > 0)
                    Up();
            }
            if (game.down)
            {
                if (this.Y < game.winHeight - 30)
                    Down();
            }
            if (game.left)
            {
                if (this.X > 0)
                    Left();
            }
            if (game.right)
            {
                if (this.X < game.winWidth - 50)
                    Right();
            }
            if (game.fired)
            {
                Fire();
            }
            if (game.bomb)
            {
                DropBomb();
            }

            if (isPoweredUp) {
                powerUpCounter++;
                //Console.WriteLine("I AM POWERED UP");
                if (powerUpCounter >= 5000) 
                    Deactivate_Powerup();      
            }

            hitbox.X = Convert.ToInt32(X);
            hitbox.Y = Convert.ToInt32(Y);

        }
        public override string Serialize()
        {
            return X + "," + Y + "," + powerup + "," + lives + "," + bombs + "," + isPoweredUp + "," + powerUpCounter + "," + cheating;
        }
    }
}
