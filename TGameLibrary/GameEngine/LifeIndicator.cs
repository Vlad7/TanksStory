using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace TanksGameEngine.GameEngine
{
    public class LifeIndicator
    {
        public GameObject TargetObject { get; set; }

        //public Int32 RestLife { get; set; }

        public Int32 ShowTime  { get; set; }

        public Int32 RestTime  { get; set; }

        public LifeIndicator(GameObject gameObject, Int32 showtime)
        {
            TargetObject = gameObject;

            ShowTime = showtime;
            RestTime = showtime;

            //RestLife = TargetObject.Life;
        }

        public void Update(Int32 timeMilliseconds)
        {
            RestTime -= timeMilliseconds;
        }

        public void Reset()
        {
            RestTime = ShowTime;
        }

      
    }
}
