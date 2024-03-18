using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageColorFader : MonoBehaviour
{
     #region Parameters
    
            [SerializeField] private float TransitionDuration = 0.25f;
            [SerializeField] private Ease TransitionEasing = Ease.InOutSine;
    
            #endregion
    
            #region Private Members
    
            private Image? _image;
    
            #endregion
    
            #region Start
    
            private void Start()
            {
                _image = GetComponent<Image>();
    
                if (_image is null)
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
    
                if (_image is not null)
                {
                    _image.DOColor(newColor, (float)transitionDuration).SetEase((Ease)transitionEasing);
                }
                else
                {
                    WarnMissingText();
                }
            }
    
            public void SetColor(Color newColor)
            {
                if (_image is not null)
                {
                    _image.color = newColor;
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
                Debug.LogWarning("Image Ref Is Missing");
            }
    
            #endregion
}
