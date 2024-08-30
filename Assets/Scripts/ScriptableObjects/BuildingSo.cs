using Buildings;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewBuilding", menuName = "ScriptableObjects/Building", order = 1)]
    public class BuildingSo : ScriptableObject
    {
        public string buildingName;
        public BuildingType buildingType;
        public GameObject buildingPrefab;
        public Texture2D buildingIcon;
        public Vector2Int dimensions;
        public int health;
    }
}