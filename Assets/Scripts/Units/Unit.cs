using Interfaces;
using UnityEngine;

namespace Units
{
    public enum UnitType
    {
        Soldier1,
        Soldier2,
        Soldier3,
    }
    public class Unit : MonoBehaviour, IDamageable
    {
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
            
            if (IsDead)
            {
                DestroyUnit();
            }
        }
        
        private void DestroyUnit()
        {
            Destroy(gameObject);
        }
    }
}