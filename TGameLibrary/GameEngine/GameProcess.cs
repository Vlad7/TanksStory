using System.Diagnostics;
using System.Windows;
using TiledSharp;
using System;
using System.Collections.Generic;
using System.Windows.Threading;
using System.Threading;
using TanksGameEngine.GameEngine.Controllers;
using TanksGameEngine.TanksGame.Objects.Characters;
using TanksGameEngine.GameEngine.Components;
using System.Windows.Media.Imaging;
using TanksGameEngine.TanksGame.Objects.MapElements;
using System.Linq;


namespace TanksGameEngine.GameEngine
{
    /// <summary>
    /// Game process class. It is cernel of game. Lets do it singletone
    /// </summary>
    public class GameProcess
    {
        /// <summary>
        /// Current game reference
        /// </summary>
        public static GameProcess Current_Game { get; set; }

        /// <summary>
        /// This field show if game is working
        /// </summary>
        private bool isGameWorking = false;

        /// <summary>
        /// Frames per second
        /// </summary>
         private int FPS = 30;

        /// <summary>
        /// Timer tick interval
        /// </summary>
        public int updateInterval { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Controller> Controllers;

        /// <summary>
        /// 
        /// </summary>
        public CollisionManager Manager;
       
        /// <summary>
        /// Game camera 
        /// </summary>
        public Camera Camera { get; set; }

        /// <summary>
        /// Map property
        /// </summary>
        public Map gameMap { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<BitmapImage> frameImages;


        /// <summary>
        /// Target object
        /// </summary>
        public Tank Player;

        public List<Tank> Enemies;

        public TanksMapBuilder Builder { get; set; }
 

        /// <summary>
        /// Game process class constructor
        /// </summary>
        /// <param name="input_controller"></param>
        /// <param name="map"></param>
        /// <param name="obj"></param>
        public GameProcess(FrameworkElement gField, String mapName)
        {
            Current_Game = this;

            Camera = new Camera(new Vector(500, 500), new Vector(gField.ActualWidth, gField.ActualHeight), new Vector(200, 100));

            updateInterval = 1000 / FPS;

            Builder = new TanksMapBuilder();

            TmxBuildDirrector director = new TmxBuildDirrector(mapName, Builder);

            director.CreateMap();

            gameMap = Builder.GetMap();

            Manager = new CollisionManager(gameMap);

            director.CreateLandscape();

            director.CreateObjects();

            frameImages = TmxBuildDirrector.GetImagesFromCropedImage("Maps/Textures/explosion.png", 128, 128, 512, 512);

            Controllers = new List<Controller>();


         
            Locator loc = new Locator(new Vector(310, 310), 0, 2);       

            Engine  engine = new Engine(new Vector(0, 0), new Vector(0, 0), 0, 0);
                                                                              

            Player = Builder.BuildTank("Player", loc, engine);
            //Player.Body.Viewer = Boom;
     

            HITankController controller = new HITankController(gField, Player);

            Controllers.Add(controller);

            Enemies = new List<Tank>();

            List<GameObject> Objects = gameMap.GetObjects();

            for(int i = 0; i < Objects.Count; i++)
            {
                if(Objects[i].GetType().Name.ToLower() == "respaun")
                {
                    Engine eng = new Engine(new Vector(0, 0), new Vector(0, 0), 0, 0);

                    Tank enemy = Builder.BuildTank("Enemy_" + i.ToString(), new Locator(Objects[i].AbsoluteCenter, 0, 2), eng);

                    AITankController AIController = new AITankController(enemy);

                    Controllers.Add(AIController);
                }
            }

            Player.LocationChanged += GameProcess.Current_Game.Camera.UpdateFocus; 
        }


        /// <summary>
        /// Method that start game
        /// </summary>
        public void StartProcess()
        {
            isGameWorking = true;

            StartCycle();

        }

        /// <summary>
        /// Method that stop game
        /// </summary>
        public void StopProcess()
        {
            isGameWorking = false;
        }

        private void StartCycle()
        {
            Stopwatch watch = new Stopwatch();

            while (isGameWorking)
            {               
                watch.Start();

                foreach (Controller controller in Controllers)
                {
                    controller.Update(updateInterval);
                }

                gameMap.Update(updateInterval);

    
                Manager.CollisionHandlerRun();

                Camera.UpdateCamera(gameMap.GetPrimitivesByArea(Camera.Focus, Camera.ViewSize, 0), updateInterval);   
     
                int dt_rest = updateInterval - watch.Elapsed.Milliseconds;
                            
                watch.Reset();

                if (dt_rest > 0)
                    System.Threading.Thread.Sleep(dt_rest);          
            }
        }
    }
}