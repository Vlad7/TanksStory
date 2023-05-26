namespace TanksGameEngine.GameEngine.Components
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    /// <summary>
    /// A special class that links game objects to game world by coordinates, angles and zIndex.
    /// </summary>
    public class Locator
    {
        /// <summary>
        /// The angle of rotation of the object relative to the world space
        /// </summary>
        public Double Angle { get; set; }

        /// <summary>
        /// Vector of the center of object relative to the world space
        /// </summary>
        public Vector Center { get; set; }  

        /// <summary>
        /// // Index of placing the object on the plane of world space (the layer from 0 to 255 )
        /// </summary>
        public Int32  ZIndex { get; set; }  
      
        /// <summary>
        /// Locator constructor
        /// </summary>
        public Locator()
        {        
            this.Center = new Vector(0, 0);
            this.Angle = 0;
            this.ZIndex = 0;        
        }     

        /// <summary>
        /// Second locator class constructor
        /// </summary>
        /// <param name="gl_center"></param>
        /// <param name="gl_size"></param>
        /// <param name="gl_angle"></param>
        /// <param name="z_index"></param>
        public Locator(Vector gl_center, Double gl_angle, Int32 z_index)
        {
            this.Center = gl_center;
            this.Angle = gl_angle;  
            this.ZIndex = z_index;
        }  
    }

    /// <summary>
    /// Center changed arguments
    /// </summary>
    public class LocationChangedArgs : EventArgs
    {
        /// <summary>
        /// Mouse changed arguments constructor
        /// </summary>
        /// <param name="pos"></param>
        public LocationChangedArgs(Vector center, double angle, int zindex)
        {
            this.Center = center;
            this.Angle = angle;
            this.ZIndex = zindex;
        }

        /// <summary>
        /// Target object position property
        /// </summary>
        public Vector Center
        {
            get;
            private set;
        }

        /// <summary>
        /// Target object angle property
        /// </summary>
        public Double Angle
        {
            get;
            private set;
        }

        /// <summary>
        /// Target object zIndex property
        /// </summary>
        public Int32 ZIndex
        {
            get;
            private set;
        }
    } 
}