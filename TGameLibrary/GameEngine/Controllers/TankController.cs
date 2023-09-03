using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TanksGameEngine.TanksGame.Objects.Characters;

namespace TanksGameEngine.GameEngine.Controllers
{
    public class TankController : Controller
    {
        public Tank Target { get; set; }
   

        public TankController(Tank target)
        {
            Target = target;

        }

        public override void UpdateTargetDirection(Vector direction, Int32 dt)
        {
            if (direction.X == 0 && direction.Y == 0)
            {
                Target.Engine.Enabled = false;
            }
            else
            {
                Target.Engine.Enabled = true;

                if (direction.Y != 0)
                {
                    Target.Engine.rotationAcceleration = direction.X * direction.Y  * dt * 2;

                }
                else
                {
                    Target.Engine.rotationAcceleration = direction.X  * dt * 2;
                }

                //MessageBox.Show(Engine.rotationAcceleration.ToString());

                Target.Engine.translateAcceleration = Controller.AngleToDirection(Target.AbsoluteAngle) * direction.Y * 100;

                //MessageBox.Show(Engine.translateAcceleration.ToString());
            }

            Target.Engine.translateSpeed /= 1.2;
            Target.Engine.rotationSpeed /= 1.3;
         

           
            
        }


        public virtual void UpdateGunDirection(Vector direction, Int32 dt)
        {
            int delta_angle = (int)(Target.Gun.AbsoluteAngle - DirectionToAngle(direction));

            if (Math.Abs(delta_angle) >= 5)
            {
                Target.Gun.LocalAngle += CalculateRotateSign(delta_angle) * dt * 0.1;
            }
        }

        public override void Update(Int32 dt)
        {
            
        }
    }
}
