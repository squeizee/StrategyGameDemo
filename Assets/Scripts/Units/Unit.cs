using System.Collections.Generic;
using Controllers;
using DG.Tweening;
using Interfaces;
using PathFind;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Units
{
    public enum UnitType
    {
        Soldier1,
        Soldier2,
        Soldier3,
    }
    public class Unit : MonoBehaviour, IDamageable, IPlaceable
    {
        [SerializeField] private UnitSo unitSo;
        [SerializeField] private int health = 100;
        
        public List<Vector2Int> GridPosition { get; private set; }
        
        private Sequence _sequence;
        
        public int Health
        {
            get => health;
            set => health = value;
        }
        public UnitSo So => unitSo;
        public bool IsDead => health <= 0;
        
        public void Place(Vector3 position, List<Vector2Int> gridPositions)
        {
            transform.position = position;
            GridPosition = gridPositions;
            
            Debug.Log($"{So.unitType} placed.");
        }

        public void Init()
        {
            Health = unitSo.health;
        }
        public void TakeDamage(int damage)
        {
            health -= damage;
            
            Debug.Log($" {So.unitType} took {damage} damage. Remaining health: {health}");
            
            if (IsDead)
            {
                GridController.Instance.SetEmpty(transform.position, So.dimension);
                DestroyUnit();
            }
        }
        
        public void OnUnitSelect()
        {
            transform.DOComplete();
            transform.DOScale(Vector3.one * 1.2f, 0.2f).SetLoops(4, LoopType.Yoyo);
            
            Debug.Log($"{So.unitType} selected");
        }
        public Sequence MoveAlongPath(List<Point> path)
        {
            transform.DOComplete();

            var firstPosition = transform.localPosition;
            
            List<Vector3> pathVector3 = GridController.Instance.GetPathVector3(path);
            
            _sequence?.Kill();
            _sequence = DOTween.Sequence();
            
            foreach (var point in pathVector3)
            {
                _sequence.Append(transform.DOLocalMove(point, 1/unitSo.moveSpeed));
            }
            _sequence.AppendCallback(()=>
            {
                GridController.Instance.SetEmpty(firstPosition, unitSo.dimension);
                GridController.Instance.Place(transform.position, CellType.Unit, unitSo.dimension, out var gridPositions);
                GridPosition = gridPositions;
            });
            
            return _sequence;
        }
        
        private void DestroyUnit()
        {
            Destroy(gameObject);
        }

        
    }
}