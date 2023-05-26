using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TanksGameEngine.GameEngine.Components;
using TiledSharp;

namespace TanksGameEngine.GameEngine
{
    public static class Resources
    {
        public static string mapsFolderPath = "Maps\\";

        private static Dictionary<String, BitmapImage> ImageDictionary = new Dictionary<String, BitmapImage>();

        public static void AddImage(String key, BitmapImage image)
        {
            if(!ImageDictionary.Keys.Contains(key))
            {
                ImageDictionary.Add(key, image);
            }   
        }

        public static void RemoveImage(String key)
        {
            if(ImageDictionary.ContainsKey(key))
            {
                ImageDictionary.Remove(key);
            }           
        }

        public static BitmapImage GetImage(String key)
        {
            return ImageDictionary[key];
        }

        public static void ClearImages()
        {
            ImageDictionary.Clear();
        }
    }
}
