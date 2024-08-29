using System.Collections.Generic;
using BuildingSystem;
using PathFinding;
using UnityEngine;

namespace Controllers
{
    public enum CellType
    {
        None,
        Empty,
        Building,
        Unit,
    }
    
    public class GridController : MonoBehaviour
    {
        public static GridController Instance;
        
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Cell cellPrefab;
        
        private readonly Vector3 _cellOffset = new(.5f, .5f, 0);
        
        private int _gridWidth,_gridHeight;
        private List<Cell> _cells = new();
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        private void Start()
        {
            CreateGrid();
        }
        
        public bool TryGetEmptyCellAtPosition(Vector3 position, out Vector3 cellWorldPosition)
        {
            var gridPosition = GetGridPosition(position);
            var cell = GetCell(gridPosition);
            if (!cell)
            {
                cellWorldPosition = Vector3.zero;
                return false;
            }
            cellWorldPosition = cell.WorldPosition - _cellOffset;
            return cell.CellType == CellType.Empty;
        }
        public Node[,] GetNodes()
        {
            var nodes = new Node[_gridWidth, _gridHeight];
            
            foreach (var cell in _cells)
            {
                nodes[cell.GridPosition.x, cell.GridPosition.y] = 
                    new Node(cell.CellType == CellType.Empty,
                        cell.WorldPosition, cell.GridPosition.x, cell.GridPosition.y);
            }
            
            return nodes;
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
        private Vector2Int GetGridPosition(Vector3 worldPosition)
        {
            var firstPos = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
            var x = Mathf.FloorToInt(worldPosition.x - firstPos.x);
            var y = Mathf.FloorToInt(worldPosition.y - firstPos.y);
            
            return new Vector2Int(x, y);
        }
        
        private Cell GetCell(Vector2Int gridPosition)
        {
            return _cells.Find(cell => cell.GridPosition == gridPosition);
        }
        
        
    }
}