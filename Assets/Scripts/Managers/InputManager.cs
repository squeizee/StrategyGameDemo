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
                OnRightClick?.Invoke(GetGridSnappedPosition(Input.mousePosition));
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