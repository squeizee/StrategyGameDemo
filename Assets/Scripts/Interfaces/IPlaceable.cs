using System.Collections.Generic;
using Buildings;
using UnityEngine;

namespace Interfaces
{
    public interface IPlaceable
    {
        // Place is a method of the interface
        void Place(Vector3 position,List<Vector2Int> gridPosition);
        // GridPosition is a property of the interface
        List<Vector2Int> GridPosition { get; }
        void Init();
    }
}