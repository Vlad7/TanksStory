using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using TanksGameEngine.GameEngine.Components;
using TanksGameEngine.TanksGame.Objects.Characters;
using TiledSharp;

namespace TanksGameEngine.GameEngine
{
    public class MapBuilder
    {
        protected Map map = null;

        public MapBuilder()
        {

        }

        public void BuildMap(string name, int rows, int columns, int tileSize)
        {
            map = new Map(name, rows, columns, tileSize);
        }

        public Map GetMap()
        {
            return map;
        }

        /// <summary>
        /// Transparent object is an object without collision shape.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="loc"></param>
        /// <param name="viewer"></param>
        public virtual PrimitiveObject BuildLandscapeTile(Locator loc, Viewer viewer)
        {
            PrimitiveObject obj = new PrimitiveObject();

            obj.LocalCenter = loc.Center;
            obj.LocalAngle  = loc.Angle;
            obj.LocalZIndex = loc.ZIndex;

            obj.Viewer = viewer;
            obj.CollisionShape = CollisionShape.None;

            obj.Size = new Vector(map.TileSize / 2, map.TileSize / 2);

            this.map.AddObject(obj);

            return obj;
        }

        public virtual void BuildSpecialObject(string name, String type, Locator loc, Vector size, CollisionShape shape, Viewer viewer)
        {
            BuildStandartObject(name, loc, size, shape, viewer);
        }

        public PrimitiveObject BuildStandartObject(string name, Locator loc, Vector size, CollisionShape shape, Viewer viewer)
        {
            PrimitiveObject obj = new PrimitiveObject(name);

            obj.Viewer = viewer;

            obj.LocalCenter = loc.Center;
            obj.LocalAngle  = loc.Angle;
            obj.LocalZIndex = loc.ZIndex;

            obj.CollisionShape = shape;
            obj.Size = size;

            map.AddObject(obj);
            
            obj.LocationChanged += map.UpdateObject;
            obj.LocationChanged += GameProcess.Current_Game.Manager.RegisterObject;

            obj.Crashed += map.RemoveObject;
            obj.Crashed += GameProcess.Current_Game.Camera.TryRemoveIndicator;
            obj.Crashed += GameProcess.Current_Game.Manager.UnRegisterObject;

            obj.LifeChanged += GameProcess.Current_Game.Camera.IndicateLifeChange;

            return obj;
        }    
    }
}
