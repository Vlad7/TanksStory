using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TanksGameEngine.GameEngine.Components;

namespace TanksGameEngine.GameEngine
{
    /// <summary>
    /// Leaf - лист
    /// </summary>
    /// <remarks>
    /// <li>
    /// <lu>представляет листовой узел композиции и не имеет потомков;</lu>
    /// <lu>определяет поведение примитивных объектов в композиции;</lu>
    /// </li>
    /// </remarks>
    public class PrimitiveObject : GameObject
    {
        public static double _initialObjSize = 64;

        /// <summary>
        /// Half-size of primitive object. 
        /// </summary>
        public Vector Size   { get; set; }

        /// <summary>
        /// It is responsible for visual state of primitive object.
        /// </summary>
        public Viewer Viewer { get; set; }

        /// <summary>
        /// Stores collision shape of primitive object.
        /// </summary>
        public CollisionShape CollisionShape { get; set; }
      

        /// <summary>
        /// Primitive object constructor
        /// </summary>
        public PrimitiveObject() : base(string.Empty)
        {
            Size = new Vector(_initialObjSize, _initialObjSize);

            Viewer  = null;

            CollisionShape = CollisionShape.None;
        }

        /// <summary>
        /// Leaf object constructor
        /// </summary>
        /// <param name="name"></param>
        public PrimitiveObject(string name) : base(name)
        {
            Size = new Vector(_initialObjSize, _initialObjSize);

            Viewer = null;

            CollisionShape = CollisionShape.None;
        }

        /// <summary>
        /// Leaf object constructor
        /// </summary>
        /// <param name="name"></param>
        public PrimitiveObject(string name, Locator locator) : base(name, locator)
        {
            Size = new Vector(_initialObjSize, _initialObjSize);

            Viewer = null;

            CollisionShape = CollisionShape.None;
        }

         /// <summary>
        /// Leaf object constructor
        /// </summary>
        /// <param name="name"></param>
        public PrimitiveObject(string name, Locator locator, Engine engine) : base(name, locator, engine)
        {
            Size = new Vector(_initialObjSize, _initialObjSize);

            Viewer = null;

            CollisionShape = CollisionShape.None;
        }

        public override List<PrimitiveObject> GetPrimitives()
        {            
            List<PrimitiveObject> result = new List<PrimitiveObject>();

            result.Add(this);

            return result; 
        }

        public override bool Contains(GameObject obj)
        {
            return this.Equals(obj);
        }


        public override void UpdateObject(Int32 dt)
        {                
            UpdateLocation(dt);

            Update(dt);            
        }

        public override void Update(Int32 dt)
        {
            if (Viewer != null)
                Viewer.Update(dt);          
        }

        /// <summary>
        /// Method show if object is solid
        /// </summary>
        /// <returns></returns>
        public override bool IsSolid()
        {
            if (CollisionShape != CollisionShape.None)
                return true;
            else
                return false;
        }

        public override long GetWeight()
        {
            return Int32.MaxValue;
        }
    
      
    }

    // <summary>
    /// Enum, that describe four collider states: rectangle, circle, point, none.
    /// </summary>
    public enum CollisionShape
    {
        Rectangle, Circle, Point, None
    }
}
