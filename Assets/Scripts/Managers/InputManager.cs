using System;
using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }
        
        [SerializeField] private Camera mainCamera;
        
        public Action<Vector3> OnLeftClick, OnRightClick;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnLeftClick?.Invoke(GetWorldPosition(Input.mousePosition));
            }
            if (Input.GetMouseButtonDown(1))
            {
                OnRightClick?.Invoke(GetWorldPosition(Input.mousePosition));
            }
        }
        
        private Vector3 GetWorldPosition(Vector3 screenPosition)
        {
            return mainCamera.ScreenToWorldPoint(screenPosition);
        }
    }
}