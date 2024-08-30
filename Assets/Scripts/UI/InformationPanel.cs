using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InformationPanel : Panel
    {
        private void Start()
        {
            MoveDirection = 1;
            InitialXPosition = transform.localPosition.x;
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