using DG.Tweening;
using UI.Utils;
using UnityEngine;

namespace UI
{
    public class ThumbnailHighlighter : MonoBehaviour
    {
        #region Parameters
        
        [SerializeField] private float TransitionDuration = 0.25f;
        [SerializeField] private Ease TransitionEasing = Ease.InOutSine;
        [Space(10)]
        [SerializeField] private ImageMaterialFloatFader? MaterialFader;
        [SerializeField] private TextColorFader? TextFade;

        #endregion

        #region Public Members
        
        public bool IsHighlighted
        {
            get => _isHighlighted;
            set
            {
                _isHighlighted = value;
                SetThumbnailHighlight();
            }
        }
        
        #endregion
        
        #region Private Members

        private bool _isHighlighted;

        #endregion

        #region Start

        private void Start()
        {
            float newAlpha = 0f;
            Color newTextColor = ColorPalette.DefaultPillTextColor;

            if (MaterialFader is not null)
            {
                MaterialFader.SetFloat(newAlpha, "_HighlightAmount");
            }
            else
            {
                Debug.LogWarning("Missing Canvas Fader Reference");
            }

            if (TextFade is not null)
            {
                TextFade.SetColor(newTextColor);
            }
        }
        
        #endregion
        
        #region Private Functions

        private void SetThumbnailHighlight()
        {
            float newAlpha = _isHighlighted ? 1f : 0f;
            Color newTextColor = _isHighlighted ? 
                ColorPalette.HighlightedPillTextColor : ColorPalette.DefaultPillTextColor ;

            if (MaterialFader is not null)
            {
                MaterialFader.SetFloatEase(newAlpha, "_HighlightAmount", TransitionDuration, TransitionEasing);
            }
            else
            {
                Debug.LogWarning("Missing Canvas Fader Reference");
            }

            if (TextFade is not null)
            {
                TextFade.SetColorEase(newTextColor, TransitionDuration, TransitionEasing);
            }
        }

        #endregion
    }
}
