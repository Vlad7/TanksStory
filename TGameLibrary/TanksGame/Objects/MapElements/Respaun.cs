using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TanksGameEngine.GameEngine;

namespace TanksGameEngine.TanksGame.Objects.MapElements
{
    public class Respaun : PrimitiveObject
    {
        public Respaun(string name)
            : base(name)
        {

        }

        public override bool IsSolid()
        {
            return false;
        }
    }
}
