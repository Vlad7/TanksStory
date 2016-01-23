using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TanksGameEngine.GameEngine.Components
{
    /// <summary>
    /// Sprite class
    /// </summary>
    public class Sprite
    {
        /// <summary>
        /// This method create drawing visual by bitmap image, widht and height
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static DrawingVisual CreateVisual(BitmapImage image, int width, int height)
        {
            DrawingVisual visual = new DrawingVisual();

            using (DrawingContext dc = visual.RenderOpen())
            {
                System.Windows.Size size = new System.Windows.Size(width, height);

                dc.DrawImage(image, new System.Windows.Rect(new System.Windows.Point(0, 0), size));
            }

            return visual;
        }

        /// <summary>
        /// Sprite width
        /// </summary>
        private Int32 Width;

        /// <summary>
        /// Sprite height
        /// </summary>
        private Int32 Height;

        /// <summary>
        /// Drawing visual for sprite
        /// </summary>
        public DrawingVisual DrawingVisual { get; set; }
  
        /// <summary>
        /// This method get bitmap image of sprite
        /// </summary>
        /// <returns></returns>
        public BitmapImage GetBitmapImage()
        {
            RenderTargetBitmap bitmap = new RenderTargetBitmap(Width, Height, 96, 96,
                                                         PixelFormats.Pbgra32);
            bitmap.Render(DrawingVisual);
       
            var bitmapImage = new BitmapImage();
            var bitmapEncoder = new PngBitmapEncoder();

            bitmapEncoder.Frames.Add(BitmapFrame.Create(bitmap));

            using (var stream = new MemoryStream())
            {
                bitmapEncoder.Save(stream);
                stream.Seek(0, SeekOrigin.Begin);

                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
            }

            return bitmapImage;
        }

        /// <summary>
        /// This method set bitmap image to sprite
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetBitmapImage(BitmapImage image, int width, int height)
        {
            this.Width  = width;
            this.Height = height;

            DrawingVisual = CreateVisual(image, width, height);
        }

        /// <summary>
        /// First constructor. Parametres: image path, width and height
        /// </summary>
        /// <param name="imagePath"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Sprite(String imagePath)
        {
            BitmapImage image = new BitmapImage(new Uri(imagePath, UriKind.Relative));

            Width = image.PixelWidth;
            Height = image.PixelHeight;

            DrawingVisual = CreateVisual(image, Width, Height);
        }

        /// <summary>
        /// Second constructor. Parametres: image, width, height
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Sprite(BitmapImage image)
        {
            Width = image.PixelWidth;
            Height = image.PixelHeight; 

            DrawingVisual = CreateVisual(image, Width, Height);
        }       

        /// <summary>
        /// First constructor. Parametres: image path, width and height
        /// </summary>
        /// <param name="imagePath"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Sprite(String imagePath, int width, int height)
        {
            this.Width  = width;
            this.Height = height;

            BitmapImage image = new BitmapImage(new Uri(imagePath, UriKind.Relative));

            DrawingVisual = CreateVisual(image, width, height);
        }

        /// <summary>
        /// Second constructor. Parametres: image, width, height
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Sprite(BitmapImage image, int width, int height)
        {
            this.Width  = width;
            this.Height = height;

            DrawingVisual = CreateVisual(image, width, height);
        }
    }
}
