using System;
using System.Collections.Generic;
using Managers;
using ScriptableObjects;
using TMPro;
using Units;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class InformationPanel : Panel
    {
        public Action OnUnitProductSelected;

        [SerializeField] private TMP_Text productText;
        [SerializeField] private Image productImage;
        [SerializeField] private TMP_Text dimensionText;
        [SerializeField] private Transform productionsParent;
        [SerializeField] private UnitProduct productionPrefab;

        [SerializeField] private List<UnitSo> unitSos;
        private void Awake()
        {
            MoveDirection = 1;
            InitialXPosition = transform.localPosition.x;
        }

        public void Init(BuildingSo buildingSo)
        {
            if(buildingSo.buildingName == productText.text) return;
            
            productText.text = buildingSo.buildingName;
            productImage.sprite = buildingSo.buildingIcon;
            dimensionText.text = $"Dimension: {buildingSo.dimension.x}x{buildingSo.dimension.y}";
            foreach (var production in buildingSo.producibleUnits)
            {
                var unit = Instantiate(productionPrefab, productionsParent);
                unit.Init(production.unitIcon, production.unitType);
                unit.OnProductClicked += UnitProductClicked;
            }
        }
        private void UnitProductClicked(UnitType unitType)
        {
            var unitSo = unitSos.Find(x => x.unitType == unitType);
            ObjectPlacer.Instance.SpawnUnit(unitSo);
            OnUnitProductSelected?.Invoke();
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