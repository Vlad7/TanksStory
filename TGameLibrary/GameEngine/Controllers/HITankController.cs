namespace TanksGameEngine.GameEngine.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Threading;
    using TanksGameEngine.TanksGame.Objects.Characters;

    /// <summary>
    /// Input controller class. It is listening input signals
    /// from keyboard and mouse and calling events for other
    /// objects.
    /// </summary>
    public class HITankController : TankController
    {
        /// <summary>
        /// Game field who sends mouse moved events to input controller
        /// </summary>
        private FrameworkElement Field;

        private Boolean fireState;

        private Vector mouseCanvasPosition;


        /// <summary>
        /// Input controller class constructor
        /// </summary>
        /// <param name="field"></param>
        public HITankController(FrameworkElement field, Tank target)
            : base(target)
        {
            field.Focus();
            if (field != null)
            {
                this.Field = field;
                this.Field.MouseMove += MousePositionChanged;
                this.Field.MouseDown += Field_MouseDown;
            }
        }


        /// <summary>
        /// Method that update input controller
        /// </summary>
        public override void Update(Int32 dt)
        {
            Vector direction = new Vector(0, 0);

            Action action = () =>
            {

                if ((Keyboard.GetKeyStates(Key.Up) & KeyStates.Down) > 0 || (Keyboard.GetKeyStates(Key.W) & KeyStates.Down) > 0)
                {
                    direction.Y += 1;
                }

                if ((Keyboard.GetKeyStates(Key.Down) & KeyStates.Down) > 0 || (Keyboard.GetKeyStates(Key.S) & KeyStates.Down) > 0)
                {
                    direction.Y -= 1;
                }

                if ((Keyboard.GetKeyStates(Key.Left) & KeyStates.Down) > 0 || (Keyboard.GetKeyStates(Key.A) & KeyStates.Down) > 0)
                {
                    direction.X -= 1;
                }

                if ((Keyboard.GetKeyStates(Key.Right) & KeyStates.Down) > 0 || (Keyboard.GetKeyStates(Key.D) & KeyStates.Down) > 0)
                {
                    direction.X += 1;
                }

                if (!(direction.X == 0 && direction.Y == 0))
                {
                    direction.Normalize();
                }

                if ((Keyboard.GetKeyStates(Key.Space) & KeyStates.Down) > 0)
                {
                    fireState = true;
                }

            };

            Application.Current.Dispatcher.Invoke(action);

            Camera camera = GameProcess.Current_Game.Camera;

            Vector mouseAbsLocation = camera.Focus - camera.ViewSize + mouseCanvasPosition;

            Vector towerDirection = mouseAbsLocation - Target.Gun.AbsoluteCenter;
            towerDirection.Normalize();     //Maybe check by zero


            UpdateTargetDirection(direction, dt);
          
            UpdateGunDirection(towerDirection ,dt);

            if (fireState == true)
                Target.Fire();

            fireState = false;
        }


        /// <summary>
        /// Method called when mouse position changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MousePositionChanged(object sender, MouseEventArgs e)
        {
            Point p_pos = e.GetPosition((FrameworkElement)sender);

            mouseCanvasPosition = new Vector(p_pos.X, p_pos.Y);
        }

        private void Field_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                fireState = true;
            }
        }

        ///// <summary>
        ///// Direction changed event
        ///// </summary>
        //public event EventHandler<DirectionChangedArgs> DirectionChanged;

        ///// <summary>
        ///// Fire button pressed event
        ///// </summary>
        //public event EventHandler<FireChangedArgs> FireChanged;

        ///// <summary>
        ///// Mouse moved event
        ///// </summary>
        //public event EventHandler<MouseChangedArgs> MouseChanged;
    }





    ///// <summary>
    ///// Class that store direction args
    ///// </summary>
    //public class DirectionChangedArgs : EventArgs
    //{
    //    /// <summary>
    //    /// Direction changed arguments class constructor
    //    /// </summary>
    //    /// <param name="direction"></param>
    //    public DirectionChangedArgs(Vector direction)
    //    {
    //        this.Direction = direction;
    //    }

    //    /// <summary>
    //    /// Target object direction property
    //    /// </summary>
    //    public Vector Direction
    //    {
    //        get;
    //        private set;
    //    }
    //}

    ///// <summary>
    ///// Fire changed args class
    ///// </summary>
    //public class FireChangedArgs : EventArgs
    //{
    //    /// <summary>
    //    /// Fire changed arguments class constructor
    //    /// </summary>
    //    /// <param name="isFire"></param>
    //    public FireChangedArgs(Boolean isFire)
    //    {
    //        this.IsFire = isFire;
    //    }

    //    /// <summary>
    //    /// Is fire butto pressed property
    //    /// </summary>
    //    public Boolean IsFire
    //    {
    //        get;
    //        private set;
    //    }
    //}

    ///// <summary>
    ///// Mouse changed args class
    ///// </summary>
    //public class MouseChangedArgs : EventArgs
    //{
    //    /// <summary>
    //    /// Mouse changed arguments constructor
    //    /// </summary>
    //    /// <param name="pos"></param>
    //    public MouseChangedArgs(Vector pos)
    //    {
    //        this.Position = pos;
    //    }

    //    /// <summary>
    //    /// Target object position property
    //    /// </summary>
    //    public Vector Position
    //    {
    //        get;
    //        private set;
    //    }
    //}



}
