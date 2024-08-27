using Controllers;
using UnityEngine;
using BuildingSystem;

namespace BuildingSystem
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private Vector2Int position;
        [SerializeField] private CellType cellType;
        
        public Vector2Int Position => position;
        public CellType CellType => cellType;

        public void Init(string objectName,CellType cType, Vector2Int gridPos, Vector3 localPos)
        {
            name = objectName;
            cellType = cType;
            position = gridPos;
            transform.localPosition = localPos;
        }
    }
}