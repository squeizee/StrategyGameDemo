using Buildings;
using Interfaces;
using UnityEngine;

namespace Factories
{
    public class BarrackFactory : Factory
    {
        [SerializeField] private Barrack barrackPrefab;
        
        public override IPlaceable Create()
        {
            return Instantiate(barrackPrefab);
        }
    }
}