using Buildings;
using Interfaces;
using UnityEngine;

namespace Factories
{
    public class PowerPlantFactory : Factory
    {
        [SerializeField] private PowerPlant powerPlantPrefab;
        
        public override IPlaceable Create()
        {
            return Instantiate(powerPlantPrefab);
        }
    }
}