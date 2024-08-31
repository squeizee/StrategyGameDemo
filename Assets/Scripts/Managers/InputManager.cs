using System;
using Controllers;
using UnityEngine;
using UnityEngine.EventSystems;
using Utility;

namespace Managers
{
    public class InputManager : MonoSingleton<InputManager>
    {
        [SerializeField] private Camera mainCamera;
        
        public Action<Vector3> OnLeftClick, OnRightClick;
        public Action<GameObject> OnBuildingSelected, OnUnitSelected;
        public Action<GameObject,Vector2> OnAttack;
        public Vector3 MousePosition => GetGridSnappedPosition(Input.mousePosition);
        
        private void Update()
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                if (GetGameObjectUnderMouse(LayerMask.NameToLayer("Board"), out var underMouse, out var hit))
                {
                    if (underMouse.CompareTag("Building"))
                    {
                        OnBuildingSelected?.Invoke(underMouse);
                    }
                    else if (underMouse.CompareTag("Unit"))
                    {
                        OnUnitSelected?.Invoke(underMouse);
                    }
                }
                else
                {
                    OnLeftClick?.Invoke(GetGridSnappedPosition(Input.mousePosition));
                }
                
                
            }
            if (Input.GetMouseButtonDown(1))
            {
                var pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                
                // if CellType is Empty
                if (GridController.Instance.IsEmptyCell(pos))
                {
                    OnRightClick?.Invoke(pos);
                }
                else if (GetGameObjectUnderMouse(LayerMask.NameToLayer("Board"), out var underMouse, out var hit))
                {
                    if (underMouse.CompareTag("Building") || underMouse.CompareTag("Unit"))
                    {
                        OnAttack?.Invoke(underMouse,hit.point);
                    }
                }
            }
        }
        
        private Vector3 GetGridSnappedPosition(Vector3 position)
        {
            var pos = mainCamera.ScreenToWorldPoint(position);
            return GridController.Instance.GetSnappedPosition(pos);
        }

        private bool GetGameObjectUnderMouse(LayerMask layerMask, out GameObject underMouse, out RaycastHit2D hit)
        {
            if (mainCamera != null)
            {
                Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                hit = Physics2D.Raycast(mousePosition, Vector2.zero,layerMask);
                
                if (hit.collider != null)
                {
                    GameObject hitObject = hit.collider.gameObject;
                    underMouse = hitObject.transform.parent.gameObject;

                    return true;
                }
            }

            hit = new RaycastHit2D();
            underMouse = null;
            return false;
        }
    }
}