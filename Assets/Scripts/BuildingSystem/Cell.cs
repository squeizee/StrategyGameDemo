using Controllers;
using UnityEngine;
using BuildingSystem;
using PathFinding;
using UnityEngine.Serialization;

namespace BuildingSystem
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private Vector2Int gridPosition;
        [SerializeField] private CellType cellType;
        
        public Vector2Int GridPosition => gridPosition;
        public Vector3 WorldPosition => transform.position;
        public CellType CellType => cellType;

        public void Init(string objectName,CellType cType, Vector2Int gridPos, Vector3 localPos)
        {
            name = objectName;
            cellType = cType;
            gridPosition = gridPos;
            transform.localPosition = localPos;
        }
    }
}