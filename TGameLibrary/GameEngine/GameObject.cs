using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Media;
using System.Windows;
using System.Windows.Automation.Peers;
using TanksGameEngine.GameEngine.Components;


namespace TanksGameEngine.GameEngine
{
    /// <summary>
    /// MapComponent - компонент
    /// </summary>
    /// <li>
    /// <lu>объявляет интерфейс для компонуемых объектов;</lu>
    /// <lu>предоставляет подходящую реализацию операций по умолчанию,
    /// общую для всех классов;</lu>
    /// <lu>объявляет интерфейс для доступа к потомкам и управлению ими;</lu>
    /// <lu>определяет интерфейс доступа к родителю компонента в рекурсивной структуре
    /// и при необходимости реализует его. Описанная возможность необязательна;</lu>
    /// </li>
    public abstract class GameObject
    {
        private Double life;

        /// <summary>
        /// 
        /// </summary>
        protected Locator locator;

        /// <summary>
        /// Private engine field
        /// </summary>
        public Engine Engine { get; set; }

        /// <summary>
        /// Название, тег обьекта
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        ///  Родительский обьект, контейнер
        /// </summary>
        public GameObject Parent { get; set; }

        
        public Boolean HasController { get; set; }

        /// <summary>
         /// 
         /// </summary>
        public Double Life
        {
             get
             {
                 return life;
             }
             set
             {
                 if (life != value)
                 {
                     life = value > 100 ? 100 : value;

                     if (life > 0)
                     {
                         if (LifeChanged != null)
                         {
                             LifeChanged(this);
                         }                        
                     }
                     else
                     {
                         Crash();
                     }               
                 }
             }
        }
       
        /// <summary>
        /// This property returns local position of base object(position at parent element)
        /// </summary>                        
        public Vector LocalCenter
        {
            get
            {
                return locator.Center;
            }
            set
            {
                if (locator.Center != value)
                {
                    locator.Center = value;

                    if (LocationChanged != null)   
                    {
                        LocationChanged(this);
                    }
                }
            }
        }      

        /// <summary>
        /// This property returns local rotation of base object(rotation at parent element)
        /// </summary>
        public Double LocalAngle
        {
            get
            {
                return locator.Angle;
            }
            set
            {
                if (locator.Angle != value)
                {
                    if (value < 0)
                    {
                        value += 360;
                    }

                    if (value >= 360)
                    {
                        value -= 360;
                    }

                    locator.Angle = value;

                     if (LocationChanged != null)   
                    {
                        LocationChanged(this);
                    }
                }
            }
        }

        /// <summary>
        /// This property retturns local zIndex of base object(zIndex at parent element)
        /// </summary>
        public Int32  LocalZIndex
        {
            get
            {
                return locator.ZIndex;
            }
            set
            {
                if (locator.ZIndex != value)
                {
                    locator.ZIndex = value;

                    if (LocationChanged != null)   
                    {
                        LocationChanged(this);
                    }
                }
            }

        }


        /// <summary>
        /// This property retturns absolute position of base object(position at game map)
        /// </summary>
        public Vector AbsoluteCenter
        {
            get
            {
                if (Parent == null)
                {
                    return locator.Center;
                }
            
                else
                {    
                    Double pRadians = Parent.AbsoluteAngle * Math.PI / 180;

                    Double new_X = locator.Center.X * Math.Cos(pRadians) - locator.Center.Y * Math.Sin(pRadians);
                    Double new_Y = locator.Center.Y * Math.Cos(pRadians) + locator.Center.X * Math.Sin(pRadians); 
                
                    return (new Vector(new_X, new_Y) + Parent.AbsoluteCenter);
                }
            }
        }

        /// <summary>
        /// This property retturns absolute rotation of base object(rotation at game map)
        /// </summary>
        public Double AbsoluteAngle 
        {
            get
            {
                if (Parent == null)
                {
                    return locator.Angle;
                }
                else
                {
                    return (locator.Angle + Parent.AbsoluteAngle) % 360;
                }
            }
        }

        /// <summary>
        /// This property retturns absolute zIndex of base object(zIndex at game map)
        /// </summary>
        public Int32  AbsoluteZIndex
        {
            get
            {
                if (Parent == null)
                {
                    return locator.ZIndex;
                }

                else
                {
                    return locator.ZIndex + Parent.AbsoluteZIndex;
                }   
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public event Action<GameObject> LocationChanged;

        /// <summary>
        /// Direction changed event
        /// </summary>
        public event Action<GameObject> LifeChanged;

        /// <summary>
        /// Object crashed event
        /// </summary>
        public event Action<GameObject> Crashed;

        /// <summary>
        /// 
        /// </summary>
        public GameObject()
        {
            this.Name = string.Empty;

            this.Parent = null;

            this.locator = new Locator();

            this.Engine = null;

            this.life = 100;
        }

        /// <summary>
        /// Конструктор, принимает имя обьекта
        /// </summary>
        /// <param name="name"></param>
        public GameObject(string name)
        {
            this.Name = name;

            this.Parent = null;

            this.locator = new Locator();

            this.Engine = null;

            this.life = 100;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="locator"></param>
        public GameObject(string name, Locator locator)
        {
            this.Name = name;

            this.Parent = null;

            this.locator = locator;

            this.Engine = null;

            this.life = 100;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="locator"></param>
        public GameObject(string name, Locator locator, Engine engine)
        {
            this.Name = name;

            this.Parent = null;

            this.locator = locator;

            this.Engine = engine;

            this.life = 100;
        }
         
        /// <summary>
        /// This method return root object of this object.
        /// </summary>
        /// <returns></returns>
        public GameObject GetRootObject()
        {
            GameObject root = this;

            while(root.Parent != null) 
                root = root.Parent;

            return root;      
        }

        /// <summary>
        /// This method return primitives of this object
        /// </summary>
        /// <returns></returns>
        public abstract List<PrimitiveObject> GetPrimitives();

        /// <summary>
        /// This method check if this object contains someone primitive
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public abstract Boolean Contains(GameObject obj);

        /// <summary>
        /// UpdateObject method
        /// </summary>
        /// <param name="dt"></param>
        public abstract void UpdateObject(Int32 dt);

        /// <summary>
        /// Update method
        /// </summary>
        /// <param name="dt"></param>
        public abstract void Update(Int32 dt);     

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        protected void UpdateLocation(Int32 dt)
        {
            if (Engine != null)
            {
                Engine.Update(dt);

                this.locator.Center += Engine.translateSpeed;
                this.locator.Angle  += Engine.rotationSpeed;

                if (this.locator.Angle < 0)
                {
                    this.locator.Angle += 360;
                }

                if (this.locator.Angle >= 360)
                {
                    this.locator.Angle -= 360;
                }

                 if (LocationChanged != null)   
                 {
                      LocationChanged(this);
                 }
            }
        }


        public abstract bool IsSolid();
        public abstract Int64 GetWeight();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="direction"></param>
        /// <param name="overlap"></param>
        public virtual void Collide(GameObject gameObject, Vector direction, double overlap)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Crash() 
        {
            if(Crashed != null)
            {
                Crashed(this);
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public virtual void Damage (Double value)
        {
            if (Parent != null)
            {
                Parent.Damage(value);
            }
            else
            {
                Life -= value; 
            }
        }       
    }



    public class LifeChangedArgs : EventArgs
    {
        public Double LifePercent {get; private set; }

        public LifeChangedArgs(Double percent)
        {
            LifePercent = percent;
        }
    } 
}

