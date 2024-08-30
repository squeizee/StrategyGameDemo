using Managers;
using UnityEngine;

namespace BuildingSystem
{
    public class PreviewObject : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        private Color _validColor = new Color(1, 1, 1, 0.5f);
        private Color _invalidColor = new Color(1, 0, 0, 0.5f);

        public void Init(Sprite sprite, Vector2Int dimensions)
        {
            spriteRenderer.sprite = sprite;
            spriteRenderer.transform.localScale = new Vector3(dimensions.x, dimensions.y, 1);
            spriteRenderer.transform.localPosition = new Vector3(dimensions.x / 2f, dimensions.y / 2f, 0);
            transform.localPosition = InputManager.Instance.MousePosition;

            Show();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void ChangePosition(Vector3 position, bool isValid)
        {
            transform.localPosition = position;
            SetColor(isValid ? _validColor : _invalidColor);
        }

        private void SetColor(Color color)
        {
            spriteRenderer.color = color;
        }
    }
}