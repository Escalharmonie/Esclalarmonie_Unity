using DG.Tweening;
using UnityEngine;

namespace UI.Utils
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasGroupVisibilityFader : MonoBehaviour
    {
        #region Parameters

        [SerializeField] private float TransitionDuration = 0.25f;
        [SerializeField] private Ease TransitionEasing = Ease.InOutSine;

        #endregion

        #region Private Members

        private CanvasGroup? _canvasGroup;

        #endregion

        #region Start

        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();

            if (_canvasGroup is null)
            {
                WarnMissingGroup();
            }
        }
        #endregion

        #region Public Functions

        public void SetAlphaEase(float newAlpha)
        {
            SetAlphaEase(newAlpha, null, null);
        }
        
        public void SetAlphaEase(float newAlpha, float? transitionDuration = null, Ease? transitionEasing = null )
        {
            transitionDuration ??= TransitionDuration;
            transitionEasing ??= TransitionEasing;
            
            if (_canvasGroup is not null)
            {
                _canvasGroup.DOFade(newAlpha, (float)transitionDuration).SetEase((Ease)transitionEasing);
            }
            else
            {
                WarnMissingGroup();
            }
        }

        public void SetAlpha(float newAlpha)
        {
            if (_canvasGroup is not null)
            {
                _canvasGroup.alpha = newAlpha;
            }
            else
            {
                WarnMissingGroup();
            }
        }

        #endregion

        #region Private Functions

        private void WarnMissingGroup()
        {
            Debug.LogWarning("Canvas Group Ref Is Missing");
        }

        #endregion
    }
}