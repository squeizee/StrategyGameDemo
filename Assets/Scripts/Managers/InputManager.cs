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
        
        public Vector3 MousePosition => GetGridSnappedPosition(Input.mousePosition);

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnLeftClick?.Invoke(GetGridSnappedPosition(Input.mousePosition));
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
    }
}