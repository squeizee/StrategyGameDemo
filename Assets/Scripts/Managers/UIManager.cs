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

        private void OnProductSelected()
        {
            HideAllPanels();
        }
        
        private void ShowProductPanel()
        {
            productionPanel.Show();
        }
        private void HideAllPanels()
        {
            informationPanel.Hide();
            productionPanel.Hide();
        }
    }
}