using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

namespace Managers
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Transform cellPrefab;

        private void Start()
        {
            CreateGrid();
        }

        private void CreateGrid()
        {
            int gridWidth = Mathf.RoundToInt(mainCamera.aspect * mainCamera.orthographicSize * 2);
            int gridHeight = Mathf.RoundToInt(mainCamera.orthographicSize * 2);
            
            Vector3 firstPos = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
            
            for (int i = 0; i < gridWidth; i++)
            {
                for (int j = 0; j < gridHeight; j++)
                {
                    Transform cell = Instantiate(cellPrefab, transform);
                    cell.name = $"Cell ({i}, {j})";
                    cell.localPosition = new Vector2(firstPos.x + i + .5f, firstPos.y + j + .5f);
                }
            }
        }
    }
}