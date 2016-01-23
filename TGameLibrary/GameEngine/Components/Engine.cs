using System.Collections.Generic;
using System.Windows;
using System;



namespace TanksGameEngine.GameEngine.Components
{
    /// <summary>
    /// Tank engine class
    /// </summary>
    public class Engine
    {
        /// <summary>
        /// Is enable property
        /// </summary>
        public Boolean Enabled { get; set; }

        /// <summary>
        /// Engine translation speed property
        /// </summary>
        public Vector translateSpeed { get; set; }

        /// <summary>
        /// Tank acceleration property
        /// </summary>
        public Vector translateAcceleration { get; set; }

        /// <summary>
        /// Engine tank rotation speed property
        /// </summary>
        public Double rotationSpeed { get; set; }

        /// <summary>
        /// Engine tower rotate speed property
        /// </summary>
        public Double rotationAcceleration { get; set; }
         
        /// <summary>
        /// Engine constructor
        /// </summary>
        public Engine()
        {
            this.translateSpeed = new Vector(0, 0);
            this.translateAcceleration = new Vector(0, 0);
            this.rotationSpeed = 0;
            this.rotationAcceleration = 0;

            Enabled = false;
        }

        /// <summary>
        /// Engine full constructor
        /// </summary>
        /// <param name="tr_speed"></param>
        /// <param name="tr_acc"></param>
        /// <param name="rot_speed"></param>
        /// <param name="rot_acc"></param>
        public Engine(Vector tr_speed, Vector tr_acc, double rot_speed, double rot_acc)
        {
            this.translateSpeed = tr_speed;
            this.translateAcceleration = tr_acc;
            this.rotationSpeed = rot_speed;
            this.rotationAcceleration = rot_acc;

            Enabled = false;
        }

        /// <summary>
        /// Engine update method
        /// </summary>
        public void Update(Int32 dtMSec)
        {
            if (this.Enabled)
            {
                this.translateSpeed += this.translateAcceleration * dtMSec / 1000;
                this.rotationSpeed += this.rotationAcceleration * dtMSec / 1000;
            }
        }    
    }
}