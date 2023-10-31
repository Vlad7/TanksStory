using System.Windows;

using TiledSharp;
using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Linq;
using System.Diagnostics;

namespace TanksGameEngine.GameEngine
{
    /// <summary>
    /// Map class tha store all map information
    /// </summary>
    public class Map : ActiveRecord<Map>
    {
        /// <summary>
        /// Map grid
        /// </summary>
        private GridCorner[,] Grid;

        /// <summary>
        /// Object_corner dependences
        /// </summary>
        private Dictionary<PrimitiveObject, List<GridCorner>> Object_CornerDependences;

        /// <summary>
        /// Game objects store
        /// </summary>
        private List<GameObject> gameObjectsStore;

        /// <summary>
        /// Map name
        /// </summary>
        public String Name
        {
            get;
            set;
        }

        /// <summary>
        /// Map columns count
        /// </summary>
        public Int32 Columns
        {
            get;
            set;
        }

        /// <summary>
        /// Map rows count
        /// </summary>
        public Int32 Rows
        {
            get;
            set;
        }

        /// <summary>
        /// Map tile size
        /// </summary>
        public Int32 TileSize
        {
            get;
            set;
        }
     
        /// <summary>
        /// This method calculates indexes of borders that limit area in rectangle. This rectangle has
        /// two parametres: center position and size. If one of the indexes located outside map, it 
        /// return index of map border.
        /// </summary>
        /// <param name="center"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        private GridArea CalculateSubmapBorders(Vector center, Vector size)
        {
            int leftup_row = (int)((center.Y - size.Y) / TileSize);
            int leftup_column = (int)((center.X - size.X) / TileSize);
            int rightdown_row = (int)((center.Y + size.Y - 1) / TileSize);
            int rightdown_column = (int)((center.X + size.X - 1) / TileSize);

            if (leftup_row < 0)
            {
                leftup_row = 0;
            }
            if (leftup_column < 0)
            {
                leftup_column = 0;
            }
            if (rightdown_row > Rows - 1)
            {
                rightdown_row = Rows - 1;
            }
            if (rightdown_column > Columns - 1)
            {
                rightdown_column = Columns - 1;
            }

            return new GridArea(leftup_row, leftup_column, rightdown_row, rightdown_column);
        }

        /// <summary>
        /// Returns corners that placed in rectangle
        /// </summary>
        /// <param name="center"></param>
        /// <returns></returns>
        public List<GridCorner> GetCornersByArea(Vector center, Vector size, Double angle)
        {
            Vector rectSize;

            if (angle == 0)
            {
                rectSize = size;
            }

            else
            {
                Double angleRad = (angle % 180) * Math.PI / 180;

                if (angleRad > Math.PI / 2) angleRad = Math.PI - angleRad;

                Double newX = size.X * Math.Cos(angleRad) + size.Y * Math.Sin(angleRad);
                Double newY = size.X * Math.Sin(angleRad) + size.Y * Math.Cos(angleRad);

                rectSize = new Vector(newX, newY);
            }

            GridArea area = CalculateSubmapBorders(center, rectSize);

            List<GridCorner> Corners = new List<GridCorner>();

            for (int i = area.leftupRow; i <= area.rightdownRow; i++)
            {
                for (int j = area.leftupColumn; j <= area.rightdownColumn; j++)
                {
                    Corners.Add(Grid[i, j]);
                }
            }

            return Corners;
        }

        /// <summary>
        /// This method returns corner from collision grid by object vercicle position
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        private GridCorner GetCornerByPoint(Vector point)
        {
            int grid_row = (int)(point.Y / TileSize);
            int grid_column = (int)(point.X / TileSize);

            return Grid[grid_row, grid_column];
        }
       
        /// <summary>
        /// This method add primitive object to Map
        /// </summary>
        /// <param name="pObj"></param>
        private void AddPrimitiveObject(PrimitiveObject pObj)
        {
            ///Mayby optimize point collider
            List<GridCorner> busyCorners = GetCornersByArea(pObj.AbsoluteCenter, pObj.Size, pObj.AbsoluteAngle);

            foreach (GridCorner corner in busyCorners)
            {
                corner.ObjectsInCorner.Add(pObj);
            }

            Object_CornerDependences.Add(pObj, busyCorners);
        }

        /// <summary>
        /// This method remove primitive object from Map
        /// </summary>
        /// <param name="pObj"></param>
        private void RemovePrimitiveObject(PrimitiveObject pObj)
        {

            List<GridCorner> busyCorners = Object_CornerDependences[pObj];

            foreach (GridCorner corner in busyCorners)
            {
                corner.ObjectsInCorner.Remove(pObj);
            }

            Object_CornerDependences.Remove(pObj);
        }

        /// <summary>
        /// This method update primitive object in map
        /// </summary>
        /// <param name="pObj"></param>
        private void UpdatePrimitiveObject(PrimitiveObject pObj)
        {
            RemovePrimitiveObject(pObj);
            AddPrimitiveObject(pObj);
        }

        /// <summary>
        /// Map constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <param name="tileSize"></param>
        public Map(string name, int rows, int columns, int tileSize)
        {
            this.Name = name;

            this.Columns = columns;

            this.Rows = rows;

            this.TileSize = tileSize;

            this.Grid = new GridCorner[rows, columns];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                {
                    this.Grid[i, j] = new GridCorner();
                }

            this.Object_CornerDependences = new Dictionary<PrimitiveObject, List<GridCorner>>();

            this.gameObjectsStore = new List<GameObject>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="center"></param>
        /// <param name="size"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public List<PrimitiveObject> GetPrimitivesByArea(Vector center, Vector size, Double angle)
        {
            List<GridCorner> corners = GetCornersByArea(center, size, angle);

            List<PrimitiveObject> primitives = new List<PrimitiveObject>();

            foreach (GridCorner corner in corners)
            {
                foreach (PrimitiveObject obj in corner.ObjectsInCorner)
                {
                    primitives.Add(obj);
                }
            }

            return primitives.Distinct().ToList();
        }

        /// <summary>
        /// Get objects by area
        /// </summary>
        /// <param name="center"></param>
        /// <param name="size"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public List<GameObject> GetObjectsByArea(Vector center, Vector size, Double angle)
        {
            List<PrimitiveObject> primitives = GetPrimitivesByArea(center, size, angle);

            List<GameObject> gameObjects = new List<GameObject>();

            foreach (PrimitiveObject pObj in primitives)
            {
                GameObject obj = pObj;

                while (obj.Parent != null)
                {
                    obj = obj.Parent;
                }

                gameObjects.Add(obj);
            }

            return gameObjects.Distinct().ToList();
        }

        //public List<PrimitiveObject> GetFirstPrimitives(Vector start, Vector direction)
        //{
        //    double tMaxX, tMaxY, tDeltaX, tDeltaY;

        //    int stepX, stepY;

        //    if(direction.X > 0)
        //    {
        //        stepX = 1;
        //        tMaxX = (TileSize - start.X % TileSize) / direction.X;
        //        tDeltaX = TileSize / direction.X
        //    }
        //    else
        //    {
        //        if(direction.X < 0)
        //        {
        //            stepX = -1;
        //            tMaxX = (- start.X % TileSize) / direction.X;
        //            tDeltaX = - TileSize / direction.X
        //        }

        //        else
        //        {
        //            tDeltaX = 1000000000000;
        //        }
        //    }

        //    if(direction.Y > 0)
        //    {
        //        stepY = 1;
        //        tMaxY = (TileSize - start.Y % TileSize) / direction.Y;
        //        tDeltaY = TileSize / direction.Y
        //    }
        //    else
        //    {
        //        if(direction.Y < 0)
        //        {
        //            stepY = -1;
        //            tMaxY = (- start.Y % TileSize) / direction.Y;
        //            tDeltaY = - TileSize / direction.Y
        //        }
        //        else
        //        {
        //            tDeltaY = 1000000000000;
        //        }
        //    }      
            
        //    ///Check out boundaries
        //    Vector startCorner = GetSectorLocation(start);
           
        //    List<PrimitiveObject> primitives = new List<PrimitiveObject>();
        //    do 
        //    {
        //        if(tMaxX < tMaxY) 
        //        {                   
        //            startCorner.X += stepX;

        //            if(X == Rows || Y == - 1)
        //                return null; 

        //            tMaxX += tDeltaX;        
        //        } 
        //        else 
        //        {    
        //            startCorner.Y += stepY;

        //            if(Y == Rows || Y == - 1)
        //                return null;

        //            tMaxY += tDeltaY;
        //        } 
            
        //        primitives = GetColliderObjectsFromCorner(Grid[startCorner.Y, startCorner.X]); 
        //    } 
        //    while(primitives.Count == 0)

        //    return(list);
        //}

        //public Boolean CanSeeObject(Vector startPoint, Vector endPoint)
        //{
        //    Vector direction = endPoint - startPoint;

        //    direction.Normalize();

        //    double tMaxX, tMaxY, tDeltaX, tDeltaY;

        //    int stepX, stepY;

        //    if(direction.X > 0)
        //    {
        //        stepX = 1;
        //        tMaxX = (TileSize - start.X % TileSize) / direction.X;
        //        tDeltaX = TileSize / direction.X
        //    }
        //    else
        //    {
        //        if(direction.X < 0)
        //        {
        //            stepX = -1;
        //            tMaxX = (- start.X % TileSize) / direction.X;
        //            tDeltaX = - TileSize / direction.X
        //        }

        //        else
        //        {
        //            tDeltaX = 1000000000000;
        //        }
        //    }

        //    if(direction.Y > 0)
        //    {
        //        stepY = 1;
        //        tMaxY = (TileSize - start.Y % TileSize) / direction.Y;
        //        tDeltaY = TileSize / direction.Y
        //    }
        //    else
        //    {
        //        if(direction.Y < 0)
        //        {
        //            stepY = -1;
        //            tMaxY = (- start.Y % TileSize) / direction.Y;
        //            tDeltaY = - TileSize / direction.Y
        //        }
        //        else
        //        {
        //            tDeltaY = 1000000000000;
        //        }
        //    }      
            
        //    ///Check out boundaries
        //    Vector startCorner = GetSectorLocation(startPoint);
        //    Vector endCorner = GetSectorLocation(endPoint)
           
        //    List<PrimitiveObject> primitives = new List<PrimitiveObject>();
        //    do 
        //    {
        //        if(tMaxX < tMaxY) 
        //        {                   
        //            startCorner.X += stepX;

        //            if(X == Rows || Y == - 1)
        //                return null; 

        //            tMaxX += tDeltaX;        
        //        } 
        //        else 
        //        {    
        //            startCorner.Y += stepY;

        //            if(Y == Rows || Y == - 1)
        //                return null;

        //            tMaxY += tDeltaY;
        //        } 
            
        //        primitives = GetColliderObjectsFromCorner(Grid[startCorner.Y, startCorner.X]);            
        //    } 
        //    while(startCorner != endCorner)

        //    return false;

        //    //List<PrimitiveObject> primitives = GetFirstPrimitives(startPoint, direction);
        //}

        //private List<PrimitiveObject> GetColliderObjectsFromCorner(GridCorner corner)
        //{
        //    List<PrimitiveObject> colliderObjects = new List<PrimitiveObject>();

        //    foreach (PrimitiveObject pObj in corner.ObjectsInCorner)
        //    {
        //        if (pObj.CollisionShape == CollisionShape.Rectangle || pObj.CollisionShape == CollisionShape.Circle)
        //            colliderObjects.Add(pObj);
        //    }
        //}
                    

        /// <summary>
        /// Add object to map
        /// </summary>
        /// <param name="gObj"></param>
        public void AddObject(GameObject gObj)
        {
            gameObjectsStore.Add(gObj);

            List<PrimitiveObject> pObjects = gObj.GetPrimitives();

            foreach (PrimitiveObject pObj in pObjects)
            {
                AddPrimitiveObject(pObj);
            }
        }

        /// <summary>
        /// Remove object from map
        /// </summary>
        /// <param name="gObj"></param>
        public void RemoveObject(GameObject gObj)
        {
            gameObjectsStore.Remove(gObj);

            List<PrimitiveObject> pObjects = gObj.GetPrimitives();

            foreach (PrimitiveObject pObj in pObjects)
            {
                RemovePrimitiveObject(pObj);
            }
        }

        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="gObj"></param>
        public void UpdateObject(GameObject gObj)
        {
            List<PrimitiveObject> pObjects = gObj.GetPrimitives();

            foreach (PrimitiveObject pObj in pObjects)
            {
                UpdatePrimitiveObject(pObj);
            }
        }
      

        /// <summary>
        /// Method that clear map grid
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Columns; j++)
                {
                    Grid[i, j].ObjectsInCorner.Clear();
                }

            Object_CornerDependences.Clear();

            gameObjectsStore.Clear();
        }

        /// <summary>
        /// This method return corner location in grid by point
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Vector GetSectorLocation(Vector point)
        {
            int grid_row = (int)(point.Y / TileSize);
            int grid_column = (int)(point.X / TileSize);

            return new Vector(grid_column, grid_row);
        }

        /// <summary>
        /// This metod check if corner is busy
        /// </summary>
        /// <param name="corner"></param>
        /// <returns></returns>
        public bool IsCornerBusy(Point corner)
        {
            foreach (PrimitiveObject pObj in Grid[(int)corner.Y, (int)corner.X].ObjectsInCorner)
            {
                if (pObj.IsSolid() && pObj.GetType().Name != "Catarpillar" && pObj.GetType().Name != "Capsule")
                    return true;
              
            }

            return false;
        }

        /// <summary>
        /// Gets first object by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public GameObject GetObject(string key)
        {
            foreach (GameObject gObj in gameObjectsStore)
            {
                if (gObj.Name == key) return gObj;
            }

            return null;
        }

        /// <summary>
        /// Return game objects
        /// </summary>
        /// <returns></returns>
        public List<GameObject> GetObjects()
        {
            return gameObjectsStore;
        }

        /// <summary>
        /// Set game objects to map
        /// </summary>
        /// <param name="gameObjects"></param>
        public void SetObjects(List<GameObject> gameObjects)
        {
            Clear();

            foreach (GameObject gObj in gameObjects)
            {
                AddObject(gObj);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        public void Update(Int32 dt)
        {
            foreach (GameObject gObj in gameObjectsStore.ToArray())
            {
                gObj.UpdateObject(dt);                                
            }
        }
    }




    /// <summary>
    /// Grid corner class
    /// </summary>
    public class GridCorner
    {
        /// <summary>
        /// Grid corner class constructor
        /// </summary>
        public GridCorner()
        {
            ObjectsInCorner = new List<PrimitiveObject>();
        }

        /// <summary>
        /// List of objects in corner property
        /// </summary>
        public List<PrimitiveObject> ObjectsInCorner { get; set; }
    }

    /// <summary>
    /// Grid Area class
    /// </summary>
    public class GridArea
    {
        public int leftupRow { get; set; }
        public int leftupColumn { get; set; }
        public int rightdownRow { get; set; }
        public int rightdownColumn { get; set; }


        public GridArea(int lu_row, int lu_col, int rd_row, int rd_column)
        {
            this.leftupRow = lu_row;
            this.leftupColumn = lu_col;
            this.rightdownRow = rd_row;
            this.rightdownColumn = rd_column;
        }
    }
}