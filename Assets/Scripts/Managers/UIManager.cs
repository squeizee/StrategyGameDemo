using System;
using Buildings;
using DG.Tweening;
using UI;
using Units;
using Unity.VisualScripting;
using UnityEngine;
using Utility;

namespace Managers
{
    public class UIManager : MonoSingleton<UIManager>
    {
        [Header("Panels")]
        [SerializeField] private ProductionPanel productionPanel;
        [SerializeField] private InformationPanel informationPanel;

        private bool _isProductPanelActive, _isInformationPanelActive;
        private void OnEnable()
        {
            ObjectPlacer.Instance.OnBuildingPlaced += ShowProductPanel;
            productionPanel.OnBuildingOnProductSelected += OnBuildingProductSelected;
            InputManager.Instance.OnBuildingSelected += SetInformationPanel;
            
            informationPanel.OnUnitProductSelected += OnUnitProductSelected;
        }

        

        private void OnDisable()
        {
            ObjectPlacer.Instance.OnBuildingPlaced -= ShowProductPanel;
            productionPanel.OnBuildingOnProductSelected -= OnBuildingProductSelected;
            InputManager.Instance.OnBuildingSelected -= SetInformationPanel;
            
            informationPanel.OnUnitProductSelected -= OnUnitProductSelected;
        }

        private void Start()
        {
            informationPanel.Hide(0f);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                _isProductPanelActive = !_isProductPanelActive;
                
                if (_isProductPanelActive)
                {
                    ShowProductPanel();
                }
                else
                {
                    HideProductPanel();
                }
            }
            if(Input.GetKeyDown(KeyCode.E))
            {
                _isInformationPanelActive = !_isInformationPanelActive;
                
                if (_isInformationPanelActive)
                {
                    ShowInformationPanel();
                }
                else
                {
                    HideInformationPanel();
                }
            }
        }
        private void OnUnitProductSelected()
        {
            HideInformationPanel();
        }
        private void OnBuildingProductSelected()
        {
            HideProductPanel();
            HideInformationPanel();
        }
        
        private void ShowProductPanel()
        {
            productionPanel.Show();
        }
        private void HideProductPanel()
        {
            productionPanel.Hide();
        }
        private void SetInformationPanel(GameObject building)
        {
            if(building.TryGetComponent(out Building b) && b.So.canProduceUnits)
            {
                informationPanel.Init(b.So);
                ShowInformationPanel();
            }
            else
            {
                HideInformationPanel();
            }
        }
        private void ShowInformationPanel()
        {
            informationPanel.Show();
        }
        private void HideInformationPanel()
        {
            informationPanel.Hide();
        }
    }
}