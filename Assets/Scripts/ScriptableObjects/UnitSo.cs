using Units;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu (fileName = "NewUnit", menuName = "ScriptableObjects/Unit", order = 1)]
    public class UnitSo : ScriptableObject
    {
        public string unitName;
        public UnitType unitType;
        public Unit unitPrefab;
        public Sprite unitIcon;
        public Vector2Int dimension;
        public int health;
        public int damage;
        public float attackRange;
        public float attackSpeed;
        public float moveSpeed;
    }
}