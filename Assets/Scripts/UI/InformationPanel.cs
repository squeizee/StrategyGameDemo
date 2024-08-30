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
        }

        public override void Show()
        {
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
        }
        
    }
}