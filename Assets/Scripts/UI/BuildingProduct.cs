using Buildings;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BuildingProduct : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private BuildingType buildingType;
        
        public void Init(Sprite sprite, BuildingType bType,Vector3 position)
        {
            image.sprite = sprite;
            buildingType = bType;
            transform.localPosition = position;
        }
    }
}