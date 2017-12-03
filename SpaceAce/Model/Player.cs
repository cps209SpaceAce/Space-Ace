using System;
using System.Drawing;
using System.Windows;
using System.Windows.Media;

namespace Model
{
    public enum PowerUp { Empty, Helix, Invincible, ExtraSpeed, ExtraLife, ExtraBomb, TripleShot, RapidFire } // TODO: pick what powerups to put here

    public class Player : Entity

    {
        public bool wanderingbullets = false;   // flag for sin and cos bullets. currently broken 
        public bool triple = false;             //flag for triple shot
        public bool rapid_fire = false;         //flag to engage rapid fire mode by reducing the cooldown
        public bool extraSpeed = false;
        public int cooldown = 0;
        public int bombCooldown = 0;
        public int Lives { get; set; }
        public int Bombs { get; set; }
        public PowerUp powerup { get; set; }
        public int HitCoolDown;
        public string image = "spaceship-hi.png";
        double baseSpeed;

        public double powerUpCounter = 0;
        public bool isPoweredUp = false;
        public bool isInvincible = false;
        public bool cheating = false;


        private GameController game;

        public Player(double X, double Y, int lives, int bombs, GameController flags, string shipIMG) : base(X, Y)
        {
            image = shipIMG;
            game = flags;
            this.Lives = lives;
            this.Bombs = bombs;
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


            var sound = new MediaPlayer();
            sound.Open(new Uri(System.Environment.CurrentDirectory.Substring(0, System.Environment.CurrentDirectory.Length - 9) + "Resources\\PowerUp.wav", UriKind.Absolute));
            Application.Current.Dispatcher.BeginInvoke(new Action(() => sound.Play()));


            if (powerup == PowerUp.ExtraSpeed)
            {
                speed = baseSpeed * 2;
                extraSpeed = true;
            }
            else if (powerup == PowerUp.Invincible)
            {
                isInvincible = true;
                powerUpCounter = 0;
            }
            else if (powerup == PowerUp.ExtraLife)
            {
                ++Lives;
                isPoweredUp = false;
            }
            else if (powerup == PowerUp.ExtraBomb)
            {
                ++Bombs;
                isPoweredUp = false;
            }
            else if (powerup == PowerUp.RapidFire)
            {
                rapid_fire = true;
                wanderingbullets = false;
                triple = false;
            }
            else if (powerup == PowerUp.TripleShot)
            {
                triple = true;
                wanderingbullets = false;
                rapid_fire = false;
            }
            else if (powerup == PowerUp.Helix)
            {
                wanderingbullets = true;
                rapid_fire = false;
                triple = false;
            }

            powerup = PowerUp.Empty;
        }
        void Deactivate_Powerup()
        {
            powerUpCounter = 0;
            speed = baseSpeed;
            isPoweredUp = false;
            triple = false;
            rapid_fire = false;
            wanderingbullets = false;
            extraSpeed = false;
        }
        public void DropBomb()
        {
            if (bombCooldown == 0)
            {
                bombCooldown = 50;
                if (Bombs != 0)
                {
                    game.Bomb();
                    Bombs--;
                }
            }
        }

        public override bool Hit()
        {
            if (isInvincible || cheating)
            {
                return false;
            }

            if (HitCoolDown == 0)
            {
                Lives--;
                HitCoolDown = 300;
            }

            if (Lives == 0)
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

            if (isInvincible || cheating)
            {
                return false;
            }

            if (HitCoolDown == 0)
            {
                Lives--;
                HitCoolDown = 300;
            }

            if (Lives == 0)
                game.gameResult = GameResult.Lost;

            Deactivate_Powerup();
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
                if (Y > 0)
                    Up();
            }
            if (game.down)
            {
                if (Y < game.winHeight - 30)
                    Down();
            }
            if (game.left)
            {
                if (X > 0)
                    Left();
            }
            if (game.right)
            {
                if (X < game.winWidth - 50)
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

            if (isPoweredUp)
            {
                powerUpCounter++;
                if (powerUpCounter >= 800)
                {
                    isInvincible = false;
                    powerUpCounter = 0;
                }
            }

            hitbox.X = Convert.ToInt32(X);
            hitbox.Y = Convert.ToInt32(Y);
        }

        //Method for loading currently active powerups
        public void LoadPowerups()
        {
            if (extraSpeed)
            {
                speed = baseSpeed * 2;
                extraSpeed = true;
            }
            if (isInvincible)
            {
                isInvincible = true;
                powerUpCounter = 0;
            }
            if (rapid_fire)
            {
                rapid_fire = true;
                wanderingbullets = false;
                triple = false;
            }
            else if (triple)
            {
                triple = true;
                wanderingbullets = false;
                rapid_fire = false;
            }
            else if (wanderingbullets)
            {
                wanderingbullets = true;
                rapid_fire = false;
                triple = false;
            }
        }

        //Serialization method that converts all necessary values into a string
        public override string Serialize()
        {
            return X + "," + Y + "," + powerup + "," + Lives + "," + Bombs + "," + isPoweredUp + "," + powerUpCounter + "," + cheating + "," + image + "," + triple + "," + wanderingbullets + "," + extraSpeed + "," + rapid_fire + "," + isInvincible;
        }
    }
}
