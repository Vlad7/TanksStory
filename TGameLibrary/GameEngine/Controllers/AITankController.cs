using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TanksGameEngine.GameEngine.Controllers.AITankStates;
using TanksGameEngine.TanksGame.Objects.Characters;


namespace TanksGameEngine.GameEngine.Controllers
{
    public class AITankController : TankController
    {
        public TankState CurrentState { get; set; }

        //PrimitiveObject Player { get; set; }

        public AITankController(Tank target) : base(target)
        {
            CurrentState = new SearchState();      
        }

        public override void Update(Int32 dt)
        {
            CurrentState.Update(this, dt);     
        }
    }
}
