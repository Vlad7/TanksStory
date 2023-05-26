using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace TanksGameEngine.GameEngine.Controllers
{
    public class PathNode
    {
        // Координаты точки на карте.
        public Point Position { get; set; }

        // Длина пути от старта (G).
        public int PathLengthFromStart { get; set; }     

        // Примерное расстояние до цели (H).
        public int HeuristicEstimatePathLength { get; set; }

        // Точка, из которой пришли в эту точку.
        public PathNode CameFrom { get; set; }

        // Ожидаемое полное расстояние до цели (F).
        public int EstimateFullPathLength
        {
            get
            {
                return this.PathLengthFromStart + this.HeuristicEstimatePathLength;
            }
        }
    }


    public static class AStarSearch
    {
        /// <summary>
        /// Создаем класс для вычисления маршрута. Основной метод вычисления маршрута будет выглядеть так:
        /// </summary>
        /// <param name="field"></param>
        /// <param name="startCorner"></param>
        /// <param name="goalCorner"></param>
        /// <returns></returns>
        public static List<Point> FindPath(Map map, Point startCorner, Point goalCorner)
        {
            // Шаг 1. Создается 2 списка вершин – ожидающие рассмотрения и уже рассмотренные. 
            //        В ожидающие добавляется точка старта, список рассмотренных пока пуст.

            var closedSet = new Collection<PathNode>();
            var openSet   = new Collection<PathNode>();

            // Шаг 2. Для каждой точки рассчитывается F = G + H. G – расстояние от старта 
            //        до точки, H – примерное расстояние от точки до цели. Так же каждая 
            //        точка хранит ссылку на точку, из которой в нее пришли.

            PathNode startNode = new PathNode()
            {
                Position = startCorner,
                CameFrom = null,
                PathLengthFromStart = 0,
                HeuristicEstimatePathLength = GetHeuristicPathLength(startCorner, goalCorner) 
            };

            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                // Шаг 3. Из списка точек на рассмотрение выбирается точка X с наименьшим F.
                var currentNode = openSet.OrderBy(node =>
                  node.EstimateFullPathLength).First();

                // Шаг 4. Если точка X – цель, то мы нашли маршрут.
                if (currentNode.Position == goalCorner)
                    return GetPathForNode(currentNode);

                // Шаг 5. Переносим точку X из списка ожидающих рассмотрения в список уже 
                //        рассмотренных.
                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                // Шаг 6. Для каждой из точек, соседних для X (обозначим эту соседнюю точку Y), 
                //        делаем следующее
                foreach (var neighbourNode in GetNeighbours(currentNode, goalCorner, map))
                {
                    // Шаг 7. Если Y уже находится в рассмотренных – пропускаем ее.

                    if (closedSet.Count(node => node.Position == neighbourNode.Position) > 0)
                        continue;
                    var openNode = openSet.FirstOrDefault(node =>
                      node.Position == neighbourNode.Position);

                    // Шаг 8. Если Y еще нет в списке на ожидание – добавляем ее туда, запомнив ссылку 
                    //        на X и рассчитав Y.G (это X.G + расстояние от X до Y) и Y.H.

                    if (openNode == null)
                        openSet.Add(neighbourNode);
                    else
                        if (openNode.PathLengthFromStart > neighbourNode.PathLengthFromStart)
                        {
                            // Шаг 9. Если же Y в списке на рассмотрение – проверяем, если X.G + расстояние 
                            //        от X до Y < Y.G, значит мы пришли в точку Y более коротким путем, заменяем 
                            //        Y.G на X.G + расстояние от X до Y, а точку, из которой пришли в Y на X.

                            openNode.CameFrom = currentNode;
                            openNode.PathLengthFromStart = neighbourNode.PathLengthFromStart;
                        }
                }
            }

            // Шаг 10. Если список точек на рассмотрение пуст, а до цели мы так и не дошли – значит маршрут 
            //         не существует.
            return null;
        }


        /// <summary>
        /// Первая из них – функция расстояния от X до Y:
        /// </summary>
        /// <returns></returns>
        private static int GetDistanceBetweenNeighbours()
        {
            return 1;
        }

        /// <summary>
        /// Функция примерной оценки расстояния до цели:


        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        private static int GetHeuristicPathLength(Point from, Point to)
        {
            return (int)(Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y));
        }


        /// <summary>
        /// Получение списка соседей для точки:
        /// </summary>
        /// <param name="pathNode"></param>
        /// <param name="goalCorner"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        private static Collection<PathNode> GetNeighbours(PathNode pathNode, Point goalCorner, Map map)
        {
            var result = new Collection<PathNode>();

            // Соседними точками являются соседние по стороне клетки.
            Point[] neighbourPoints = new Point[4];

            neighbourPoints[0] = new Point(pathNode.Position.X + 1, pathNode.Position.Y);
            neighbourPoints[1] = new Point(pathNode.Position.X - 1, pathNode.Position.Y);
            neighbourPoints[2] = new Point(pathNode.Position.X, pathNode.Position.Y + 1);
            neighbourPoints[3] = new Point(pathNode.Position.X, pathNode.Position.Y - 1);

            foreach (var point in neighbourPoints)
            {
                // Проверяем, что не вышли за границы карты.
                if (point.X < 0 || point.X >= map.Columns)
                    continue;

                if (point.Y < 0 || point.Y >= map.Rows)
                    continue;

                // Проверяем, что по клетке можно ходить.
                if (map.IsCornerBusy(point))
                    continue;

                // Заполняем данные для точки маршрута.
                var neighbourNode = new PathNode()
                {
                    Position = point,
                    CameFrom = pathNode,
                    PathLengthFromStart = pathNode.PathLengthFromStart +
                      GetDistanceBetweenNeighbours(),
                    HeuristicEstimatePathLength = GetHeuristicPathLength(point, goalCorner)
                };
                result.Add(neighbourNode);
            }
            return result;
        }

        /// <summary>
        /// Получения маршрута. Маршрут представлен в виде списка координат точек.
        /// </summary>
        /// <param name="pathNode"></param>
        /// <returns></returns>
        private static List<Point> GetPathForNode(PathNode pathNode)
        {
            var result = new List<Point>();
            var currentNode = pathNode;
            while (currentNode != null)
            {
                result.Add(currentNode.Position);
                currentNode = currentNode.CameFrom;
            }
            result.Reverse();
            return result;
        }
    }
}
