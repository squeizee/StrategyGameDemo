using System;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class InformationPanel : Panel
    {
        public Action OnProductSelected;

        [SerializeField] private TMP_Text productText;
        [SerializeField] private Image productImage;
        [SerializeField] private TMP_Text dimensionText;
        [SerializeField] private Transform productionsParent;
        [SerializeField] private UnitProduct productionPrefab;

        private void Awake()
        {
            MoveDirection = 1;
            InitialXPosition = transform.localPosition.x;
        }

        public void Init(BuildingSo buildingSo)
        {
            productText.text = buildingSo.buildingName;
            productImage.sprite = buildingSo.buildingIcon;
            dimensionText.text = $"Dimension: {buildingSo.dimensions.x}x{buildingSo.dimensions.y}";
            foreach (var production in buildingSo.producibleUnits)
            {
                var unit = Instantiate(productionPrefab, productionsParent);
                unit.Init(production.unitIcon, production.unitType);
            }
        }

        public override void Show(float duration = .5f)
        {
            base.Show(duration);
        }

        public override void Hide(float duration = .5f)
        {
            base.Hide(duration);
        }
    }
}