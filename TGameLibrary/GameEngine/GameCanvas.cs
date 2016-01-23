using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;


namespace TanksGameEngine.GameEngine
{
    public class GameCanvas : FrameworkElement
    {
        private VisualCollection visuals;

        private SolidColorBrush IndicatorBackBrush;
        private SolidColorBrush IndicatorBordBrush;
        private SolidColorBrush IndicatorFillBrush;

        private Pen IndicatorBorderPen;

        protected override Visual GetVisualChild(int index)
        {
            return visuals[index];
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return visuals.Count;
            }
        }

        private Vector CalculatePositionOnCanvas(Vector center, Vector viewSize, Vector focus)
        {
            Vector pos = new Vector();

            pos.X = viewSize.X - (focus.X - center.X);
            pos.Y = viewSize.Y - (focus.Y - center.Y);

            return pos;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="primitive"></param>
        /// <returns></returns>
        private TransformGroup GetTransformation(PrimitiveObject primitive, Vector focus, Vector viewSize)
        {
            TransformGroup transformGroup = new TransformGroup();

            if (primitive.AbsoluteAngle != 0)
            {
                RotateTransform rot = new RotateTransform(primitive.AbsoluteAngle, primitive.Size.X, primitive.Size.Y);
                transformGroup.Children.Add(rot);
            }

            TranslateTransform translate = new TranslateTransform();

            Vector position = CalculatePositionOnCanvas(primitive.AbsoluteCenter, viewSize, focus);

            translate.X = position.X - primitive.Size.X;
            translate.Y = position.Y - primitive.Size.Y;

            transformGroup.Children.Add(translate);

            return transformGroup;
        }

        private Visual GetIndicatorVisual(GameObject TargetObject, Vector focus, Vector viewSize)
        {
            int backWidth = GameProcess.Current_Game.gameMap.TileSize;

            int backHeight = 10;
            int fillPadding = 1;

            DrawingVisual visual = new DrawingVisual();

            double percents = TargetObject.Life;

            int fillWidth = (int)((backWidth - fillPadding) * percents / 100);

            Vector position = CalculatePositionOnCanvas(TargetObject.AbsoluteCenter, viewSize, focus);

            Point rBackPoint = new Point(position.X - backWidth / 2, position.Y - backHeight / 2 - backWidth / 2);
            Point rFillPoint = new Point(rBackPoint.X + fillPadding, rBackPoint.Y + fillPadding);

            Rect rectBack = new Rect(rBackPoint, new Size(backWidth, backHeight));
            Rect rectFill = new Rect(rFillPoint, new Size(fillWidth, backHeight - fillPadding * 2));


            using (DrawingContext dc = visual.RenderOpen())
            {
                dc.DrawRectangle(IndicatorBackBrush, IndicatorBorderPen, rectBack);
                dc.DrawRectangle(IndicatorFillBrush, IndicatorBorderPen, rectFill);
            }

            return visual;
        } 


        public GameCanvas()
        {
            this.visuals = new VisualCollection(this);
            this.ClipToBounds = true;
            this.VisualCacheMode = new BitmapCache();

            IndicatorBackBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
            IndicatorBordBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
            IndicatorFillBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Yellow"));

            IndicatorBorderPen = new Pen(IndicatorBordBrush, 1);
        }


        public void AddVisual(Visual visual)
        {
            visuals.Add(visual);
        }

        public void RemoveVisual(Visual visual)
        {
            visuals.Remove(visual);
        }

        public void Clear()
        {
            visuals.Clear();
        }   

        public void Update(object sender, ViewChangedArgs e)
        {
            Camera camera = sender as Camera;

            Action action = () =>    
            {
                Clear();

                List<Int32> keys = e.Layers.Keys.ToList();

                keys.Sort();

                foreach (Int32 key in keys)
                {
                    foreach (PrimitiveObject obj in e.Layers[key].Items)
                    {
                        if (obj.Viewer != null && obj.Viewer.Enabled == true)
                        {
                            obj.Viewer.Sprite.DrawingVisual.Transform = GetTransformation(obj, camera.Focus, camera.ViewSize);

                            visuals.Add(obj.Viewer.Sprite.DrawingVisual);
                        }
                    }
                }


                foreach (LifeIndicator indicator in e.LifeIndicators)
                {
                    visuals.Add(GetIndicatorVisual(indicator.TargetObject, camera.Focus, camera.ViewSize));
                }
            };

            if (!Application.Current.Dispatcher.CheckAccess())
            {
                Application.Current.Dispatcher.Invoke(action);
            }
            else
            {
                action();
            }        
        }

          
    }
}
