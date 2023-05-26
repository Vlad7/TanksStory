
using System;
using System.Windows;
using TanksGameEngine.GameEngine;
using TanksGameEngine.GameEngine.Components;
using TanksGameEngine.GameEngine.Controllers;



namespace TanksGameEngine.TanksGame.Objects.CharacterParts.Guns

{
    /// <summary>
    /// Gun class
    /// </summary>
    public class Gun : PrimitiveObject
    {
        /// <summary>
        /// Gun recharge time in milliseconds
        /// </summary>
        public Int32 RechargeTime { get; set; }

        /// <summary>
        /// Time from last fire
        /// </summary>
        public Int32 ChargedTime { get; set; }

        public Int32 MaxFireRange { get; set; }


        /// <summary>
        /// Gun constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="rechargeSpeed"></param>
        public Gun(String name) : base(name)
        {
            RechargeTime = ChargedTime = 0;
        }

        /// <summary>
        /// Gun constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="rechargeSpeed"></param>
        public Gun(String name, Int32 rechargeTime) : base(name)       
        {
            this.RechargeTime = ChargedTime = rechargeTime;

            MaxFireRange = 300;
        }

        public override void Update(Int32 dt)
        {
            base.Update(dt);

            if (ChargedTime < RechargeTime)
            {
                ChargedTime += dt;               
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Fire()
        {
            if (ChargedTime < RechargeTime) return;

            int CapsuleVelocity = 50;

            SoundManager.PlayBoomSound();

            double angle_rad = Math.PI * AbsoluteAngle / 180.0;
      
            Vector tower_end = new Vector(Math.Cos(angle_rad), Math.Sin(angle_rad)) * Size.X + AbsoluteCenter;
   
            Locator loc = new Locator(tower_end, AbsoluteAngle + 45, 5);

            Engine engine = new Engine(new Vector(CapsuleVelocity * Math.Cos(angle_rad), CapsuleVelocity * Math.Sin(angle_rad)), new Vector(0, 0), 0, 0);

            GameProcess.Current_Game.Builder.BuildCapsule("Capsule", loc, engine);

            ChargedTime = 0;                   
        }   
    }
}