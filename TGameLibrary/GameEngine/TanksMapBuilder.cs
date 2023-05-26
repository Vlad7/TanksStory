using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using TanksGameEngine.GameEngine.Components;
using TanksGameEngine.TanksGame.Objects.Capsules;
using TanksGameEngine.TanksGame.Objects.CharacterParts.Bodies;
using TanksGameEngine.TanksGame.Objects.CharacterParts.Guns;
using TanksGameEngine.TanksGame.Objects.CharacterParts.Catarpillars;
using TanksGameEngine.TanksGame.Objects.Characters;
using TanksGameEngine.TanksGame.Objects.Explosions;
using TanksGameEngine.TanksGame.Objects.MapElements;

namespace TanksGameEngine.GameEngine
{
    public class TanksMapBuilder : MapBuilder
    {
        public override void BuildSpecialObject(string name, String type, Locator loc, Vector size, CollisionShape shape, Viewer viewer)
        {
            switch (type.ToLower())
            {             
                case "solidblock":
                    {
                        BuildSolidBlock(name, loc, size, shape, viewer);
                    }
                    break;
                case "woodbox":
                    {
                        BuildWoodBox(name, loc, size, shape, viewer);
                    }
                    break;
                case "steelblock":
                    {
                        BuildSteelBlock(name, loc, size, shape, viewer);
                    }
                    break;
                case "brickblock":
                    {
                        BuildBrickBlock(name, loc, size, shape, viewer);                     
                    }
                    break;
                case "barrel":
                    {
                        BuildBarrel(name, loc, size, shape, viewer);
                    }
                    break;
                case "respaun":
                    {
                        BuildRespaun(name, loc, size, shape, viewer);
                    }
                    break;
                default:
                    {
                        BuildStandartObject(name, loc, size, shape, viewer);
                        
                    }
                    break;
            }
        }

        public SolidBlock BuildSolidBlock(string name, Locator loc, Vector size, CollisionShape shape, Viewer viewer)
        {
            SolidBlock obj = new SolidBlock(name);

            obj.Viewer = viewer;

            obj.LocalCenter = loc.Center;
            obj.LocalAngle = loc.Angle;
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

        public WoodBox BuildWoodBox(string name, Locator loc, Vector size, CollisionShape shape, Viewer viewer)
        {
            WoodBox obj = new WoodBox(name);

            obj.Viewer = viewer;

            obj.LocalCenter = loc.Center;
            obj.LocalAngle = loc.Angle;
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

        public SteelBlock BuildSteelBlock(string name, Locator loc, Vector size, CollisionShape shape, Viewer viewer)
        {
            SteelBlock obj = new SteelBlock(name);

            obj.Viewer = viewer;

            obj.LocalCenter = loc.Center;
            obj.LocalAngle = loc.Angle;
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

        public BrickBlock BuildBrickBlock(string name, Locator loc, Vector size, CollisionShape shape, Viewer viewer)
        {
            BrickBlock obj = new BrickBlock(name);

            obj.Viewer = viewer;

            obj.LocalCenter = loc.Center;
            obj.LocalAngle = loc.Angle;
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

        public Barrel BuildBarrel(string name, Locator loc, Vector size, CollisionShape shape, Viewer viewer)
        {
            Barrel obj = new Barrel(name);

            obj.Viewer = viewer;

            obj.LocalCenter = loc.Center;
            obj.LocalAngle = loc.Angle;
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

        public Respaun BuildRespaun(string name, Locator loc, Vector size, CollisionShape shape, Viewer viewer)
        {
            Respaun obj = new Respaun(name);

            obj.Viewer = viewer;

            obj.LocalCenter = loc.Center;
            obj.LocalAngle = loc.Angle;
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


        object lockObj;

        public TanksMapBuilder() 
        {
            lockObj = new object();

        }
         
      
 
        public Tank BuildTank(string name, Locator locator, Engine engine)
        {
            Tank tank = new Tank(name);

            tank.LocalCenter = locator.Center;
            tank.LocalAngle = locator.Angle;
            tank.LocalZIndex = locator.ZIndex;

            tank.Engine = engine;
            tank.Engine.Enabled = true;

            if(tank.Name.ToLower() =="player")
                tank.Body =  BuildMachineBody("Body", @"Maps/Textures/dbody128_without_catarpillar.png");
            else
                tank.Body = BuildMachineBody("Body", @"Maps/Textures/dbodyred128_without_catarpillar.png");

            tank.Gun = BuildMachineTower("Gun", @"Maps/Textures/tower_green.png");
            tank.Catarpillar = BuildMachineCatarpillar("Catarpillar", @"Maps/Textures/catarpillars256.png");
            tank.LocationChanged += map.UpdateObject;
            tank.LocationChanged += GameProcess.Current_Game.Manager.RegisterObject;
 

            tank.Crashed += map.RemoveObject;
            tank.Crashed += GameProcess.Current_Game.Camera.TryRemoveIndicator;
            tank.Crashed += GameProcess.Current_Game.Manager.UnRegisterObject;

            tank.LifeChanged += GameProcess.Current_Game.Camera.IndicateLifeChange;

            map.AddObject(tank);

            return tank;
        }


        public Gun BuildMachineTower(string name, string imagePath)
        {
            Gun Gun = new Gun(name, 200);

            Gun.LocalCenter = new Vector(0, 0);
            Gun.LocalAngle =  0;
            Gun.LocalZIndex = 2;

            Gun.Engine = new Engine(new Vector(0, 0), new Vector(3, 3), 0, 5) ;

            Gun.Engine.Enabled = true;

            Gun.Viewer = new Viewer(new Sprite(imagePath, (int)(map.TileSize / 1.41), (int)(map.TileSize/1.41)));
           
            Gun.Size = new Vector(map.TileSize / 2.82, map.TileSize / 2.82);

           
            Gun.CollisionShape = CollisionShape.None;

            return Gun;
        }

        public Body BuildMachineBody(string name, string imagePath)
        {
            Body body = new Body(name);

            body.LocalAngle = 0;
            body.LocalCenter = new Vector(0, 0);
            body.LocalZIndex = 1;

            body.CollisionShape = CollisionShape.Rectangle ;

            body.Size = new Vector(map.TileSize / 2.82, map.TileSize / 2.82);

            body.Viewer = new Viewer(new Sprite(imagePath, (int)(map.TileSize / 1.41), (int)(map.TileSize/1.41)));

            //map.AddObject(body);

            return body;
      
        }

        public Catarpillar BuildMachineCatarpillar(string name, string imagePath)
        {
            Catarpillar catarpillar = new Catarpillar(name);

            catarpillar.LocalAngle = 0;
            catarpillar.LocalCenter = new Vector(0, 0);
            catarpillar.LocalZIndex = 1;

            catarpillar.CollisionShape = CollisionShape.Rectangle ;

            catarpillar.Size = new Vector(map.TileSize / 2.82, map.TileSize / 2.82);

            catarpillar.Viewer = new Viewer(new Sprite(imagePath, (int)(map.TileSize / 1.41), (int)(map.TileSize/1.41)));

            //map.AddObject(body);

            return catarpillar;
      
        }

        public Capsule BuildCapsule(string name, Locator locator, Engine engine)
        {
            Func<Capsule> action = () =>
                {
                    Capsule bl = new Capsule(name);

                    bl.LocalCenter = locator.Center;
                    bl.LocalAngle = locator.Angle;
                    bl.LocalZIndex = locator.ZIndex;

                    bl.Engine = engine;

                    bl.Viewer = new Viewer(new Sprite("Maps /Textures/capsule.png", 30, 30));

                    bl.Size = new Vector(15, 15);

                    bl.CollisionShape = CollisionShape.Rectangle;

                    bl.LocationChanged += map.UpdateObject;
                    bl.LocationChanged += GameProcess.Current_Game.Manager.RegisterObject;
                  
                    bl.Crashed += map.RemoveObject;
                    bl.Crashed += GameProcess.Current_Game.Manager.UnRegisterObject;

                    map.AddObject(bl);

                    return bl;
                };


            if (!Application.Current.Dispatcher.CheckAccess())
            {
                return Application.Current.Dispatcher.Invoke(action);
            }
            else
            {
                return action();
            }        
        }



        public void BuildBoom(Locator locator, Vector size, List<BitmapImage> frames)
        {
            Action action = () =>
            {
                Animation Boom = new Animation();

                foreach (BitmapImage image in frames)
                {
                    Frame frame = new Frame(new Sprite(image), 10);
                    Boom.AddFrame(frame);
                }

                Explosion explosion = new Explosion("boom");

                explosion.LocalCenter = locator.Center;
                explosion.LocalAngle =  locator.Angle;
                explosion.LocalZIndex = locator.ZIndex;

                explosion.Viewer = Boom;

                explosion.Size = size;

                explosion.CollisionShape = CollisionShape.None;

                explosion.LocationChanged += map.UpdateObject;
                explosion.Crashed += map.RemoveObject;
            
                map.AddObject(explosion);      
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

    

    //public enum Guns
    //{
    //    ShotGun, ShockGun, RocketGun, BombGun, CannonGun, EMPGun, ImpulseGun,
    //    RicochetGun, MiniGun, LaserGun, FightGun
    //}

   

    //public enum Explosions
    //{
    //    WoodBlockExplosion, BrickBlockExplosion, BarrelExplosion, TankExplosion, 
    //    EmptyExplosion, CapsuleExplosion, Smoke, Crater
    //}


}
