using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanksGameEngine.GameEngine.Components
{
    /// <summary>
    /// Animation frame class
    /// </summary>
    public class Frame
    {
        /// <summary>
        /// Frame constructor. Parametres: sprite and frame duration in milliseconds
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="duration"></param>
        public Frame(Sprite sprite, Int32 duration)
        {
            Sprite = sprite;
            Duration = duration;
        }

        /// <summary>
        /// Sprite property
        /// </summary>
        public Sprite Sprite { get; set; }

        /// <summary>
        /// Duration property
        /// </summary>
        public Int32 Duration { get; set; }
    }
}
