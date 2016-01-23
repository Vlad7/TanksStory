using System;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace TanksGameEngine.GameEngine.Components
{
    /// <summary>
    /// Viewer class. It draw all objects in camera view in realtime
    /// </summary>
    public class Viewer
    {
        /// <summary>
        /// IsEnabled property. Show or hide object view on render.
        /// </summary>
        public Boolean Enabled { get; set; }

        /// <summary>
        /// Sprite of viewer
        /// </summary>
        public Sprite  Sprite  { get; set; }

        /// <summary>
        /// Viewer constructor
        /// </summary>
        /// <param name="sprite"></param>
        public Viewer()
        {
            Sprite = null;

            Enabled = false;
        }

        /// <summary>
        /// Viewer constructor
        /// </summary>
        /// <param name="sprite"></param>
        public Viewer(Sprite sprite)
        {
            Sprite = sprite;

            Enabled = true;
        }   

        /// <summary>
        /// Viewer update method
        /// </summary>
        public virtual void Update(Int32 dt) { }

        /// <summary>
        /// Reset method
        /// </summary>
        public virtual void Reset() { }
    }
}