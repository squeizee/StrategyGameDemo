using System;
using Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace Buildings
{
    public class Building : MonoBehaviour, IDamageable
    {
        private enum BuildingType
        {
            Barracks,
            PowerPlant,
        }
        
        [SerializeField] private BuildingType buildingType;
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
            
            Debug.Log($"{buildingType} took {damage} damage. Remaining health: {health}");
        }
    }
}
