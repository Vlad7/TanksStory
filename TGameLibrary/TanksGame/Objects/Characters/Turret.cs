using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TanksGameEngine.GameEngine;
using TanksGameEngine.GameEngine.Controllers;
using TanksGameEngine.TanksGame.Objects.CharacterParts.Bodies;
using TanksGameEngine.TanksGame.Objects.CharacterParts.Guns;
using TanksGameEngine.TanksGame.Objects.Characters;


namespace TanksGameEngine.TanksGame.Objects.Characters
{
    public class Turret : CompositeObject
    {
        /// <summary>
        /// Tank constructor
        /// </summary>
        /// <param name="name"></param>
        public Turret(String name) : base(name)
        {

        }

        /// <summary>
        /// Body property
        /// </summary>
        public Body Body
        {
            get
            {
                return (Body)base.GetComponentByType("Body");
            }
            set
            {
                base.SetComponentByType(value);
            }
        }

        /// <summary>
        /// Gun property
        /// </summary>
        public Gun Gun
        {
            get
            {
                return (Gun)base.GetComponentByType("Gun");
            }
            set
            {
                base.SetComponentByType(value);
            }
        }     

        public void Fire()
        {
            Gun.Fire();
        }  
   
    }
}
