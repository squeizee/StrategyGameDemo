using System;
using BuildingSystem;
using Controllers;
using ScriptableObjects;
using UnityEngine;
using Utility;

namespace Managers
{
    public class BuildManager : MonoSingleton<BuildManager>
    {
        public Action OnBuildingPlaced;
            
        [SerializeField] private PreviewObject previewObject;
        
        private BuildingSo _selectedBuilding;
        
        

        private void Start()
        {
            InputManager.Instance.OnLeftClick += TryToBuild;
            previewObject.Hide();
        }
        private void OnDisable()
        {
            InputManager.Instance.OnLeftClick -= TryToBuild;
        }

        private void Update()
        {
            TryMovePreviewObject(InputManager.Instance.MousePosition);
        }

        public void SetSelectedBuilding(BuildingSo buildingSo)
        {
            _selectedBuilding = buildingSo;
            previewObject.Init(_selectedBuilding.buildingIcon, _selectedBuilding.dimensions);
        }
        
        private void TryMovePreviewObject(Vector3 cursorPos)
        {
            if (_selectedBuilding)
            {
                previewObject.ChangePosition(cursorPos, 
                    GridController.Instance.IsPlaceValid(cursorPos, _selectedBuilding.dimensions));
            }
        }
        private void TryToBuild(Vector3 position)
        {
            if(!_selectedBuilding)
            {
                return;
            }
            
            if(GridController.Instance.TryGetEmptyCellAtPosition(position, out var buildingPosition))
            {
                Instantiate(_selectedBuilding.buildingPrefab, buildingPosition, Quaternion.identity);
            }
            else
            {
                Debug.Log("Can't build at " + position);
            }
            
        }
        
        

        
    }
}