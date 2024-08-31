using System;
using Interfaces;
using Managers;
using Units;
using UnityEngine;

namespace Controllers
{
    public class BoardController : MonoBehaviour
    {
        private Unit _selectedUnit;

        private void OnEnable()
        {
            InputManager.Instance.OnUnitSelected += SetSelectedUnit;
            InputManager.Instance.OnBuildingSelected += RemoveSelectedUnit;
            InputManager.Instance.OnRightClick += TryMoveUnit;
            InputManager.Instance.OnAttack += TryAttack;
        }


        private void RemoveSelectedUnit(GameObject obj)
        {
            _selectedUnit = null;
        }

        private void OnDisable()
        {
            InputManager.Instance.OnUnitSelected -= SetSelectedUnit;
            InputManager.Instance.OnBuildingSelected -= RemoveSelectedUnit;
            InputManager.Instance.OnRightClick -= TryMoveUnit;
        }

        private void SetSelectedUnit(GameObject obj)
        {
            _selectedUnit = obj.TryGetComponent(out Unit unit) ? unit : null;

            _selectedUnit?.OnUnitSelect();
        }

        private void TryAttack(GameObject targetObject, Vector2 hitPoint)
        {
            if (targetObject == _selectedUnit.gameObject)
            {
                return;
            }

            if (_selectedUnit)
            {
                var tPlaceable = targetObject.TryGetComponent(out IPlaceable placeable) ? placeable : null;
                
                if (tPlaceable == null) return;
                
                if (GridController.Instance.IsCloseEnough(_selectedUnit.GridPosition,tPlaceable.GridPosition,_selectedUnit.So.attackRange))
                {
                    var target = targetObject.TryGetComponent(out IDamageable damageable) ? damageable : null;
                    target?.TakeDamage(_selectedUnit.So.damage);
                }

            }
        }

        private void TryMoveUnit(Vector3 targetPosition)
        {
            if (_selectedUnit)
            {
                if (GridController.Instance.FindPath(_selectedUnit.transform.position, targetPosition, out var path))
                {
                    _selectedUnit.MoveAlongPath(path);
                }
            }
        }
    }
}