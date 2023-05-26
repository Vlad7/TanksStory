using System;
using TanksGameEngine.GameEngine;

namespace TanksGameEngine.TanksGame.Objects.Capsules
{
    using System.Collections.Generic; 
    using System.Windows;

    /// <summary>
    /// Capsule class
    /// </summary>
    public class Capsule : PrimitiveObject
    {
        /// <summary>
        /// Capsule constructor
        /// </summary>
        public Capsule() : base(String.Empty)
        {
             
        }

        public Capsule(String name) : base(name)
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