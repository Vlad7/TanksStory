using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TanksGameEngine.TanksGame.Objects.Characters;


namespace TanksGameEngine.GameEngine.Controllers
{
    abstract public class Controller
    {
        /// <summary>
        /// Get angle from direction method
        /// </summary>
        /// <param name="direct"></param>
        /// <returns></returns>
        public static Double DirectionToAngle(Vector direct)
        {
            Double angle = Math.Atan2(direct.Y, direct.X) * (180 / Math.PI);

            if (angle < 0)
            {
                angle += 360;
            }

            return angle;
        }

        /// <summary>
        /// Get dirrection from angle method
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Vector AngleToDirection(Double angle)
        {
            Vector dir = new Vector();

            dir.X = Math.Cos(angle * Math.PI / 180);
            dir.Y = Math.Sin(angle * Math.PI / 180);

            return dir;
        }

        /// <summary>
        /// Calculate rotate sign
        /// </summary>
        /// <param name="delta_angle"></param>
        /// <returns></returns>
        public static Int32 CalculateRotateSign(double delta_angle)
        {
            int sign = 0;

            if (delta_angle >= 180)
            {
                delta_angle -= 360;
            }
            else if (delta_angle < -180)
            {
                delta_angle += 360;
            }

            if (delta_angle > 0) sign = -1;
            else sign = 1;

            return sign;
        }



    

        public Controller()
        {
            
        }  


        public virtual void UpdateTargetDirection(Vector direction, Int32 dt)
        {
        }
       


        public abstract void Update(Int32 dt);
    }
}
