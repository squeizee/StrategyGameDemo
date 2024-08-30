using Buildings;
using Interfaces;
using UnityEngine;

namespace Factories
{
    public class BuildingFactory : MonoBehaviour
    {
        public BarrackFactory barrackFactory;
        public PowerPlantFactory powerPlantFactory;
        
        public IPlaceable CreateBuilding(BuildingType buildingType, Transform parent)
        {
            IPlaceable building = buildingType switch
            {
                BuildingType.Barrack => barrackFactory.Create(parent),
                BuildingType.PowerPlant => powerPlantFactory.Create(parent),
                _ => null
            };

            return building;
        }
    }
}