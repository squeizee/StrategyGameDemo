using System;
using Units;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UnitProduct : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image image;
        [SerializeField] private UnitType unitType;
        
        public Action<UnitType> OnProductClicked;
        
        private void Start()
        {
            button??=GetComponent<Button>();
            image??=GetComponent<Image>();
        }
        
        public void Init(Sprite sprite, UnitType uType)
        {
            image.sprite = sprite;
            unitType = uType;
            
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => OnProductClicked?.Invoke(unitType));
        }
    }
}