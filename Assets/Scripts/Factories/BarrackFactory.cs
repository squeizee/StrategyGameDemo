using Buildings;
using Interfaces;
using ScriptableObjects;
using UnityEngine;

namespace Factories
{
    public class BarrackFactory : Factory
    {
        [SerializeField] private BuildingSo barrackSo;
        
        public override IPlaceable Create(Transform parent)
        {
            return Instantiate(barrackSo.buildingPrefab, parent);
        }
    }
}