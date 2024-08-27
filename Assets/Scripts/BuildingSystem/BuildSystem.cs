using System;
using Controllers;
using Managers;
using UnityEngine;

namespace BuildingSystem
{
    public class BuildSystem : MonoBehaviour
    {
        [SerializeField] private GameObject barrackPrefab;
        
        private float _cellSize = .32f;
        private void Start()
        {
            InputManager.Instance.OnLeftClick += TryToBuild;
        }
        
        private void TryToBuild(Vector3 position)
        {
            if(GridController.Instance.IsValidToBuild(position, out var buildingPosition))
            {
                Instantiate(barrackPrefab, buildingPosition, Quaternion.identity);
            }
            else
            {
                Debug.Log("Can't build at " + position);
            }
            
        }
    }
}