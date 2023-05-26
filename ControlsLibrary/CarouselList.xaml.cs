using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace ControlsLibrary
{
    /// <summary>
    /// Carousel list control, provides cicle element scrolling
    /// </summary>
    public partial class CarouselList : UserControl
    {
        public static DependencyProperty ItemsSourcesProperty;
        public static DependencyProperty CurrentItemProperty;
        public static DependencyProperty ScaleKeficientProperty;
        public static DependencyProperty AnimationTimeProperty;
        public static DependencyProperty CurrentIndexProperty;
        public static DependencyProperty MarginBetweenItemsProperty;

        static CarouselList()
        {
            ItemsSourcesProperty = DependencyProperty.Register("ItemsSources", typeof(CycleCollection<Uri>), typeof(CarouselList),
                new FrameworkPropertyMetadata(new CycleCollection<Uri>(), new PropertyChangedCallback(OnItemsChanged)));

            ScaleKeficientProperty = DependencyProperty.Register("ScaleKeficient", typeof(Double), typeof(CarouselList),
                new FrameworkPropertyMetadata(1.0, new PropertyChangedCallback(OnScaleKeficientChanged)));

            AnimationTimeProperty = DependencyProperty.Register("AnimationTime", typeof(Int32), typeof(CarouselList),
                new FrameworkPropertyMetadata(1000, new PropertyChangedCallback(OnAnimationTimeChanged)));

            CurrentIndexProperty = DependencyProperty.Register("CurrentIndex", typeof(Int32), typeof(CarouselList),
                new FrameworkPropertyMetadata(0, new PropertyChangedCallback(OnCurrentIndexChanged)));

            CurrentItemProperty = DependencyProperty.Register("CurrentItem", typeof(Uri), typeof(CarouselList),
               new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnCurrentItemChanged)));
        
            MarginBetweenItemsProperty = DependencyProperty.Register("MarginBetweenItems", typeof(Double), typeof(CarouselList),
                new FrameworkPropertyMetadata(0.0, new PropertyChangedCallback(OnMarginBetweenItemsChanged)));
        }

        /// <summary>
        /// On items sources changed handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnItemsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            CarouselList carouselList = (CarouselList)sender;

            if (carouselList.bufferSize != 0)
            {
                carouselList.FillCarousel();

                carouselList.ScaleCentralElement();

                carouselList.CurrentIndex = 0;
            }
        }

        /// <summary>
        /// On scale keficient changed handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnScaleKeficientChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
           
        }

        /// <summary>
        /// On current index changed handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnCurrentIndexChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            CarouselList carouselList = (CarouselList)sender;

            Uri uri = carouselList.ItemsSources[(Int32)e.NewValue];

            carouselList.lblCurrentName.Content = System.IO.Path.GetFileName(uri.OriginalString).Split('.')[0];

            carouselList.CurrentItem = carouselList.ItemsSources[carouselList.CurrentIndex];
        }

        /// <summary>
        /// On current item changed handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnCurrentItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            CarouselList carouselList = (CarouselList)sender;

            carouselList.CurrentIndex = carouselList.ItemsSources.IndexOf((Uri)e.NewValue);       
        }


        /// <summary>
        /// On margin between items changed handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnMarginBetweenItemsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
         
        }

        /// <summary>
        /// On animation time changed handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnAnimationTimeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            
        }

        
        /// <summary>
        /// Carousel panel center point
        /// </summary>
        private Point carouselCenter;

        /// <summary>
        /// Distance between centers of items
        /// </summary>
        private double distanceCenterX = 0;

        /// <summary>
        /// Is animation finished flag
        /// </summary>
        private bool IsAnimationCompleted = true;

        /// <summary>
        /// Control size(width and height)
        /// </summary>
        private double controlSize = 0;

        /// <summary>
        /// Buffer size(elements in canvas)
        /// </summary>
        private int bufferSize = 0;


        /// <summary>
        /// Carousel list constructo
        /// </summary>
        public CarouselList()
        {
            InitializeComponent();

            CarouselCanvas.ClipToBounds = true;
        }


        /// <summary>
        /// Items sources property
        /// </summary>
        public CycleCollection<Uri> ItemsSources
        {
            get { return (CycleCollection<Uri>)GetValue(ItemsSourcesProperty); }
            set { SetValue(ItemsSourcesProperty, value); }
        }

        /// <summary>
        /// Scale keficient property
        /// </summary>
        public Double ScaleKeficient
        {
            get { return (Double)GetValue(ScaleKeficientProperty); }
            set { SetValue(ScaleKeficientProperty, value); }
        }

        /// <summary>
        /// Animation time property
        /// </summary>
        public Int32  AnimationTime
        {
            get { return (Int32)GetValue(AnimationTimeProperty); }
            set { SetValue(AnimationTimeProperty, value); }
        }

        /// <summary>
        /// Current index property
        /// </summary>
        public Int32  CurrentIndex
        {
            get { return (Int32)GetValue(CurrentIndexProperty); }
            set { SetValue(CurrentIndexProperty, value); }
        }

        /// <summary>
        /// Current index property
        /// </summary>
        public Uri CurrentItem
        {
            get { return (Uri)GetValue(CurrentItemProperty); }
            set { SetValue(CurrentItemProperty, value); }
        }

        /// <summary>
        /// Margin between items property
        /// </summary>
        public Double MarginBetweenItems
        {
            get { return (Double)GetValue(MarginBetweenItemsProperty); }
            set { SetValue(MarginBetweenItemsProperty, value); }
        }
   


        /// <summary>
        /// Create new image by uri
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private Image CreateControlByUri(Uri source)
        {
            Image image = new Image();

            image.Width = controlSize;
            image.Height = controlSize;

            image.RenderTransformOrigin = new Point(0.5, 0.5);
            image.RenderTransform = new ScaleTransform(1.0, 1.0);

            var bitmap = new BitmapImage();

            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.UriSource = source;
            bitmap.EndInit();

            image.Source = bitmap;

            return image;
        }

        /// <summary>
        /// Carousel loaded handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Carousel_Loaded(object sender, RoutedEventArgs e)
        {
            carouselCenter = new Point(CarouselCanvas.ActualWidth / 2, CarouselCanvas.ActualHeight / 2);

            controlSize = CarouselCanvas.ActualHeight / ScaleKeficient;

            distanceCenterX = controlSize + MarginBetweenItems;

            bufferSize = (int)(Math.Ceiling(CarouselCanvas.ActualWidth / (controlSize + MarginBetweenItems)));
           
            if (bufferSize % 2 == 0)
                bufferSize++;
                             
            FillCarousel();

            ScaleCentralElement();

            CurrentItem = ItemsSources[CurrentIndex];

            lblCurrentName.Content = System.IO.Path.GetFileName(ItemsSources[CurrentIndex].OriginalString).Split('.')[0];

            
        }

        /// <summary>
        /// Scale central element when page loaded
        /// </summary>
        private void ScaleCentralElement()
        {
            Image centralElement = (Image)CarouselCanvas.Children[(int)(bufferSize / 2)];

            centralElement.RenderTransform = new ScaleTransform(ScaleKeficient, ScaleKeficient);        
        }

        /// <summary>
        /// This method fill carousel buffer(canvas) by buffer size
        /// (number of elements in canvas), current index of central element,
        /// center point of carousel and X distance between elements.
        /// </summary>
        private void FillCarousel()
        {     
            CarouselCanvas.Children.Clear();

            int bufSideRange = bufferSize / 2;

            for (int i = -bufSideRange; i <= bufSideRange; i++)
            {
                 Image Image = CreateControlByUri(ItemsSources[CurrentIndex + i]);

                 Canvas.SetTop(Image, carouselCenter.Y - Image.Height / 2);
                 Canvas.SetLeft(Image, carouselCenter.X - Image.Width / 2 + i * distanceCenterX);

                 CarouselCanvas.Children.Add(Image);
            }
        }

        /// <summary>
        /// Update carousel buffer(elements). It remove last element and add first by
        /// move direction.
        /// </summary>
        /// <param name="moveCommand"></param>
        private void UpdateCarousel(MoveCommand moveCommand)
        {
            DrawAnimation(moveCommand);

            int sign = moveCommand == MoveCommand.Left ? -1 : 1;

            CurrentIndex -= sign;

            Image Image = CreateControlByUri(ItemsSources[CurrentIndex - sign * (bufferSize / 2)]);

            if (moveCommand == MoveCommand.Left)
            {                  
                CarouselCanvas.Children.RemoveAt(0);
                CarouselCanvas.Children.Insert(bufferSize - 1, Image);
            }
            else
            {
                CarouselCanvas.Children.RemoveAt(bufferSize - 1);
                CarouselCanvas.Children.Insert(0, Image);       
            }

            Canvas.SetLeft(Image, carouselCenter.X - Image.Width / 2 - sign * ((int)(bufferSize / 2)) * distanceCenterX);
            Canvas.SetTop(Image, carouselCenter.Y - Image.Height / 2);
        }

        /// <summary>
        /// Animate list moving
        /// </summary>
        /// <param name="moveCommand"></param>
        private void DrawAnimation(MoveCommand moveCommand)
        {
            int sign = moveCommand == MoveCommand.Left ? -1 : 1;

            int bufSideRange = bufferSize / 2;

            for (int i = -bufSideRange; i <= bufSideRange; i++)
            {
                Image Image = (Image)CarouselCanvas.Children[i + bufSideRange];

                Storyboard storyboard = new Storyboard();
              
                DoubleAnimation translAnimation = TranslateXAnimation(Image, distanceCenterX * sign, AnimationTime);

                storyboard.Children.Add(translAnimation);

                DoubleAnimation scXAnimation;
                DoubleAnimation scYAnimation;

                if(i == 0)
                {
                    scXAnimation = ScaleXAnimation(Image, ScaleKeficient, 1, AnimationTime);
                    scYAnimation = ScaleYAnimation(Image, ScaleKeficient, 1, AnimationTime);
                    storyboard.Children.Add(scXAnimation);
                    storyboard.Children.Add(scYAnimation);
                }
                else
                {
                    if (sign * i == - 1)
                    {
                        scXAnimation = ScaleXAnimation(Image, 1, ScaleKeficient, AnimationTime);
                        scYAnimation = ScaleYAnimation(Image, 1, ScaleKeficient, AnimationTime);
                        storyboard.Children.Add(scXAnimation);
                        storyboard.Children.Add(scYAnimation);
                    }     
                }
                storyboard.Begin();
            }   
        }

        /// <summary>
        /// Translate animation
        /// </summary>
        /// <param name="Image"></param>
        /// <param name="deltaXValue"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        private DoubleAnimation TranslateXAnimation(Image Image, double deltaXValue, int time)
        {
            DoubleAnimation translateXAnimation = new DoubleAnimation();
            translateXAnimation.Duration = TimeSpan.FromMilliseconds(time);

            translateXAnimation.By = deltaXValue;
           
            translateXAnimation.AccelerationRatio = 0.1;

            Storyboard.SetTargetProperty(translateXAnimation, new PropertyPath("(Canvas.Left)"));
            Storyboard.SetTarget(translateXAnimation, Image);

            translateXAnimation.Completed += Animation_Completed;

            IsAnimationCompleted = false;

            return translateXAnimation;
        }

        /// <summary>
        /// Scale X animation
        /// </summary>
        /// <param name="Image"></param>
        /// <param name="fromValue"></param>
        /// <param name="toValue"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        private DoubleAnimation ScaleXAnimation(Image Image, double fromValue, double toValue, int time)
        {
            DoubleAnimation growWidthAnimation = new DoubleAnimation();
            growWidthAnimation.Duration = TimeSpan.FromMilliseconds(time);

            growWidthAnimation.From = fromValue;
            growWidthAnimation.To = toValue;

            Storyboard.SetTargetProperty(growWidthAnimation, new PropertyPath("RenderTransform.ScaleX"));
            Storyboard.SetTarget(growWidthAnimation, Image);

            growWidthAnimation.Completed += Animation_Completed;

            IsAnimationCompleted = false;

            return growWidthAnimation;
        }

        /// <summary>
        /// Scale Y animation
        /// </summary>
        /// <param name="Image"></param>
        /// <param name="fromValue"></param>
        /// <param name="toValue"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        private DoubleAnimation ScaleYAnimation(Image Image, double fromValue, double toValue, int time)
        {
            DoubleAnimation growHeightAnimation = new DoubleAnimation();
            growHeightAnimation.Duration = TimeSpan.FromMilliseconds(time);

            growHeightAnimation.From = fromValue;
            growHeightAnimation.To = toValue;


            Storyboard.SetTargetProperty(growHeightAnimation, new PropertyPath("RenderTransform.ScaleY"));
            Storyboard.SetTarget(growHeightAnimation, Image);

            growHeightAnimation.Completed += Animation_Completed;

            IsAnimationCompleted = false;

            return growHeightAnimation;
        }

        /// <summary>
        /// Animation comledet event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Animation_Completed(object sender, EventArgs e)
        {
            IsAnimationCompleted = true;
        }

        /// <summary>
        /// Get prevoios element handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if(IsAnimationCompleted)
                UpdateCarousel(MoveCommand.Right); 
        }

        /// <summary>
        /// Get next element handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (IsAnimationCompleted)
                UpdateCarousel(MoveCommand.Left);
        }    
    }

    /// <summary>
    /// Move command(move elements left or right)
    /// </summary>
    public enum MoveCommand { Left, Right}
}
