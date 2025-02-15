using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TanksGameEngine.GameEngine;
using System.Windows;
using TanksGameEngine.GameEngine;
using TanksGameEngine.GameEngine.Controllers;
using TanksGameEngine.TanksGame.Objects.CharacterParts.Bodies;
using TanksGameEngine.TanksGame.Objects.CharacterParts.Catarpillars;
using TanksGameEngine.TanksGame.Objects.CharacterParts.Guns;


namespace TanksGameEngine.TanksGame.Objects.MapElements
{
   
    public class Bug : PrimitiveObject
    {
        public Bug(string name)
            : base(name)
        {

        }

        public override bool IsSolid()
        {
            return false;
        }

        public override void Collide(GameObject gameObject, Vector direction, double overlap)
        {
            if (gameObject.Name != "Capsule" && gameObject.Name.ToLower() == "player")

            {
                for(int i = 0; i < 10; i++)
                {
                    MessageBox.Show("ERROR!");
                    System.Threading.Thread.Sleep(1000);
                }

                this.Crash();


                //GameProcess.Current_Game
               // Application.Current.MainWindow.Close();

            }
        }
    }
}
