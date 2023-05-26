using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TanksGameEngine.GameEngine;

namespace TanksGameEngine.TanksGame.Objects.MapElements
{
    public class Barrel : PrimitiveObject
    {
        public Barrel(string name) : base(name)
        {

        }

        public override void Collide(GameObject gameObject, System.Windows.Vector direction, double overlap)
        {
            base.Collide(gameObject, direction, overlap);

            Damage(overlap * 5);
        }

        public override void Crash()
        {
            base.Crash();

            if (this.Name != "boom" && this.Name != "Capsule")
                GameProcess.Current_Game.Builder.BuildBoom(this.locator, new Vector(64, 64), GameProcess.Current_Game.frameImages);

        }
    }
}
