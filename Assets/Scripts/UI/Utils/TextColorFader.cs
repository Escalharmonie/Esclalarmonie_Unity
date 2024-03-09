using DG.Tweening;
using TMPro;
using UnityEngine;
using Color = UnityEngine.Color;

namespace UI.Utils
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextColorFader : MonoBehaviour
    {
        #region Parameters

        [SerializeField] private float TransitionDuration = 0.25f;
        [SerializeField] private Ease TransitionEasing = Ease.InOutSine;

        #endregion

        #region Private Members

        private TextMeshProUGUI? _textMeshPro;

        #endregion

        #region Start

        private void Start()
        {
            _textMeshPro = GetComponent<TextMeshProUGUI>();

            if (_textMeshPro is null)
            {
                WarnMissingText();
            }
        }

        #endregion

        #region Public Functions

        public void SetColorEase(Color newColor, float? transitionDuration = null, Ease? transitionEasing = null)
        {
            transitionDuration ??= TransitionDuration;
            transitionEasing ??= TransitionEasing;

            if (_textMeshPro is not null)
            {
                _textMeshPro.DOColor(newColor, (float)transitionDuration).SetEase((Ease)transitionEasing);
            }
            else
            {
                WarnMissingText();
            }
        }

        public void SetColor(Color newColor)
        {
            if (_textMeshPro is not null)
            {
                _textMeshPro.color = newColor;
            }
            else
            {
                WarnMissingText();
            }
        }

        #endregion

        #region Private Functions

        private void WarnMissingText()
        {
            Debug.LogWarning("Text Mesh Pro Ref Is Missing");
        }

        #endregion
    }
}