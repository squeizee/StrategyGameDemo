using UnityEngine;

namespace Controllers
{
    public class GridController : MonoBehaviour
    {
        private enum CellType
        {
            None,
            Empty,
            Building,
            Unit,
        }
        
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Transform cellPrefab;
        
        private CellType[,] _grid;
        
        private void Start()
        {
            CreateGrid();
        }

        private void CreateGrid()
        {
            int gridWidth = Mathf.RoundToInt(mainCamera.aspect * mainCamera.orthographicSize * 2);
            int gridHeight = Mathf.RoundToInt(mainCamera.orthographicSize * 2);
            
            _grid = new CellType[gridWidth, gridHeight];
            
            Vector3 firstPos = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
            
            for (int i = 0; i < gridWidth; i++)
            {
                for (int j = 0; j < gridHeight; j++)
                {
                    Transform cell = Instantiate(cellPrefab, transform);
                    cell.name = $"Cell ({i}, {j})";
                    cell.localPosition = new Vector2(firstPos.x + i + .5f, firstPos.y + j + .5f);
                    
                    _grid[i, j] = CellType.Empty;
                }
            }
        }
    }
}