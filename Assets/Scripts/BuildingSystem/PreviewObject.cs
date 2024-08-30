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
            transform.localScale = new Vector3(dimensions.x, dimensions.y, 1);
        }
        
        public void ChangePosition(Vector3 position, bool isValid)
        {
            transform.position = position;
            SetColor(isValid ? _validColor : _invalidColor);
        }

        public void OnInvalidPosition()
        {
            SetColor(_invalidColor);
        }

        public void OnValidPosition()
        {
            SetColor(_validColor);
        }

        private void SetColor(Color color)
        {
            spriteRenderer.color = color;
        }
    }
}