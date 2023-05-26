using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TanksGameEngine.GameEngine;
using TanksGameEngine.GameEngine.Components;

namespace TanksGameEngine.GameEngine
{
    /// <summary>
    /// Composite - составной объект
    /// </summary>
    /// <li>
    /// <lu>определяет поведеление компонентов, у которых есть потомки;</lu>
    /// <lu>хранит компоненты-потомоки;</lu>
    /// <lu>реализует относящиеся к управлению потомками операции и интерфейсе
    /// класса <see cref="GameObject"/></lu>
    /// </li>
    public class CompositeObject : GameObject
    {
        /// <summary>
        /// Composite object children property
        /// </summary>
        private List<GameObject> components { get; set; }

        public Action<GameObject> ComponentsUpdated;

        /// <summary>
        /// 
        /// </summary>
        public CompositeObject() : base()
        {
            components = new List<GameObject>();
        }

        /// <summary>
        /// Composite object constructor
        /// </summary>
        /// <param name="name"></param>
        public CompositeObject(string name) : base(name)
        {
            components = new List<GameObject>();
        }

        /// <summary>
        /// Composite object constructor
        /// </summary>
        /// <param name="name"></param>
        public CompositeObject(string name, Locator locator) : base(name, locator)
        {
            components = new List<GameObject>();
        }

        /// <summary>
        /// Composite object constructor
        /// </summary>
        /// <param name="name"></param>
        public CompositeObject(string name, Locator locator, Engine engine) : base(name, locator, engine)
        {
            components = new List<GameObject>();
        }

        /// <summary>
        /// Set component by type
        /// </summary>
        /// <param name="nObj"></param>
        public void SetComponentByType(GameObject nObj)
        {
            nObj.Parent = this;

            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].GetType() == nObj.GetType())
                {
                    components[i] = nObj;

                    return;
                }
            }

            this.AddComponent(nObj);

            if (ComponentsUpdated != null)
                ComponentsUpdated(this);
        }       

        /// <summary>
        /// Get first component by type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public GameObject GetComponentByType(String type)
        {
            foreach (GameObject obj in components)
            {
                if (obj.GetType().Name == type) return obj;
            }

            return null;
        }

        /// <summary>
        /// Return first component by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public GameObject GetComponentByName(String name)
        {
            foreach (GameObject obj in components)
            {
                if (obj.Name == name) return obj;
            }

            return null;
        }

        /// <summary>
        /// Return all components
        /// </summary>
        /// <returns></returns>
        public List<GameObject> GetComponents()
        {
            return components;
        }

        /// <summary>
        /// Add new component
        /// </summary>
        /// <param name="gObj"></param>
        public void AddComponent(GameObject gObj)
        {
            components.Add(gObj);

            if (ComponentsUpdated != null)
                ComponentsUpdated(this);
        }
        
        /// <summary>
        /// Remove one component
        /// </summary>
        /// <param name="gObj"></param>
        public void RemoveComponent(GameObject gObj)
        {
            components.Remove(gObj);

            if (ComponentsUpdated != null)
                ComponentsUpdated(this);
        }

        /// <summary>
        /// Clear all components
        /// </summary>
        public void ClearComponents()
        {
            components.Clear();

            if (ComponentsUpdated != null)
                ComponentsUpdated(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override List<PrimitiveObject> GetPrimitives()
        {
            List<PrimitiveObject> pObjects = new List<PrimitiveObject>();

            foreach(GameObject component in components)           
                pObjects.AddRange(component.GetPrimitives());

            return pObjects;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Contains(GameObject obj)
        {
            if (this.Equals(obj)) return true;

            foreach (GameObject gObj in components)
            {
                if (gObj.Contains(obj)) return true;
            }

            return false;         
        }

        public override bool IsSolid()
        {
            foreach(GameObject gObj in components)
            {
                if (gObj.IsSolid())
                    return true;
            }

            return false;           
        }

        public override long GetWeight()
        {
            long weight = 0;

            foreach (GameObject gObj in components)
                weight += gObj.GetWeight();

            return weight;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        public override void UpdateObject(Int32 dt)
        {
            UpdateLocation(dt);
            Update(dt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        public override void Update(Int32 dt)
        {
            foreach (GameObject obj in components)
            {
                obj.Update(dt); 
            }
                        
        }   
    }
}
