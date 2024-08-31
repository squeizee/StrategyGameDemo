using System.Collections.Generic;
using BuildingSystem;
using PathFind;
using UnityEngine;
using UnityEngine.EventSystems;
using Utility;

namespace Controllers
{
    public enum CellType
    {
        None,
        Empty,
        Building,
        Unit,
    }

    public class GridController : MonoSingleton<GridController>
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Cell cellPrefab;

        private readonly Vector3 _cellOffset = new(.5f, .5f, 0);

        private int _gridWidth, _gridHeight;
        private List<Cell> _cells = new();

        private GameObject _selectedObject;

        private void Start()
        {
            CreateGrid();
        }

        public void Place(Vector3 position, CellType cellType, Vector2Int dim, out List<Vector2Int> gridPositions)
        {
            gridPositions = new List<Vector2Int>();
            var gridPosition = GetGridPosition(position);
            for (int i = gridPosition.x; i < gridPosition.x + dim.x; i++)
            {
                for (int j = gridPosition.y; j < gridPosition.y + dim.y; j++)
                {
                    var cell = GetCell(new Vector2Int(i, j));
                    if (cell)
                    {
                        cell.SetCellType(cellType);
                        gridPositions.Add(cell.GridPosition);
                    }
                }
            }
        }

        public void SetEmpty(Vector3 position, Vector2Int dim)
        {
            var gridPosition = GetGridPosition(position);
            for (int i = gridPosition.x; i < gridPosition.x + dim.x; i++)
            {
                for (int j = gridPosition.y; j < gridPosition.y + dim.y; j++)
                {
                    var cell = GetCell(new Vector2Int(i, j));
                    if (cell)
                    {
                        cell.SetCellType(CellType.Empty);
                    }
                }
            }
        }
        public Vector3 GetSnappedPosition(Vector3 position)
        {
            var gridPosition = GetGridPosition(position);
            var cell = GetCell(gridPosition);
            if (!cell)
            {
                return position;
            }

            //ignore Z axis
            var worldPos = cell.WorldPosition;
            worldPos.z = 0;

            return worldPos - _cellOffset;
        }
        public bool IsCloseEnough(List<Vector2Int> obj1, List<Vector2Int> obj2, float minDistance)
        {
            foreach (var pos1 in obj1)
            {
                foreach (var pos2 in obj2)
                {
                    var distance = Vector2.Distance(pos1, pos2);
                    if (distance <= minDistance)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        public bool[,] GetCellMap()
        {
            bool[,] cellMap = new bool[_gridWidth, _gridHeight];
            for (int i = 0; i < _gridWidth; i++)
            {
                for (int j = 0; j < _gridHeight; j++)
                {
                    var cell = GetCell(new Vector2Int(i, j));
                    cellMap[i, j] = cell.CellType == CellType.Empty;
                }
            }

            return cellMap;
        }

        public bool FindPath(Vector3 from, Vector3 to, out List<Point> path)
        {
            var fromGridPos = GetGridPosition(from);
            var toGridPos = GetGridPosition(to);
            return FindPath(fromGridPos, toGridPos, out path);
        }

        

        public bool IsPlaceValid(Vector3 worldPos, Vector2Int selectedBuildingDimensions, out Vector3 buildingPos)
        {
            buildingPos = GetSnappedPosition(worldPos);
            return IsValidToBuild(selectedBuildingDimensions, worldPos);
        }

        public bool IsEmptyCell(Vector3 position)
        {
            var gridPosition = GetGridPosition(position);
            var cell = GetCell(gridPosition);
            return cell != null && cell.CellType == CellType.Empty;
        }
        public Vector2Int GetGridPosition(Vector3 worldPosition)
        {
            var firstPos = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
            var x = Mathf.FloorToInt(worldPosition.x - firstPos.x);
            var y = Mathf.FloorToInt(worldPosition.y - firstPos.y);

            return new Vector2Int(x, y);
        }

        public List<Vector3> GetPathVector3(List<Point> path)
        {
            List<Vector3> pathVector3 = new List<Vector3>();

            foreach (var point in path)
            {
                pathVector3.Add(GetCell(new Vector2Int(point.X, point.Y)).WorldPosition - _cellOffset);
            }
            
            return pathVector3;
        }
        private bool FindPath(Vector2Int from, Vector2Int to, out List<Point> path)
        {
            if (to.x < 0 || to.x >= _gridWidth || to.y < 0 || to.y >= _gridHeight)
            {
                path = new List<Point>();
                return false;
            }

            return FindPath(new Point(from.x, from.y), new Point(to.x, to.y), out path);
        }

        private bool FindPath(Point from, Point to, out List<Point> path)
        {
            if (from == to)
            {
                path = new List<Point>();
                return true;
            }

            if (from.X < 0 || from.X >= _gridWidth || from.Y < 0 || from.Y >= _gridHeight)
            {
                path = new List<Point>();
                return false;
            }

            var cellMap = GetCellMap();

            PathFind.Grid grid = new PathFind.Grid(_gridWidth, _gridHeight, cellMap);
            path = Pathfinding.FindPath(grid, from, to);

            return path.Count > 0;
        }
        private bool IsValidToBuild(Vector2Int dim, Vector3 position)
        {
            var gridPosition = GetGridPosition(position);
            if (gridPosition.x + dim.x > _gridWidth || gridPosition.y + dim.y > _gridHeight)
            {
                return false;
            }

            for (int i = gridPosition.x; i < gridPosition.x + dim.x; i++)
            {
                for (int j = gridPosition.y; j < gridPosition.y + dim.y; j++)
                {
                    var cell = GetCell(new Vector2Int(i, j));
                    if (cell == null || cell.CellType != CellType.Empty)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void CreateGrid()
        {
            _gridWidth = Mathf.RoundToInt(mainCamera.aspect * mainCamera.orthographicSize * 2);
            _gridHeight = Mathf.RoundToInt(mainCamera.orthographicSize * 2);

            Vector3 firstPos = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));

            for (int i = 0; i < _gridWidth; i++)
            {
                for (int j = 0; j < _gridHeight; j++)
                {
                    Cell cellObject = Instantiate(cellPrefab, transform);
                    cellObject.Init($"Cell ({i}, {j})",
                        CellType.Empty,
                        new Vector2Int(i, j),
                        new Vector3(firstPos.x + i + .5f, firstPos.y + j + .5f));

                    _cells.Add(cellObject);
                }
            }
        }

        

        private Cell GetCell(Vector2Int gridPosition)
        {
            return _cells.Find(cell => cell.GridPosition == gridPosition);
        }
    }
}