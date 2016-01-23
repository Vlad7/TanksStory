using System;
using TanksGameEngine.GameEngine;

namespace TanksGameEngine.TanksGame.Objects.Bullets
{
    using System.Collections.Generic; 
    using System.Windows;

    /// <summary>
    /// Bullet class
    /// </summary>
    public class Bullet : PrimitiveObject
    {
        /// <summary>
        /// Bullet constructor
        /// </summary>
        public Bullet() : base(String.Empty)
        {
             
        }

        public Bullet(String name) : base(name)
        {

        }

        public override void Collide(GameObject gameObject, Vector direction, double overlap)
        {
            Crash();
            //base.Collide(gameObject, direction, overlap);
        }

   
        
        
        











        //public override void Collide()
        //{
        //    Destroy();
        //}

        public void Detonate()
        {
            //GameProcess.Current_Game.gameMap.RemoveObject();

        }

        
    }
}