using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class ProductionPanel : Panel
    {
        [SerializeField] private BuildingProduct buildingProductPrefab;
        [SerializeField] private Transform productsParent;
        
        [SerializeField] private List<BuildingSo> buildingSos;
        
        [Header("Minimum Product for Infinite Scroll")]
        [SerializeField] private int productCount = 12;
        
        [SerializeField] private bool randomizeProducts = true;
        [SerializeField] private List<BuildingProduct> _buildingProducts = new();
        private Vector3 _initialPosition = new Vector3(-100f, -125f, 0f);
        
        private const int _columnCount = 2;
        private float _increment = 200f;
        private const float threshold = 200f;
        
        private float _lastCheckedYPosition = 200f;

        private void Start()
        {
            CreateProducts();
        }

        private void Update()
        {
            if (productsParent.localPosition.y > _lastCheckedYPosition + threshold)
            {
                MoveToBottom();
                _lastCheckedYPosition += threshold;
            }
            else if (productsParent.localPosition.y < _lastCheckedYPosition - threshold)
            {
                MoveToTop();
                _lastCheckedYPosition -= threshold;
            }
        }
        
        

        private void CreateProducts()
        {
            for (int i = 0; i < productCount / _columnCount; i++)
            {
                // Left Side
                BuildingProduct buildingProduct = Instantiate(buildingProductPrefab, productsParent);
                var position = _initialPosition + new Vector3(0, -_increment * i, 0);
                var randomBuildIndex = UnityEngine.Random.Range(0, buildingSos.Count);
                buildingProduct.Init(buildingSos[randomBuildIndex].buildingIcon, buildingSos[randomBuildIndex].buildingType, position);
                
                _buildingProducts.Add(buildingProduct);
                
                // Right Side
                buildingProduct = Instantiate(buildingProductPrefab, productsParent);
                position = _initialPosition + new Vector3(_increment, -_increment * i, 0);
                randomBuildIndex = UnityEngine.Random.Range(0, buildingSos.Count);
                buildingProduct.Init(buildingSos[randomBuildIndex].buildingIcon, buildingSos[randomBuildIndex].buildingType, position);
                
                _buildingProducts.Add(buildingProduct);
            }
        }
        
        private void MoveToBottom()
        {
            var firstTwo = _buildingProducts.GetRange(0,2);
            _buildingProducts.RemoveRange(0,2);
        
            var lastYValue = _buildingProducts[^1].transform.localPosition.y;
        
            firstTwo[0].transform.localPosition = new Vector3(firstTwo[0].transform.localPosition.x, lastYValue - threshold, firstTwo[0].transform.localPosition.z);
            firstTwo[1].transform.localPosition = new Vector3(firstTwo[1].transform.localPosition.x, lastYValue - threshold, firstTwo[1].transform.localPosition.z);


            if (randomizeProducts)
            {
                var randomBuildIndex = UnityEngine.Random.Range(0, buildingSos.Count);
                firstTwo[0].Init(buildingSos[randomBuildIndex].buildingIcon, buildingSos[randomBuildIndex].buildingType, firstTwo[0].transform.localPosition);
                randomBuildIndex = UnityEngine.Random.Range(0, buildingSos.Count);
                firstTwo[1].Init(buildingSos[randomBuildIndex].buildingIcon, buildingSos[randomBuildIndex].buildingType, firstTwo[1].transform.localPosition);
            }
            
            _buildingProducts.AddRange(firstTwo);
        
        }
    
        private void MoveToTop()
        {
            var lastTwo = _buildingProducts.GetRange(_buildingProducts.Count - 2, 2);
            _buildingProducts.RemoveRange(_buildingProducts.Count - 2, 2);
        
            var firstYValue = _buildingProducts[0].transform.localPosition.y;
        
            lastTwo[0].transform.localPosition = new Vector3(lastTwo[0].transform.localPosition.x, firstYValue + threshold, lastTwo[0].transform.localPosition.z);
            lastTwo[1].transform.localPosition = new Vector3(lastTwo[1].transform.localPosition.x, firstYValue + threshold, lastTwo[1].transform.localPosition.z); 
        
            if (randomizeProducts)
            {
                var randomBuildIndex = UnityEngine.Random.Range(0, buildingSos.Count);
                lastTwo[0].Init(buildingSos[randomBuildIndex].buildingIcon, buildingSos[randomBuildIndex].buildingType, lastTwo[0].transform.localPosition);
                randomBuildIndex = UnityEngine.Random.Range(0, buildingSos.Count);
                lastTwo[1].Init(buildingSos[randomBuildIndex].buildingIcon, buildingSos[randomBuildIndex].buildingType, lastTwo[1].transform.localPosition);
            }
            _buildingProducts.InsertRange(0, lastTwo);
        }
        
        
    }
}