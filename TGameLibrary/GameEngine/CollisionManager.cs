using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace TanksGameEngine.GameEngine
{
    /// <summary>
    /// 
    /// </summary>
    public class CollisionManager
    {
        private Map targetMap;

        public List<GameObject> chekingObjects { get; set; }


        public CollisionManager(Map map)
        {
            targetMap = map;

            chekingObjects = new List<GameObject>();        
        }

        public void UnRegisterObject(GameObject gameObject)
        {
            chekingObjects.Remove(gameObject);
        }

        public void RegisterObject(GameObject gameObject)
        {
            //if(!chekingObjects.Contains(gameObject))
                chekingObjects.Add(gameObject);
        }

        private void ClearRegisteredObjects()
        {
            chekingObjects.Clear();
        }

        

        /// <summary>
        /// Method that return list of primitive objects that may be collision to current primitive object
        /// It is broad phase of collision
        /// </summary>
        /// <param name="current_obj"></param>
        /// <returns></returns>
        public List<PrimitiveObject> MaybeCollisionPrimitives(PrimitiveObject pObj)
        {
            List<GridCorner> cCorners = targetMap.GetCornersByArea(pObj.AbsoluteCenter, pObj.Size, pObj.AbsoluteAngle);

            List<PrimitiveObject> maybeCollisionPrimitives = new List<PrimitiveObject>();

            foreach (GridCorner col_corner in cCorners)
                foreach (PrimitiveObject sObj in col_corner.ObjectsInCorner)
                {
                    if (sObj.CollisionShape != CollisionShape.None) maybeCollisionPrimitives.Add(sObj);
                }

            return maybeCollisionPrimitives.Distinct().ToList();
        }


        public void CollisionHandlerRun()
        {
            var objectsToCheck = chekingObjects.ToArray();

            ClearRegisteredObjects();

            foreach (GameObject gObj in objectsToCheck)
            {
                List<PrimitiveObject> primitiveObjects = gObj.GetPrimitives();
          
                foreach (PrimitiveObject pObj in primitiveObjects)
                {
                    List<PrimitiveObject> maybeColPrimitives = MaybeCollisionPrimitives(pObj);

                    foreach (PrimitiveObject secondObj in maybeColPrimitives)
                    {
                        if (!gObj.Contains(secondObj))
                        {
                            MTV MinTranslate = GetObjectsMTV(pObj, secondObj);

                            if (MinTranslate != null)
                            {
                                GameObject gObjRoot = gObj.GetRootObject();
                                GameObject sObjRoot = secondObj.GetRootObject();

                                if (sObjRoot.IsSolid() && gObjRoot.IsSolid())
                                {
                                    long summWeight = gObjRoot.GetWeight() + sObjRoot.GetWeight();

                                    gObjRoot.LocalCenter += MinTranslate.Direction * MinTranslate.Overlap * (sObjRoot.GetWeight() / summWeight);
                                    sObjRoot.LocalCenter += -MinTranslate.Direction * MinTranslate.Overlap * (gObjRoot.GetWeight() / summWeight);
                                }

                                gObjRoot.Collide(sObjRoot, MinTranslate.Direction, MinTranslate.Overlap);

                               
                                sObjRoot.Collide(gObjRoot, -MinTranslate.Direction, MinTranslate.Overlap);
                                
                                return;
                            }
                        }
                    }
                }
            }     
        }

       

        public static MTV GetObjectsMTV(PrimitiveObject objFirst, PrimitiveObject objSecond)
        {
            MTV MinTranslate = null;

            if (objFirst.CollisionShape == CollisionShape.Point)
            {
                if (objSecond.CollisionShape == CollisionShape.Rectangle)
                {
                    MinTranslate = CollisionPointRectangle(objFirst.AbsoluteCenter,
                        objSecond.AbsoluteCenter, objSecond.Size, objSecond.AbsoluteAngle);

                    return MinTranslate;
                }                
                if (objSecond.CollisionShape == CollisionShape.Circle)
                {
                    MinTranslate = CollisionPointCircle(objFirst.AbsoluteCenter, 
                        objSecond.AbsoluteCenter, objSecond.Size.X);
           
                    return MinTranslate;
                }
                if (objSecond.CollisionShape == CollisionShape.Point)
                {
                    return MinTranslate; 
                }
            }

            if (objFirst.CollisionShape == CollisionShape.Rectangle)
            {
                if (objSecond.CollisionShape == CollisionShape.Rectangle)
                {
                    MinTranslate = CollisionRectangleRectangle(objFirst.AbsoluteCenter, objFirst.Size, 
                        objFirst.AbsoluteAngle, objSecond.AbsoluteCenter, objSecond.Size, objSecond.AbsoluteAngle);
               
                    return MinTranslate;
                }
                if (objSecond.CollisionShape == CollisionShape.Point)
                {
                    MinTranslate = CollisionPointRectangle(objSecond.AbsoluteCenter, 
                        objFirst.AbsoluteCenter, objFirst.Size, objFirst.AbsoluteAngle);

                    if (MinTranslate != null)
                        MinTranslate.Direction *= -1;

                    return MinTranslate;
                }
                if (objSecond.CollisionShape == CollisionShape.Circle)
                {
                    MinTranslate = CollisionCircleRectangle(objSecond.AbsoluteCenter, objSecond.Size.X, 
                        objFirst.AbsoluteCenter, objFirst.Size, objFirst.AbsoluteAngle);

                    if(MinTranslate != null)
                        MinTranslate.Direction *= -1;

                    return MinTranslate;

                    ///Нужно подкорректиировать коллизии от круга
                }
            }

            if (objFirst.CollisionShape == CollisionShape.Circle)
            {
                if (objSecond.CollisionShape == CollisionShape.Rectangle)
                {
                    MinTranslate = CollisionCircleRectangle(objFirst.AbsoluteCenter, objFirst.Size.X,
                        objSecond.AbsoluteCenter, objSecond.Size, objSecond.AbsoluteAngle);

                    return MinTranslate;
                }

                if (objSecond.CollisionShape == CollisionShape.Circle)
                {
                    MinTranslate = CollisionCircleCircle(objFirst.AbsoluteCenter, objFirst.Size.X, objSecond.AbsoluteCenter, objSecond.Size.X);
    
                    return MinTranslate;
                }
                if (objSecond.CollisionShape == CollisionShape.Point)
                {
                    MinTranslate = CollisionPointCircle(objSecond.AbsoluteCenter,
                        objFirst.AbsoluteCenter, objFirst.Size.X);

                    if (MinTranslate != null)
                        MinTranslate.Direction *= -1;

                    return MinTranslate;
                }
            }

            return MinTranslate;
        }


        /// <summary>
        /// Method that check collision of two rectangles placed at an angle
        /// </summary>
        /// <param name="centerFirst"></param>
        /// <param name="sizeFirst"></param>
        /// <param name="angleFirst"></param>
        /// <param name="centerSecond"></param>
        /// <param name="sizeSecond"></param>
        /// <param name="angleSecond"></param>
        /// <returns></returns>
        public static MTV CollisionRectangleRectangle(Vector centerFirst, Vector sizeFirst, Double angleFirst, Vector centerSecond, Vector sizeSecond, Double angleSecond)
        {
            //Можно птимизировать для прямоугольников без углов
            double smallestOverlap = 100000;                                                        //Устанавливаем значение малейшего пересечения как можно больше.

            Vector smallestAxis = new Vector(0, 0);                                                 //Ось выхода из столкновения.

            Vector[] cornersFirst  = GetRectangleVertices(centerFirst, sizeFirst, angleFirst);
            Vector[] cornersSecond = GetRectangleVertices(centerSecond, sizeSecond, angleSecond);

            Vector[] axesFirst  = GetRectangleAxes(cornersFirst);                                   //Получить оси первой и второй фигур.
            Vector[] axesSecond = GetRectangleAxes(cornersSecond);           

            for (int i = 0; i < 2; i++)                                                             //Перебор осей первой фигуры
            {
                Vector axis = axesFirst[i];

                Projection p1 = Projection.GetProjection(cornersFirst, axis);                       //Проецируем первую фигуру на данную ось
                Projection p2 = Projection.GetProjection(cornersSecond, axis);                      //Проецируем вторую фигуру на данную ось

                if (!Projection.IsOverlap(p1, p2))                                                  //Проверяем проекции на пересечение
                {
                    return null;
                }
                else
                {
                    double overlap = Projection.GetOverlap(p1, p2);                                 //Если пересекаются, ищем модуль пересечения

                    if (overlap < smallestOverlap)                                                  //Установления оси минимального пересечения
                    {
                        smallestOverlap = overlap;
                        smallestAxis = axis;
                    }
                }
            }

            for (int i = 0; i < 2; i++)                                                             //Аналогично, только проецируем фигуры на оси второй фигуры
            {
                Vector axis = axesSecond[i];

                Projection p1 = Projection.GetProjection(cornersFirst, axis);
                Projection p2 = Projection.GetProjection(cornersSecond, axis);

                if (!Projection.IsOverlap(p1, p2))
                {
                    return null;
                }
                else
                {
                    double overlap = Projection.GetOverlap(p1, p2);

                    if (overlap < smallestOverlap)
                    {
                        smallestOverlap = overlap;
                        smallestAxis = axis;
                    }
                }

            }

            Vector distantion_vector = (centerFirst - centerSecond);

            if (smallestAxis * distantion_vector < 0)
            {
                smallestAxis *= -1;
            }

            return new MTV(smallestAxis, smallestOverlap);
        }

        /// <summary>
        /// Method that check collision of point and rectangle
        /// </summary>
        /// <param name="pointCenter"></param>
        /// <param name="rectCenter"></param>
        /// <param name="rectSize"></param>
        /// <param name="rectAngle"></param>
        /// <returns></returns>
        public static MTV CollisionPointRectangle(Vector pointCenter, Vector rectCenter, Vector rectSize, Double rectAngle)
        {
            double smallestOverlap = 100000;
            Vector smallestAxis = new Vector(0, 0);                             //Ось выхода из столкновения.

            Vector[] cornersRect = GetRectangleVertices(rectCenter, rectSize, rectAngle);
            Vector[] axesRect = GetRectangleAxes(cornersRect);

            for (int i = 0; i < 2; i++)                                         //Перебор осей прямоугольника фигуры
            {
                Vector axis = axesRect[i];
                Projection rectProj = Projection.GetProjection(cornersRect, axis);    //Проецируем прямоугольник на данную ось
                Double pointProj = Vector.Multiply(axis, pointCenter);     //Проецируем точку на данную ось

                if (!(pointProj > rectProj.Min && pointProj < rectProj.Max))   //Проверяем принадлежность точки проекции
                {
                    return null;
                }
                else
                {
                    double overlap = Math.Min(pointProj - rectProj.Min, rectProj.Max - pointProj);

                    if (overlap < smallestOverlap)                    //Установления оси минимального пересечения
                    {
                        smallestOverlap = overlap;
                        smallestAxis = axis;
                    }
                }
            }

            Vector distantion_vector = (pointCenter - rectCenter);

            if (smallestAxis * distantion_vector < 0)
            {
                smallestAxis *= -1;
            }

            return new MTV(smallestAxis, smallestOverlap);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="circleCenter"></param>
        /// <param name="circleRadius"></param>
        /// <param name="rectCenter"></param>
        /// <param name="rectSize"></param>
        /// <param name="rectAngle"></param>
        /// <returns></returns>
        public static MTV CollisionCircleRectangle(Vector circleCenter, double circleRadius, Vector rectCenter, Vector rectSize, Double rectAngle)
        {
            double smallestOverlap = 100000;                //Устанавливаем значение малейшего пересечения как можно больше.

            Vector smallestAxis = new Vector(0, 0);                           //Ось выхода из столкновения.

            Vector[] rectCorners = GetRectangleVertices(rectCenter, rectSize, rectAngle);
            Vector[] rectAxes = GetRectangleAxes(rectCorners);       //Получить оси первой и второй фигур.

            for (int i = 0; i < 2; i++)                     //Перебор осей первой фигуры
            {
                Vector axis = rectAxes[i];

                Projection rectProj = Projection.GetProjection(rectCorners, axis);    //Проецируем первую фигуру на данную ось

                Double circleCenterProj = Vector.Multiply(axis, circleCenter);

                Projection circProj = new Projection(circleCenterProj - circleRadius, circleCenterProj + circleRadius);

                if (!Projection.IsOverlap(rectProj, circProj))                      //Проверяем проекции на пересечение
                {
                    return null;
                }
                else
                {
                    double overlap = Projection.GetOverlap(rectProj, circProj);    //Если пересекаются, ищем модуль пересечения

                    if (overlap < smallestOverlap)                    //Установления оси минимального пересечения
                    {
                        smallestOverlap = overlap;
                        smallestAxis = axis;
                    }
                }
            }

            Vector minDistVector = rectCorners[0];

            double minSDistValue  = (rectCorners[0] - circleCenter).LengthSquared; 

            for (int i = 1; i < 4; i++)
            {
                double sDistValue = (rectCorners[i] - circleCenter).LengthSquared;

                if (sDistValue < minSDistValue)
                {
                    minDistVector = rectCorners[i];
                    minSDistValue = sDistValue;
                }
            }

            if (minSDistValue < smallestOverlap)
            {
                smallestAxis = minDistVector;

                smallestOverlap = minSDistValue;
            }

            Vector distantion_vector = (circleCenter - rectCenter);

            if (smallestAxis * distantion_vector < 0)
            {
                smallestAxis *= -1;
            }

            return new MTV(smallestAxis, smallestOverlap);
        }
        
        /// <summary>
        /// Collision of point and circle
        /// </summary>
        /// <param name="pointCenter"></param>
        /// <param name="circleCenter"></param>
        /// <param name="circleRadius"></param>
        /// <returns></returns>
        public static MTV CollisionPointCircle(Vector pointCenter, Vector circleCenter, double circleRadius)
        {
            Vector distance = pointCenter - circleCenter;

            if (distance.LengthSquared < Math.Pow(circleRadius, 2))
            {
                Double length = distance.Length;

                return new MTV(new Vector(distance.X / length, distance.Y / length), circleRadius - length);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Collision of point and circle
        /// </summary>
        /// <param name="pointCenter"></param>
        /// <param name="circleCenter"></param>
        /// <param name="circleRadius"></param>
        /// <returns></returns>
        public static MTV CollisionCircleCircle(Vector firstCenter, double firstRadius, Vector secondCenter, double secondRadius)
        {
            Vector distVector = firstCenter - secondCenter;

            Double distValue = distVector.Length;

            Double overlap = distValue - firstRadius - secondRadius;

            if (overlap < 0)
            {
                return new MTV(new Vector(distVector.X / distValue, distVector.Y / distValue), overlap * (-1));
            }

            return null;
        }

        /// <summary>
        /// Method that gets two axis of rectangle
        /// </summary>
        /// <param name="corners"></param>
        /// <returns></returns>
        public static Vector[] GetRectangleAxes(Vector[] corners)
        {
            Vector[] axis = new Vector[2];

            axis[0] = corners[1] - corners[0];    //Первая ось проецирования даного прямоугольника(вектор)    X
            axis[1] = corners[3] - corners[0];    //Вторая ось проецирования даного прямоугольника(вектор)    Y


            axis[0].Normalize();    //Норамалізація не дуже еффективна через обчислення коренів
            axis[1].Normalize();    //Дивись .http://www.flipcode.com/archives/2D_OBB_Intersection.shtml

            return axis;
        }

        /// <summary>
        /// Method that calculates rectangle vercicles with rectangle rotated angle
        /// </summary>
        /// <param name="center"></param>
        /// <param name="size"></param>
        /// <param name="angleDegree"></param>
        /// <returns></returns>
        public static Vector[] GetRectangleVertices(Vector center, Vector size, double angleDegree)
        {
            Vector[] corners = new Vector[4];

            Vector new_sizeX;
            Vector new_sizeY;

          
            double angle_rad = Math.PI * angleDegree / 180.0;

            new_sizeX = new Vector(Math.Cos(angle_rad), Math.Sin(angle_rad)) * size.X;
            new_sizeY = new Vector(-Math.Sin(angle_rad), Math.Cos(angle_rad)) * size.Y;
            

           
            corners[0] = center - new_sizeX - new_sizeY;              //Left, bottom corner
            corners[1] = center + new_sizeX - new_sizeY;              //Right, bottom corner
            corners[2] = center + new_sizeX + new_sizeY;              //Right, top corner
            corners[3] = center - new_sizeX + new_sizeY;              //Left, top corner

            return corners;
        }  
    }





    /// <summary>
    /// Minimal translation vector class. Store information about collision overlap and axis
    /// of minimal overlap.
    /// </summary>
    public class MTV
    {
        /// <summary>
        /// Constructor of Minimal translate vector class 
        /// </summary>
        /// <param name="direct"></param>
        /// <param name="overlap"></param>
        public MTV(Vector direct, Double overlap)
        {
            this.Direction = direct;
            this.Overlap = overlap;
        }

        /// <summary>
        /// Minimal translation direction property(axis)
        /// </summary>
        public Vector Direction
        {
            get;
            set;
        }

        /// <summary>
        /// Overlap value property
        /// </summary>
        public Double Overlap
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Projection class. Stores information about projection of shape on axis
    /// </summary>
    public class Projection
    {
        /// <summary>
        /// Projection class constructor
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public Projection(double min, double max)
        {
            this.Min = min;
            this.Max = max;
        }

        /// <summary>
        /// Maximum limit of projection 
        /// </summary>
        public double Max
        {
            get;
            set;
        }

        /// <summary>
        /// Minimum limit of prodjection
        /// </summary>
        public double Min
        {
            get;
            set;
        }

        /// <summary>
        /// Mehod that returns projection of shape on axis
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static Projection GetProjection(Vector[] vertices, Vector axis)
        {
            double min = Vector.Multiply(axis, vertices[0]);
            double max = min;

            for (int i = 1; i < vertices.Length; i++)
            {
                double p = Vector.Multiply(axis, vertices[i]);   

                if (p < min)
                {
                    min = p;
                }
                else if (p > max)
                {
                    max = p;
                }
            }

            return new Projection(min, max);
        }

        /// <summary>
        /// Method that gets overlap of two projections
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static double GetOverlap(Projection first, Projection second)
        {
            double o_min, o_max;

            if (first.Max >= second.Max)
            {
                o_max = second.Max;
            }
            else
            {
                o_max = first.Max;
            }

            if (first.Min <= second.Min)
            {
                o_min = second.Min;
            }
            else
            {
                o_min = first.Min;
            }

            return o_max - o_min;
        }

        /// <summary>
        /// Method that check overlap of two projections
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool IsOverlap(Projection first, Projection second)
        {
            if (first.Max <= second.Min || first.Min >= second.Max)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
