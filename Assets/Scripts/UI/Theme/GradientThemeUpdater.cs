using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UI.Theme;
using UnityEngine;
using UnityEngine.Serialization;

public class GradientThemeUpdater : MonoBehaviour
{
    #region Parameters
    
    [SerializeField] private ImageColorFader? Gradient;
    [Space(10)] [SerializeField] private float TransitionDuration = 0.25f;
    [SerializeField] private Ease TransitionEasing = Ease.InOutSine;

    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        SetPillTHeme();

        if (ThemeManager.Instance is null)
        {
            Debug.Log("Missing Theme Manager Instance");
        }

        ThemeManager.Instance?.OnThemeUpdated.AddListener(UpdateTheme);
    }

    private void OnDestroy()
    {
        ThemeManager.Instance?.OnThemeUpdated.RemoveListener(UpdateTheme);
    }

    private void UpdateTheme(bool ease)
    {
        if (ease)
        {
            SetPillTHemeEase();
        }
        else
        {
            SetPillTHeme();
        }
    }
    
    private void SetPillTHeme()
    {
        Gradient?.SetColor(ThemeColorPalette.CurrentShadowColor);
        
    }

    private void SetPillTHemeEase()
    {
        Gradient?.SetColorEase(ThemeColorPalette.CurrentShadowColor, TransitionDuration, TransitionEasing);
    }
}
