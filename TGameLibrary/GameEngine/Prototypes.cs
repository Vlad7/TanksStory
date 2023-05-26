using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGameLibrary.Objects.Characters.CharacterParts.Bullets;

namespace TGameLibrary.GameEngine
{
    //public class Prototypes
    //{
    //    public static Bullets Bullets { get; set; }
    //    public static Bullets Bullets { get; set; }
    //    public static Bullets Bullets { get; set; }
    //    public static Bullets Bullets { get; set; }

    //    public PrototypeObjects()
    //    {
                
    //    }
        
        
    //}

    //public class Bullets
    //{
    //    public Bullet SimpleBullet  { get; private set; }
    //    public Bullet ShockBullet   { get; private set; }
    //    public Bullet RocketBullet  { get; private set; }
    //    public Bullet LaserBullet   { get; private set; }
    //    public Bullet CannonBullet  { get; private set; }
    //    public Bullet FireBullet    { get; private set; }
    //    public Bullet JumpingBullet { get; private set; }
    //    public Bullet Mine          { get; private set; }

     
    //}

    //public class Guns
    //{
    //    public Bullet SimpleBullet  { get; private set; }
    //    public Bullet ShockBullet   { get; private set; }
    //    public Bullet RocketBullet  { get; private set; }
    //    public Bullet LaserBullet   { get; private set; }
    //    public Bullet CannonBullet  { get; private set; }
    //    public Bullet FireBullet    { get; private set; }
    //    public Bullet JumpingBullet { get; private set; }
    //    public Bullet Mine          { get; private set; }

     
    //}

    //public class MapElements
    //{
    //    public Bullet SimpleBullet  { get; private set; }
    //    public Bullet ShockBullet   { get; private set; }
    //    public Bullet RocketBullet  { get; private set; }
    //    public Bullet LaserBullet   { get; private set; }
    //    public Bullet CannonBullet  { get; private set; }
    //    public Bullet FireBullet    { get; private set; }
    //    public Bullet JumpingBullet { get; private set; }
    //    public Bullet Mine          { get; private set; }

     
    //}


    // public class Explosions
    //{
    //    public Bullet SimpleBullet  { get; private set; }
    //    public Bullet ShockBullet   { get; private set; }
    //    public Bullet RocketBullet  { get; private set; }
    //    public Bullet LaserBullet   { get; private set; }
    //    public Bullet CannonBullet  { get; private set; }
    //    public Bullet FireBullet    { get; private set; }
    //    public Bullet JumpingBullet { get; private set; }
    //    public Bullet Mine          { get; private set; }

    //    public Bullets()
    //    {

    //    }
    //}
}


 public enum Guns
    {
        ShotGun, ShockGun, RocketGun, BombGun, CannonGun, EMPGun, ImpulseGun,
        RicochetGun, MiniGun, LaserGun, FightGun
    }

    public enum Explosions
    {
        WoodBlockExplosion, BrickBlockExplosion, BarrelExplosion, TankExplosion, 
        EmptyExplosion, BulletExplosion, Smoke, Crater
    }
