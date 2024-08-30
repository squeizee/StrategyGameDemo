using System;
using Buildings;
using DG.Tweening;
using UI;
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

        private void OnEnable()
        {
            BuildManager.Instance.OnBuildingPlaced += ShowProductPanel;
            productionPanel.OnProductSelected += OnProductSelected;
            InputManager.Instance.OnBuildingSelected += SetInformationPanel;
        }

        private void OnDisable()
        {
            BuildManager.Instance.OnBuildingPlaced -= ShowProductPanel;
            productionPanel.OnProductSelected -= OnProductSelected;
            InputManager.Instance.OnBuildingSelected -= SetInformationPanel;
        }

        private void Start()
        {
            informationPanel.Hide(0f);
        }
    
        private void OnProductSelected()
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