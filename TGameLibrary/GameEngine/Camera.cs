using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;



namespace TanksGameEngine.GameEngine
{   
    /// <summary>
    /// Camera class. Camera display only those objects in map that located in view border
    /// of camera
    /// </summary>
    public class Camera
    {
        /// <summary>
        /// 
        /// </summary>
        private List<LifeIndicator> LifeIndicators;

        /// <summary>
        /// Allowable size property. Camera doesn't move, when target object
        /// located in allowable borders.
        /// </summary>
        public Vector AllowableSize { get; set; }    

        /// <summary>
        /// View half-size of camera window
        /// </summary>
        public Vector ViewSize { get; set; }

        /// <summary>
        /// Camera focus property
        /// </summary>
        public Vector Focus { get; set; }

        /// <summary>
        /// View changed event
        /// </summary>
        public event EventHandler<ViewChangedArgs> ViewChanged;

        /// <summary>
        /// Method that update X focus coordinate 
        /// </summary>
        /// <param name="loc_X"></param>
        /// <returns></returns>
        private double UpdateFocusX(double loc_X)
        {
            Double hor = 0;

            if (loc_X < this.Focus.X - this.AllowableSize.X)
            {
                hor = loc_X + this.AllowableSize.X;
            }
            else
            {
                if (loc_X > this.Focus.X + this.AllowableSize.X)
                {
                    hor = loc_X - this.AllowableSize.X;
                }
                else
                {
                    hor = this.Focus.X;
                }
            }

            return hor;
        }

        /// <summary>
        /// Method that update Y focus coordinate 
        /// </summary>
        /// <param name="loc_Y"></param>
        /// <returns></returns>
        private double UpdateFocusY(double loc_Y)
        {
            Double vert = 0;

            if (loc_Y < this.Focus.Y - this.AllowableSize.Y)
            {
                vert = loc_Y + this.AllowableSize.Y;
            }
            else
            {
                if (loc_Y > this.Focus.Y + this.AllowableSize.Y)
                {
                    vert = loc_Y - this.AllowableSize.Y;
                }
                else
                {
                    vert = this.Focus.Y;
                }
            }

            return vert;
        }

        /// <summary>
        /// This metod returns game objects to draw from map grid. When target object is in
        /// allowable sector, camera doesn't move. Otherway, it moves.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UpdateFocus(GameObject gObject)
        {
            this.Focus = new Vector(UpdateFocusX(gObject.AbsoluteCenter.X), UpdateFocusY(gObject.AbsoluteCenter.Y));
        }

        /// <summary>
        /// Camer constructor
        /// </summary>
        public Camera()
        {
            AllowableSize = new Vector(0, 0);
            ViewSize = new Vector(0, 0);
            Focus = new Vector(0, 0);                 

            LifeIndicators = new List<LifeIndicator>();
        }

        /// <summary>
        /// Camera constructor
        /// </summary>
        /// <param name="vSize"></param>
        /// <param name="allowSize"></param>
        public Camera(Vector vSize, Vector allowSize)
        {
            ViewSize = vSize / 2;
            AllowableSize = allowSize / 2;
            Focus = new Vector(0, 0);

            LifeIndicators = new List<LifeIndicator>();
        }

        /// <summary>
        /// Camera constructor that takes target object(to follow it), half-size of camera view
        /// and half-size of allowable sector.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="vSize"></param>
        /// <param name="allowSize"></param>
        public Camera(Vector focus, Vector vSize, Vector allowSize)
        {
            Focus = focus;
            ViewSize = vSize / 2;
            AllowableSize = allowSize / 2;
            LifeIndicators = new List<LifeIndicator>();

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="gObject"></param>
        public void IndicateLifeChange(GameObject gObject)
        {
            foreach (LifeIndicator indicator in LifeIndicators)
            {
                if (indicator.TargetObject.Equals(gObject))
                {
                    indicator.Reset();

                    return;
                }
            }

            LifeIndicators.Add(new LifeIndicator(gObject, 1000));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gObject"></param>
        public void TryRemoveIndicator (GameObject gObject)
        {
            LifeIndicators.RemoveAll(x => x.TargetObject == gObject);
        }

   
        /// <summary>
        /// Update camera method, call event to GameField
        /// </summary>
        /// <param name="layers"></param>
        public void UpdateCamera(List<PrimitiveObject> primitives, Int32 timeMilliseconds)
        {      
            foreach (LifeIndicator indicator in LifeIndicators)
            {
                indicator.Update(timeMilliseconds);
            }

            LifeIndicators.RemoveAll(x => x.RestTime <= 0);

            if (ViewChanged != null)
            {
                ViewChanged(this, new ViewChangedArgs(GetLayersFromList(primitives), LifeIndicators));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="primitives"></param>
        /// <returns></returns>
        private Dictionary<Int32, Layer> GetLayersFromList(List<PrimitiveObject> primitives)
        {
            Dictionary<Int32, Layer> layers = new Dictionary<Int32, Layer>();

            foreach (PrimitiveObject obj in primitives)
            {
                if (!layers.Keys.Contains(obj.AbsoluteZIndex))
                    layers.Add(obj.AbsoluteZIndex, new Layer());

                layers[obj.AbsoluteZIndex].Items.Add(obj);
            }

            return layers;
        }
    }



    /// <summary>
    /// Class with arguments for event of camera viev changing
    /// </summary>
    public class ViewChangedArgs : EventArgs
    {
        /// <summary>
        /// VievChangedArgs class constructor. Takes dictionary with pairs: zIndex and
        /// game object
        /// </summary>
        /// <param name="_view"></param>
        public ViewChangedArgs(Dictionary<Int32, Layer> layers, List<LifeIndicator> indicators)
        {
            this.Layers = layers;
            this.LifeIndicators = indicators;

        }

        public Dictionary<Int32, Layer> Layers { get; set; }
        public List<LifeIndicator> LifeIndicators { get; set; }

        
        
    }
    
    /// <summary>
    /// Layer class
    /// </summary>
    public class Layer
    {
        public Layer()
        {
            Items = new List<PrimitiveObject>();

        }

        public List<PrimitiveObject> Items { get; set; }
    }
}