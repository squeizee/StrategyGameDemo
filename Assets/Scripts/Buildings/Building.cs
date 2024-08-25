using Interfaces;
using UnityEngine;

namespace Buildings
{
    public enum BuildingType
    {
        Barracks,
        PowerPlant,
    }

    public abstract class Building : MonoBehaviour, IPlaceable ,IDamageable
    {
        [SerializeField] protected BuildingType buildingType;
        [SerializeField] private int health = 100;

        public int Health
        {
            get => health;
            set => health = value;
        }

        public bool IsDead => health <= 0;

        public virtual void TakeDamage(int damage)
        {
            health -= damage;

            Debug.Log($"{buildingType} took {damage} damage. Remaining health: {health}");
            
            if (IsDead)
            {
                DestroyPlaceable();
            }
        }
        
        private void DestroyPlaceable()
        {
            Destroy(gameObject);
        }

        public void Place()
        {
            
        }
    }
}