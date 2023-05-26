using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TanksGameEngine.GameEngine.Components
{
    /// <summary>
    /// Animation class
    /// </summary>
    public class Animation : Viewer
    {
        /// <summary>
        /// Animation frames
        /// </summary>
        private List<Frame> frames;
                                                              
        /// <summary>
        /// Current frame index
        /// </summary>
        private int? currentFrame;

        /// <summary>
        /// Time ellapsed in each frame
        /// </summary>
        private int frameTimeEllapsed;

        /// <summary>
        /// Animation constructor withot frames
        /// </summary>
        public Animation()
        {
            frames = new List<Frame>();

            currentFrame = null;

            frameTimeEllapsed = 0;

            Sprite = null;

            Enabled = false;
        }

        /// <summary>
        /// Animation constructor with frame list
        /// </summary>
        /// <param name="frames"></param>
        public Animation(List<Frame> frames)
        {                          
            this.frames = frames;

            if (frames.Count() == 0)
                currentFrame = null;
            else
                currentFrame = 0;

            frameTimeEllapsed = 0;

            Sprite = frames[(int)currentFrame].Sprite;

            Enabled = true;
        }

        /// <summary>
        /// Add frame to frame list
        /// </summary>
        /// <param name="frame"></param>
        public void AddFrame(Frame frame)
        {
            frames.Add(frame);

            currentFrame = 0;

            Sprite = frames[(int)currentFrame].Sprite;

            Enabled = true;
        }

        /// <summary>
        /// Remove frame from frame list
        /// </summary>
        /// <param name="frame"></param>
        public void RemoveFrame(Frame frame)
        {
            frames.Remove(frame);

            if (frames.Count == 0)
            {
                currentFrame = null;
                Sprite = null;
                Enabled = false;
            }

            frameTimeEllapsed = 0;
        }

        /// <summary>
        /// Clear frames from frame list
        /// </summary>
        public void ClearFrames()
        {
            frames.Clear();

            currentFrame = null;

            Sprite = null;

            frameTimeEllapsed = 0;

            Enabled = false;
        }

        /// <summary>
        /// Update animation state(frame)
        /// </summary>
        public override void Update(Int32 dt)
        {               
            if (currentFrame != null)
            {
                frameTimeEllapsed += dt;

                if (frameTimeEllapsed >= frames[(int)currentFrame].Duration)
                {
                    currentFrame = (currentFrame + 1) % frames.Count();
                                                                 
                    Sprite = frames[(int)currentFrame].Sprite;

                    frameTimeEllapsed = 0;
                }
                
            }
            else
            {
                Enabled = false;
            }
        }

        /// <summary>
        /// Reset animation to start frame
        /// </summary>
        public override void Reset()
        {
            if (currentFrame != null)
            {
                currentFrame = 0;

                Sprite = frames[(int)currentFrame].Sprite;
            }
            else
            {
                Enabled = false;
                Sprite = null;
            }
        }
    }  
}
