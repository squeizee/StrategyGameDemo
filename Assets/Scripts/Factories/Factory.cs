using Interfaces;
using UnityEngine;

namespace Factories
{
    public abstract class Factory : MonoBehaviour
    {
       public abstract IPlaceable Create(Transform parent);
    }
    
}