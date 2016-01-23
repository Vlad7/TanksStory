using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TanksGameEngine.GameEngine.Controllers.AITankStates
{
    public class TankState
    {
        public virtual void Update(AITankController controller, Int32 dt)
        {

        }

        protected bool IsAttackState(AITankController controller)
        {
            Vector distance = GameProcess.Current_Game.Player.AbsoluteCenter - controller.Target.Gun.AbsoluteCenter;

            if (distance.LengthSquared <= Math.Pow(controller.Target.Gun.MaxFireRange, 2))
            {
                return true;
            }

            return false;
        }
    }
}
