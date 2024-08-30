using System;
using DG.Tweening;
using UI;
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
        }

        private void OnDisable()
        {
            BuildManager.Instance.OnBuildingPlaced -= ShowProductPanel;
            productionPanel.OnProductSelected -= OnProductSelected;
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