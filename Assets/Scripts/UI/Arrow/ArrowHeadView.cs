using UnityEngine;
using UnityEngine.UI;

namespace UI.Arrow
{
    public class ArrowHeadView : MonoBehaviour
    {
        [SerializeField] private Sprite _normalArrow;
        [SerializeField] private Sprite _crossfire;
        [SerializeField] private Image _image;

        public void CanUseCard(bool value)
        {
            var color = value ? Color.green : Color.red;
            _image.color = color;
            _image.sprite = value ? _crossfire : _normalArrow;
        }
    }
}