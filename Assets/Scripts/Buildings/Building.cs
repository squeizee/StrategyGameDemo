using System;
using Interfaces;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Buildings
{
    public enum BuildingType
    {
        Barrack,
        PowerPlant,
    }

    public abstract class Building : MonoBehaviour, IPlaceable ,IDamageable
    {
        [SerializeField] private BuildingSo so;
        [SerializeField] private int currentHealth;

        public int Health
        {
            get => currentHealth;
            set => currentHealth = value;
        }
        public bool IsDead => currentHealth <= 0;

        public void Initialize()
        {
            Health = so.health;
        }

        public virtual void TakeDamage(int damage)
        {
            currentHealth -= damage;

            Debug.Log($"{so.buildingType} took {damage} damage. Remaining health: {currentHealth}");
            
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