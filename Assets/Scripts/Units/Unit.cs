using Interfaces;
using UnityEngine;

namespace Units
{
    public class Unit : MonoBehaviour, IDamageable
    {
        private enum UnitType
        {
            Soldier,
        }
        
        [SerializeField] private UnitType unitType;
        [SerializeField] private int health = 100;
        
        public int Health
        {
            get => health;
            set => health = value;
        }
        
        public bool IsDead => health <= 0;
        public void TakeDamage(int damage)
        {
            health -= damage;
            
            Debug.Log($" {unitType} took {damage} damage. Remaining health: {health}");
        }
    }
}