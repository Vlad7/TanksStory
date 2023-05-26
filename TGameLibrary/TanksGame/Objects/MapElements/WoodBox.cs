using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TanksGameEngine.GameEngine;

namespace TanksGameEngine.TanksGame.Objects.MapElements
{
    public class WoodBox : PrimitiveObject
    {
        public WoodBox(string name)
            : base(name)
        {

        }

        public override long GetWeight()
        {
            return 100;
        }

        public override void Collide(GameObject gameObject, System.Windows.Vector direction, double overlap)
        {
                long summWeight = gameObject.GetWeight() + this.GetWeight();

                this.LocalCenter -= direction * overlap * (gameObject.GetWeight() / summWeight);
               // MessageBox.Show("dd");

       

            Damage(overlap * 2);
        }

        public override void Crash()
        {
            base.Crash();

            if (this.Name != "boom" && this.Name != "Capsule")
                GameProcess.Current_Game.Builder.BuildBoom(this.locator, new Vector(64, 64), GameProcess.Current_Game.frameImages);

        }
    }
}
