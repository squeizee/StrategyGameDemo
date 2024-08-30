using System.Collections.Generic;
using Buildings;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewBuilding", menuName = "ScriptableObjects/Building", order = 1)]
    public class BuildingSo : ScriptableObject
    {
        public string buildingName;
        public BuildingType buildingType;
        public Building buildingPrefab;
        public Sprite buildingIcon;
        public Vector2Int dimensions;
        public int health;
        public bool canProduceUnits;
        public List<UnitSo> producibleUnits;
    }
    
}