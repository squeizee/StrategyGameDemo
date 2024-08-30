using Buildings;
using Interfaces;
using ScriptableObjects;
using UnityEngine;

namespace Factories
{
    public class PowerPlantFactory : Factory
    {
        [SerializeField] private BuildingSo powerPlantSo;
        
        public override IPlaceable Create(Transform parent)
        {
            return Instantiate(powerPlantSo.buildingPrefab, parent);
        }
    }
}