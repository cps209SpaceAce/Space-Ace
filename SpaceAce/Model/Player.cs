using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum powerup {Power}
    
    public class Player: Entity

    {
        public int cooldown = 0;
        public int bombCooldown = 0;
        public int lives { get; set; }
        public int bombs { get; set; }
        public powerup powerup { get; set; }
        public int HitCoolDown;
        public string image = "spaceship-hi.png";
        

        private GameController game;

        public Player(double X, double Y, int lives, int bombs, GameController flags):base(X,Y)
        {
            game = flags;
            this.lives = lives;
            this.bombs = bombs;
            speed = 5;
            this.hitbox = new Rectangle(Convert.ToInt32(X), Convert.ToInt32(Y), 45, 20); //custom hit box for the player
        }

        public void Fire()
        {
            if (cooldown == 0)
            {
                FiredABullet = true;
                cooldown = 50;      // Rate of fire: bigger = slower.
            }            
        }

        public void Activate_powerup()
        {
            //TODO: activate powerup
        }
        public void DropBomb()
        {
            if(bombCooldown == 0)
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
            //TODO: remove one life(ship destroyed)

            //TODO: return true(ship destroyed)
            if (HitCoolDown == 0)
            {
                lives--;
                HitCoolDown = 300;
            }

            if(lives == 0)
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

            if(cooldown > 0)
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

            hitbox.X = Convert.ToInt32(X);
            hitbox.Y = Convert.ToInt32(Y);

        }
        public override string Serialize()
        {
            return X + "," + Y + "," + powerup + "," + lives + "," + bombs;
        }
    }
}
