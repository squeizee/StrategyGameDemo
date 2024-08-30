using System;
using Buildings;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BuildingProduct : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image image;
        [SerializeField] private BuildingType buildingType;
        
        public Action<BuildingType> OnProductClicked;

        private void Start()
        {
            button??=GetComponent<Button>();
            image??=GetComponent<Image>();
        }

        public void Init(Sprite sprite, BuildingType bType,Vector3 position)
        {
            image.sprite = sprite;
            buildingType = bType;
            transform.localPosition = position;
            
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => OnProductClicked?.Invoke(buildingType));
            
        }
    }
}