using Buildings;
using Interfaces;
using UnityEngine;

namespace Factories
{
    public class BuildingFactory : MonoBehaviour
    {
        public BarrackFactory barrackFactory;
        public PowerPlantFactory powerPlantFactory;
        
        public IPlaceable CreateBuilding(BuildingType buildingType)
        {
            IPlaceable building = buildingType switch
            {
                BuildingType.Barrack => barrackFactory.Create(),
                BuildingType.PowerPlant => powerPlantFactory.Create(),
                _ => null
            };

            return building;
        }
    }
}