using System;
using System.Collections.Generic;
using Controllers;
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
        public List<Vector2Int> GridPosition { get; private set; }
        public BuildingSo So => so;
        public int Health
        {
            get => currentHealth;
            set => currentHealth = value;
        }
        public bool IsDead => currentHealth <= 0;
        
        public void Init()
        {
            Health = so.health;
        }
        public void Place(Vector3 position, List<Vector2Int> gridPositions)
        {
            transform.position = position;
            GridPosition = gridPositions;
            
            Debug.Log($"{So.buildingType} placed.");
        }

        

        public virtual void TakeDamage(int damage)
        {
            currentHealth -= damage;

            Debug.Log($"{So.buildingType} took {damage} damage. Remaining health: {currentHealth}");
            
            if (IsDead)
            {
                GridController.Instance.SetEmpty(transform.position, So.dimension);
                DestroyPlaceable();
            }
        }
        
        private void DestroyPlaceable()
        {
            Destroy(gameObject);
        }

        
    }
}