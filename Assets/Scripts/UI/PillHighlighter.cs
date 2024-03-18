using System.Collections;
using DG.Tweening;
using UI.Theme;
using UI.Utils;
using UnityEngine;
using Color = UnityEngine.Color;

namespace UI
{
    public class PillHighlighter : MonoBehaviour
    {
        #region Parameters
        
        [SerializeField] private float TransitionDuration = 0.25f;
        [SerializeField] private Ease TransitionEasing = Ease.InOutSine;
        [Space(10)]
        [SerializeField] private CanvasGroupVisibilityFader? CanvasFader;
        [SerializeField] private TextColorFader? TextFade;

        #endregion

        public bool Highlighted = false;
        
        #region Public Members


        public bool IsHighlighted
        {
            get => _isHighlighted;
            set
            {
                _isHighlighted = value;
                SetPillHighlight();
                Highlighted = _isHighlighted;
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
            Color newTextColor = ThemeColorPalette.DefaultTextColor;

            if (CanvasFader is not null)
            {
                CanvasFader.SetAlpha(newAlpha);
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

        private void SetPillHighlight()
        {
            float newAlpha = _isHighlighted ? 1f : 0f;
            Color newTextColor = _isHighlighted ? 
                ThemeColorPalette.HighlightedTextColor : ThemeColorPalette.DefaultTextColor ;

            if (CanvasFader is not null)
            {
                CanvasFader.SetAlphaEase(newAlpha, TransitionDuration, TransitionEasing);
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