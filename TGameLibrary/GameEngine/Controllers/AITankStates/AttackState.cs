using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TanksGameEngine.GameEngine.Controllers.AITankStates
{
    public class AttackState : TankState
    {
        public override void Update(AITankController controller, Int32 dt)
        {
            if (IsAttackState(controller))
            {
                Attack(controller, dt);
            }
            else
            {
                controller.CurrentState = new SearchState();
            }              
        }

        private void Attack(AITankController controller, Int32 dt)
        {
            //Look at situation when direction not updates
            controller.UpdateTargetDirection(new Vector(0, 0), dt);

            Vector direction = GameProcess.Current_Game.Player.AbsoluteCenter - controller.Target.Gun.AbsoluteCenter;
            direction.Normalize();

            controller.UpdateGunDirection(direction, dt);


            if (Math.Abs(controller.Target.Gun.AbsoluteAngle - Controller.DirectionToAngle(direction)) < 5)
                controller.Target.Fire();

           
        }
    }
}
