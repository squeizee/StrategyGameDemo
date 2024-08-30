using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class Panel : MonoBehaviour
    {
        protected int MoveDirection = 0;
        protected float InitialXPosition;
        public virtual void Hide(float duration = .5f)
        {
            // Move the panel out of the screen
            transform.DOLocalMoveX(MoveDirection * Screen.width, duration).SetEase(Ease.InOutBack);
        }
        public virtual void Show(float duration = .5f)
        {
            // Move the panel to the screen
            transform.DOLocalMoveX(InitialXPosition, duration).SetEase(Ease.InOutBack);
        }
    }
}