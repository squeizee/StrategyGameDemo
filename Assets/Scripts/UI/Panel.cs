using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class Panel : MonoBehaviour
    {
        protected int MoveDirection = 1;
        public virtual void Hide()
        {
            // Move the panel out of the screen
            transform.DOMoveX(MoveDirection * Screen.width, .5f).SetEase(Ease.InOutBack);
        }
        public virtual void Show()
        {
            // Move the panel to the screen
            transform.DOMoveX(0, .5f).SetEase(Ease.InOutBack);
        }
    }
}