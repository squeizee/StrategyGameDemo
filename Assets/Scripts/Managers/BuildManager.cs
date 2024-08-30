using System;
using BuildingSystem;
using Controllers;
using Factories;
using ScriptableObjects;
using UnityEngine;
using Utility;

namespace Managers
{
    public class BuildManager : MonoSingleton<BuildManager>
    {
        public Action OnBuildingPlaced;

        [SerializeField] private PreviewObject previewObject;
        [SerializeField] private Transform board;
        [SerializeField] private BuildingFactory buildingFactory;
        private BuildingSo _selectedBuilding;

        private State _state;

        private enum State
        {
            Idle,
            Moving,
            Building,
        }

        private void Start()
        {
            _state = State.Idle;
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
            _state = State.Moving;
        }

        private void TryMovePreviewObject(Vector3 cursorPos)
        {
            if (_selectedBuilding && _state == State.Moving)
            {
                previewObject.ChangePosition(cursorPos,
                    GridController.Instance.IsPlaceValid(cursorPos, _selectedBuilding.dimensions, out _));
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void TryToBuild(Vector3 position)
        {
            if (!_selectedBuilding)
            {
                return;
            }

            if (GridController.Instance.IsPlaceValid(position, _selectedBuilding.dimensions, out var buildingPosition))
            {
                _state = State.Building;
                GridController.Instance.Place(position, CellType.Building, _selectedBuilding.dimensions);


                var building = buildingFactory.CreateBuilding(_selectedBuilding.buildingType, board);
                building.Place(buildingPosition);
                OnBuildingPlaced?.Invoke();

                _state = State.Idle;
                previewObject.Hide();
                _selectedBuilding = null;
            }
            else
            {
                Debug.Log("Invalid Position");
            }
        }
    }
}