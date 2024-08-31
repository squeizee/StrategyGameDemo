using System;
using Buildings;
using BuildingSystem;
using Controllers;
using Factories;
using ScriptableObjects;
using UnityEngine;
using Utility;

namespace Managers
{
    public class ObjectPlacer : MonoSingleton<ObjectPlacer>
    {
        public Action OnBuildingPlaced;

        [SerializeField] private PreviewObject previewObject;
        [SerializeField] private Transform board;
        [SerializeField] private BuildingFactory buildingFactory;
        private BuildingSo _selectedBuildingData;
        private Building _selectedBuilding;

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
            InputManager.Instance.OnBuildingSelected += SetSelectedBuilding;

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


        public void SetSelectedBuildingData(BuildingSo buildingSo)
        {
            _selectedBuildingData = buildingSo;
            previewObject.Init(_selectedBuildingData.buildingIcon, _selectedBuildingData.dimension);
            _state = State.Moving;
        }

        public void SpawnUnit(UnitSo unitSo)
        {
            if (_selectedBuilding)
            {
                if (_selectedBuilding.So.canProduceUnits)
                {
                    var barrack = (Barrack)_selectedBuilding;
                    if (GridController.Instance.IsPlaceValid(barrack.transform.position + barrack.SpawnPoint,
                            unitSo.dimension, out var position))
                    {
                        var unit = Instantiate(unitSo.unitPrefab, board);
                        unit.Init();
                        GridController.Instance.Place(position, CellType.Unit, unitSo.dimension, out var gridPositions);
                        unit.Place(position, gridPositions);
                    }
                    else
                    {
                        Debug.Log("Invalid Spawn Position");
                    }
                }
            }
        }

        private void SetSelectedBuilding(GameObject building)
        {
            if (building.TryGetComponent(out Building b))
            {
                _selectedBuilding = b;
            }
        }

        private void TryMovePreviewObject(Vector3 cursorPos)
        {
            if (_selectedBuildingData && _state == State.Moving)
            {
                previewObject.ChangePosition(cursorPos,
                    GridController.Instance.IsPlaceValid(cursorPos, _selectedBuildingData.dimension, out _));
            }
        }

        private void TryToBuild(Vector3 position)
        {
            if (!_selectedBuildingData)
            {
                return;
            }

            if (GridController.Instance.IsPlaceValid(position, _selectedBuildingData.dimension,
                    out var buildingPosition))
            {
                _state = State.Building;
                GridController.Instance.Place(position, CellType.Building, _selectedBuildingData.dimension,
                    out var gridPositions);


                var building = buildingFactory.CreateBuilding(_selectedBuildingData.buildingType, board);
                building.Init();
                building.Place(buildingPosition, gridPositions);
                OnBuildingPlaced?.Invoke();

                _state = State.Idle;
                previewObject.Hide();
                _selectedBuildingData = null;
            }
            else
            {
                Debug.Log("Invalid Position");
            }
        }
    }
}