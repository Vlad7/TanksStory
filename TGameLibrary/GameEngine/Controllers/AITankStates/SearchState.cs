using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TanksGameEngine.GameEngine.Controllers.AITankStates
{
    public class SearchState : TankState
    {
        public override void Update(AITankController controller, Int32 dt)
        {
            if (!IsAttackState(controller))
            {
                List<Vector> path = GetPathToTarget(controller);

                if (path != null)
                    MoveObject(controller, path, dt);
                else return;
            }
            else
            {
                controller.CurrentState = new AttackState();
            }     
        }



        private void MoveObject(AITankController controller, List<Vector> path, Int32 dt)
        {
            Vector direction = GameProcess.Current_Game.Player.AbsoluteCenter - controller.Target.Gun.AbsoluteCenter;
            direction.Normalize();

            controller.UpdateGunDirection(direction, dt);

            Vector moveCommand = new Vector(0, 0);

            foreach (Vector pathNode in path)
            {
                Vector nearDistance = pathNode - controller.Target.AbsoluteCenter;

                if (nearDistance.LengthSquared > controller.Target.Engine.translateSpeed.LengthSquared)
                {
                    int delta_angle = (int)(controller.Target.AbsoluteAngle - Controller.DirectionToAngle(nearDistance));

                    if (Math.Abs(delta_angle) > 5)
                    {
                        moveCommand.X = Controller.CalculateRotateSign(delta_angle);
                    }
                    else
                    {
                        moveCommand.Y = 1;
                    }
  
                    controller.UpdateTargetDirection(moveCommand, dt);

                    return;
                }
                else
                {
                    continue;
                }
            } 
        }

        private List<Vector> GetPathToTarget(AITankController controller)
        {
            Map map = GameProcess.Current_Game.gameMap;

            Vector tCorner = map.GetSectorLocation(controller.Target.AbsoluteCenter);
            Vector pCorner = map.GetSectorLocation(GameProcess.Current_Game.Player.AbsoluteCenter);
           
            Point start = new Point((int)tCorner.X, (int)tCorner.Y);
            Point goal = new Point((int)pCorner.X, (int)pCorner.Y);

            

            List<Point> path = AStarSearch.FindPath(map, start, goal);

            

            if (path == null)
            {
                return null;
            }
            else
            {
                List<Vector> vPath = new List<Vector>();

                for (int i = 1; i < path.Count; i++)
                {
                    Vector v = new Vector(map.TileSize * (path[i].X + 0.5), map.TileSize * (path[i].Y + 0.5));
                    vPath.Add(v);
                }

                return vPath;
            }
        }
    }
}
