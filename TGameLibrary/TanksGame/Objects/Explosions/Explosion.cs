using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TanksGameEngine.GameEngine;

namespace TanksGameEngine.TanksGame.Objects.Explosions
{
    public class Explosion : PrimitiveObject
    {
        
        public Explosion() : base(String.Empty)
        {
             
        }

         public Explosion(String name)
             : base(name)
        {

        }

         public override void Update(int dt)
         {
             base.Update(dt);
             Damage(8);
         }    
    }
}
