using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using TanksGameEngine.GameEngine.Components;
using TiledSharp;

namespace TanksGameEngine.GameEngine
{
    public class TmxBuildDirrector
    {
        private Dictionary<Int32, ResourceImage> tmxTilesetImages;

        private Dictionary<Int32, List<ResourceFrame>> tmxAnimations; 

        private TmxMap tmxMap;
 
        private String tmxName;

        private MapBuilder builder;

        /// <summary>
        /// This is constructor of TmxBuildDirector
        /// </summary>
        /// <param name="mapName"></param>
        /// <param name="builder"></param>
        public TmxBuildDirrector(String mapName, MapBuilder builder)
        {
            this.tmxName = mapName.Split('.')[0];

            this.tmxMap = new TmxMap(Resources.mapsFolderPath + mapName);

            this.builder = builder;

            this.tmxTilesetImages = new Dictionary<Int32, ResourceImage>();

            this.tmxAnimations = new Dictionary<Int32, List<ResourceFrame>>();

            LoadImagesFromTilesets(tmxMap.Tilesets);

            LoadAnimationsFromTilesets(tmxMap.Tilesets);
        }

        /// <summary>
        /// This method creates map
        /// </summary>
        public void CreateMap()
        {
            builder.BuildMap(tmxName, tmxMap.Height, tmxMap.Width, tmxMap.TileHeight);

            PrimitiveObject._initialObjSize = tmxMap.Height / 2;        
        }

        /// <summary>
        /// This method creates all objects of map
        /// </summary>
        public void CreateObjects()
        {
            for (int i = 0; i < tmxMap.ObjectGroups.Count; i++)
            {
                TmxObjectGroup objGroup = tmxMap.ObjectGroups[i];

                for (int j = 0; j < objGroup.Objects.Count; j++)
                {
                    CreateOneObject(objGroup.Objects[j], i, objGroup.Visible);
                }
            }
        }

        /// <summary>
        /// This method create one specific object
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="zIndex"></param>
        private void CreateOneObject(TiledSharp.TmxObjectGroup.TmxObject obj, Int32 zIndex, Boolean isLayerVisible)
        {
            String name = obj.Name;

            String type = obj.Type == null ? String.Empty : obj.Type;

            Vector size = GetObjectSize(obj);

            Double rotation = GetObjectRotation(obj);

            Vector center = new Vector(obj.X + size.X, obj.Y - size.Y);

            Locator loc = new Locator(center, rotation, tmxMap.Layers.Count + zIndex);

            Viewer viewer = GetObjectView(obj.Tile, isLayerVisible && obj.Visible);

            CollisionShape shape = GetObjectShape(obj);

            builder.BuildSpecialObject(name, type, loc, size, shape, viewer);          
        }


        /// <summary>
        /// This method return rotation angle of object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private Double GetObjectRotation(TiledSharp.TmxObjectGroup.TmxObject obj)
        {
            Double rotation = -obj.Rotation;

            while (rotation < 0) rotation += 360;

            rotation %= 360;

            return rotation;
        }

        /// <summary>
        /// This method create landscape - map from tiles, that has view, but hasn't collider.
        /// </summary>
        public void CreateLandscape()
        {
            for (int i = 0; i < tmxMap.Layers.Count; i++)
            {
                TmxLayer layer = tmxMap.Layers[i];

                for (int j = 0; j < layer.Tiles.Count; j++)
                {
                    if (layer.Tiles[j].Gid == 0) continue;

                    Viewer viewer = GetObjectView(layer.Tiles[j], layer.Visible);

                    int row = j / tmxMap.Height;
                    int col = j % tmxMap.Width;

                    Vector center = new Vector((col + 0.5) * tmxMap.TileWidth, (row + 0.5) * tmxMap.TileWidth);

                    Locator loc = new Locator(center, 0, i);

                    builder.BuildLandscapeTile(loc, viewer);
                }
            }
        }

        /// <summary>
        /// This method check, what object collision type is(Circle or rectangle)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private CollisionShape GetObjectShape(TiledSharp.TmxObjectGroup.TmxObject obj)
        {
            CollisionShape shape = CollisionShape.None;

            switch (obj.ObjectType.ToString())
            {
                case "Basic":
                    {
                        shape = CollisionShape.Rectangle;
                    }
                    break;
                case "Circle":
                    {
                        shape = CollisionShape.Circle;
                    }
                    break;

                case "Tile":
                    {
                        shape = CollisionShape.Rectangle;
                    }
                    break;
            }

            return shape;
        }

        /// <summary>
        /// This method return object size
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private Vector GetObjectSize(TiledSharp.TmxObjectGroup.TmxObject obj)
        {
            Vector size = new Vector(0, 0);

            switch (obj.ObjectType.ToString())
            {
                case "Basic":
                    {
                        size = new Vector(obj.Width / 2, obj.Height / 2);
                    }
                    break;
                case "Circle":
                    {
                        size = new Vector(obj.Width / 2, obj.Height / 2);
                    }
                    break;

                case "Tile":
                    {
                        int sizeX = tmxMap.TileWidth / 2;
                        int sizeY = tmxMap.TileHeight / 2;

                        size = new Vector(sizeX, sizeY);
                    }
                    break;
            }

            return size;
        }

        /// <summary>
        /// This method creates object viewer by object view index, isVisible parameter and object type.
        /// It takes image from dictionary, loaded previously, by id.
        /// </summary>
        /// <param name="objType"></param>
        /// <param name="id"></param>
        /// <param name="isVisible"></param>
        /// <returns></returns>
        private Viewer GetObjectView(TmxLayerTile tile, Boolean isVisible)
        {
            Viewer viewer = null;

            if (tile != null)
            {
                viewer = CreateViewer(tile.Gid, isVisible);

                viewer.Enabled = isVisible;
            }

            return viewer;
        }

        /// <summary>
        /// This method check if for this object(it's tile) is animation. We search animation in
        /// tmxAnimations dictionary by global key. If we don't find it, we use simple viewer with
        /// one image from tmxTilesetImages.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isVisible"></param>
        /// <returns></returns>
        private Viewer CreateViewer(Int32 id, Boolean isVisible)
        {
            if (tmxAnimations.Keys.Contains(id))
            {
                return CreateAnimatedViewer(id, isVisible);
            }
            else
            {
                return CreateSimpleViewer(id, isVisible);
            }
        }

        /// <summary>
        /// This method creates simple viewer with view-image of object. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isVisible"></param>
        /// <returns></returns>
        private Viewer CreateSimpleViewer(Int32 id, Boolean isVisible)
        {
            ResourceImage resImg = tmxTilesetImages[id];

            WriteImageToResources(resImg);

            return new Viewer(new Sprite(resImg.Image));
        }

        /// <summary>
        /// This method load animation info from tilesets. Then we will use this information
        /// to create animated objects.
        /// </summary>
        /// <param name="tilesets"></param>
        private void LoadAnimationsFromTilesets(TmxList<TmxTileset> tilesets)
        {
            this.tmxAnimations.Clear();

            foreach (TmxTileset tileset in tilesets)
            {
                foreach (TmxTilesetTile tile in tileset.Tiles)
                {
                    if (tile.AnimationFrames.Count != 0)
                    {
                        Int32 tileKey = tileset.FirstGid + tile.Id;

                        List<ResourceFrame> animation = new List<ResourceFrame>();

                        foreach (var frame in tile.AnimationFrames)
                        {
                            Int32 frameKey = tileset.FirstGid + frame.Id;

                            ResourceFrame nFrame = new ResourceFrame(frameKey, frame.Duration);

                            animation.Add(nFrame);
                        }

                        tmxAnimations.Add(tileKey, animation);
                    }
                }
            }
        }

        /// <summary>
        /// This method create viewer with animation frames. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isVisible"></param>
        /// <returns></returns>
        private Animation CreateAnimatedViewer(Int32 id, Boolean isVisible)
        {
            List<Frame> animation = new List<Frame>();

            foreach (ResourceFrame frame in tmxAnimations[id])
            {
                ResourceImage resImg = tmxTilesetImages[frame.Key];

                Frame nFrame = new Frame(new Sprite(resImg.Image), frame.Duration);

                animation.Add(nFrame);

                WriteImageToResources(resImg);
            }

            return new Animation(animation);
        }

        /// <summary>
        /// This method push useful images to resources dictionary(Resources class). 
        /// Other images, that are not pushed will be removed.
        /// </summary>
        /// <param name="rImage"></param>
        private void WriteImageToResources(ResourceImage rImage)
        {
            Resources.AddImage(rImage.Key, rImage.Image);
        }

        /// <summary>
        /// This mehod loads images in dictionary by path discribed in tilesets list.
        /// For example, we have map made in Tiled. Tiled save map in XML format. This XML file contains
        /// information about tilesets in map. We load this information. Than we load images by this 
        /// information. We load all images, that may be used in map building in Tiled.
        /// </summary>
        /// <param name="tilesets"></param>
        /// <returns></returns>
        private void LoadImagesFromTilesets(TmxList<TmxTileset> tilesets)
        {
            this.tmxTilesetImages.Clear();

            try
            {
                foreach (TmxTileset tileset in tilesets)
                {
                    LoadTilesetImages(tileset);
                }
            }

            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);

            }
        }

        /// <summary>
        /// This method help us to check if tileset is the set of images or one solid image
        /// that we have to crop.
        /// </summary>
        /// <param name="tileset"></param>
        private void LoadTilesetImages(TmxTileset tileset)
        {
            if (tileset.Image.Source == null)
            {
                LoadImagesFromImageSet(tileset);
            }
            else
            {
                LoadImagesFromCropedImage(tileset);
            }
        }

        /// <summary>
        /// This method load images from image set. Every tile is the same image. At the end, 
        /// it add this images to dictionary with unic global key, made from offset of 
        /// tile image relative to tileset.
        /// </summary>
        /// <param name="tileset"></param>
        private void LoadImagesFromImageSet(TmxTileset tileset)
        {
            for (int i = 0; i < tileset.Tiles.Count; i++)
            {
                int width  =  tileset.Tiles[i].Image.Width.Value;
                int height = tileset.Tiles[i].Image.Height.Value;

                BitmapImage sourceBitmap = CreateBitmapImage(tileset.Tiles[i].Image.Source, width, height);

                string key = tileset.Name + "." + i.ToString();

                tmxTilesetImages.Add(tileset.FirstGid + i, new ResourceImage(key, sourceBitmap));
            }
        }

         /// <summary>
        /// This method load images by tileset image source. Then it cropes this image to tiles
        /// of the same size. At the end, it add this tile-images to dictionary with unic global
        /// key, made from offset of tile relative tileset.
        /// </summary>
        /// <param name="tileset"></param>
        private void LoadImagesFromCropedImage(TmxTileset tileset)
        {
            int tileWidth = tileset.TileWidth;
            int tileHeight = tileset.TileHeight;

            int imageWidth = tileset.Image.Width.Value;
            int imageHeight = tileset.Image.Height.Value;

            int rowCount = imageHeight / tileHeight;
            int colCount = imageWidth / tileWidth;

            BitmapImage sourceBitmap = CreateBitmapImage(tileset.Image.Source, imageWidth, imageHeight);

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    int offset = tileset.FirstGid + i * colCount + j;

                    Int32Rect rect = new Int32Rect(j * tileWidth, i * tileHeight, tileWidth, tileHeight);

                    BitmapImage croppedBmp = BitmapSourceToBitmapImage(new CroppedBitmap(sourceBitmap, rect));

                    string key = tileset.Name + "." + offset.ToString();

                    tmxTilesetImages.Add(offset, new ResourceImage(key, croppedBmp));
                }
            }

        }

        /// <summary>
        /// This method load images by tileset image source. Then it cropes this image to tiles
        /// of the same size. At the end, it add this tile-images to dictionary with unic global
        /// key, made from offset of tile relative tileset.
        /// </summary>
        /// <param name="tileset"></param>
        public static List<BitmapImage> GetImagesFromCropedImage(string source, int tileWidth, int tileHeight, int imageWidth, int imageHeight)
        {        
            List<BitmapImage> images = new List<BitmapImage>();

            int rowCount = imageHeight / tileHeight;
            int colCount = imageWidth / tileWidth;

              
            BitmapImage sourceBitmap = CreateBitmapImage(source, imageWidth, imageHeight);

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    //int offset = i * colCount + j;

                    Int32Rect rect = new Int32Rect(j * tileWidth, i * tileHeight, tileWidth, tileHeight);

                    BitmapImage croppedBmp = BitmapSourceToBitmapImage(new CroppedBitmap(sourceBitmap, rect));

                    images.Add(croppedBmp);                  
               }
            }

            return images;
        }

        /// <summary>
        /// This method create bitmap image by uri, width and height
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static BitmapImage CreateBitmapImage(String path, Int32 width, Int32 height)
        {
            BitmapImage sourceBitmap = new BitmapImage();         
     
            sourceBitmap.BeginInit();
            sourceBitmap.UriSource = new Uri(path, UriKind.Relative);
            sourceBitmap.DecodePixelHeight = width;
            sourceBitmap.DecodePixelWidth = height;
            sourceBitmap.EndInit();

            return sourceBitmap;
        }

     

        /// <summary>
        /// This method convert bitmap source to bitmap image.
        /// </summary>
        /// <param name="bitmapSource"></param>
        /// <returns></returns>
        private static BitmapImage BitmapSourceToBitmapImage(BitmapSource bitmapSource)
        {
            PngBitmapEncoder encoder = new PngBitmapEncoder();

            MemoryStream memoryStream = new MemoryStream();

            BitmapImage bImg = new BitmapImage();

            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
            encoder.Save(memoryStream);

            bImg.BeginInit();
            bImg.StreamSource = new MemoryStream(memoryStream.ToArray());
            bImg.EndInit();

            memoryStream.Close();

            return bImg;
        }
    }

    /// <summary>
    /// This is class that help us to control image content. It contains string key
    /// (based on tileset name and tile(image) offset in it) and BitmapImage of course.
    /// </summary>
    public class ResourceImage
    {
        public String Key { get; set; }

        public BitmapImage Image { get; set; }

        public ResourceImage(String key, BitmapImage image)
        {
            this.Key = key;
            this.Image = image;
        }
    }

    /// <summary>
    /// We use this class to describe resource animation frame. It is fictive class, because in
    /// game process we will not use it. But we use it to identify animation frames from 
    /// tmxResourceImages.
    /// </summary>
    public class ResourceFrame
    {
        /// <summary>
        /// It is a key of Resource image in tmxResources images
        /// </summary>
        public Int32 Key { get; set; }

        /// <summary>
        /// It is a duration of animation
        /// </summary>
        public Int32 Duration { get; set; }

        /// <summary>
        /// It is a constuctor of resource animation frame
        /// </summary>
        /// <param name="key"></param>
        /// <param name="duration"></param>
        public ResourceFrame(int key, int duration)
        {
            Key = key;
            Duration = duration;

        }
    }
}
