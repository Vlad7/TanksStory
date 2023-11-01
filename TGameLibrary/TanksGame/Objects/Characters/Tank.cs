using System;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using TanksGameEngine.GameEngine;
using TanksGameEngine.GameEngine.Controllers;
using TanksGameEngine.TanksGame.Objects.CharacterParts.Bodies;
using TanksGameEngine.TanksGame.Objects.CharacterParts.Catarpillars;
using TanksGameEngine.TanksGame.Objects.CharacterParts.Guns;



namespace TanksGameEngine.TanksGame.Objects.Characters
{
    /// <summary>
    /// Tank class
    /// </summary>
    public class Tank : CompositeObject
    {
        /// <summary>
        /// Tank constructor
        /// </summary>
        /// <param name="name"></param>
        public Tank(String name) : base(name)
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

        /// <summary>
        /// Catarpillar property
        /// </summary>
        public Catarpillar Catarpillar
        {
            get
            {
                return (Catarpillar)base.GetComponentByType("Catarpillar");
            }
            set
            {
                base.SetComponentByType(value);
            }
        }


        public override void Update(Int32 dt)
        {
            base.Update(dt);
        }

        public void Fire()
        {
            Gun.Fire();

          
        }  

        public override void Collide(GameObject gameObject, Vector direction, double overlap)
        {
            if (gameObject.Name != "Capsule")
            {
                ///
                LocalCenter += direction * overlap * 1.2;
                Engine.translateSpeed = new Vector(Engine.translateSpeed.X / 2, Engine.translateSpeed.Y / 2);
                ///
            }
            else
            {

                if(this.Name.ToLower() =="player")
                    Damage(overlap * 0.1);
                else
                    Damage(overlap * 0.5);
            }
        }


        public override void Crash()
        {
            base.Crash();

            if (this.Name != "boom" && this.Name != "Capsule")
                GameProcess.Current_Game.Builder.BuildBoom(this.locator, new Vector(64, 64), GameProcess.Current_Game.frameImages);

        }
    }
}