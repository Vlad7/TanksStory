using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TanksGameEngine.GameEngine
{
    public static class SoundManager
    {
        [DllImport("winmm.dll")]
        static extern Int32 mciSendString(string command, StringBuilder buffer, int bufferSize, IntPtr hwndCallback);
      
        static SoundManager()
        {
            mciSendString(@"open Sounds/boom.wav type waveaudio alias sound", null, 0, IntPtr.Zero);
        }  
     
        public static void PlayBoomSound()
        {
            mciSendString("seek sound to start", null, 0, IntPtr.Zero);
            mciSendString(@"play sound", null, 0, IntPtr.Zero);

            //mciSendString(@"close sound", null, 0, IntPtr.Zero);
        }

      
    }
}
