using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    public enum SoundType
    {
        Shoot1, PowerUp, Shoot2, Bomb, HurtPlayer, HurtEnemy,
        Main, Game, Win, Lose, Boss
    }

    public class SoundManager
    {
        public SoundManager()
        {
            
        }

        public void PlayNoise(SoundType type)
        {
            SoundPlayer noisePlayer = null;
            switch (type)
            {
                case SoundType.Shoot1:
                    noisePlayer = new SoundPlayer(SpaceAce.Properties.Resources.shoot1);
                    break;
                case SoundType.Shoot2:
                    noisePlayer = new SoundPlayer(SpaceAce.Properties.Resources.shoot2);
                    break;
                case SoundType.PowerUp:
                    noisePlayer = new SoundPlayer(SpaceAce.Properties.Resources.powerup);
                    break;
                case SoundType.Bomb:
                    noisePlayer = new SoundPlayer(SpaceAce.Properties.Resources.bomb);
                    break;
                case SoundType.HurtPlayer:
                    noisePlayer = new SoundPlayer(SpaceAce.Properties.Resources.hurtplayer);
                    break;
                case SoundType.HurtEnemy:
                    noisePlayer = new SoundPlayer(SpaceAce.Properties.Resources.damage);
                    break;
                    
            }
            noisePlayer.Stream.Position = 0;
            noisePlayer.Play();
        }
        



    }
}
