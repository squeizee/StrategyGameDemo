using System;
using BuildingSystem;
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
        
        private Vector3 _cellOffset = new Vector3(.5f, .5f, 0);
        private Cell[,] _grid;

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

        private void CreateGrid()
        {
            int gridWidth = Mathf.RoundToInt(mainCamera.aspect * mainCamera.orthographicSize * 2);
            int gridHeight = Mathf.RoundToInt(mainCamera.orthographicSize * 2);
            
            _grid = new Cell[gridWidth, gridHeight];
            
            Vector3 firstPos = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
            
            for (int i = 0; i < gridWidth; i++)
            {
                for (int j = 0; j < gridHeight; j++)
                {
                    Cell cellObject = Instantiate(cellPrefab, transform);
                    cellObject.Init($"Cell ({i}, {j})", 
                        CellType.Empty, 
                        new Vector2Int(i, j), 
                        new Vector3(firstPos.x + i + .5f, firstPos.y + j + .5f));
                    
                    _grid[i, j] = cellObject;
                }
            }
        }
        
        public bool IsValidToBuild(Vector3 position, out Vector3 cellPosition)
        {
            Vector2Int cellPos = GetGridPosition(position);
            cellPosition = _grid[cellPos.x, cellPos.y].transform.position - _cellOffset;
            return _grid[cellPos.x, cellPos.y].CellType == CellType.Empty;
        }

        private Vector2Int GetGridPosition(Vector3 position)
        {
            Vector3 firstPos = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
            int x = Mathf.FloorToInt(position.x - firstPos.x);
            int y = Mathf.FloorToInt(position.y - firstPos.y);
            return new Vector2Int(x, y);
        }
    }
}