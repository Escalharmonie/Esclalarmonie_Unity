using DG.Tweening;
using UI.Theme;
using UnityEngine;

public class PillThemeUpdater : MonoBehaviour
{
    #region Parameters

    [SerializeField] private ImageColorFader? Shadow;
    [SerializeField] private ImageColorFader? Diffuse;
    [SerializeField] private ImageColorFader? InnerShadow;
    [SerializeField] private ImageColorFader? Highlight;
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
        Shadow?.SetColor(ThemeColorPalette.CurrentShadowColor);
        Diffuse?.SetColor(ThemeColorPalette.CurrentDiffuseColor);
        InnerShadow?.SetColor(ThemeColorPalette.CurrentInnerShadowColor);
        Highlight?.SetColor(ThemeColorPalette.CurrentHighlightColor);
    }

    private void SetPillTHemeEase()
    {
        Shadow?.SetColorEase(ThemeColorPalette.CurrentShadowColor, TransitionDuration, TransitionEasing);
        Diffuse?.SetColorEase(ThemeColorPalette.CurrentDiffuseColor, TransitionDuration, TransitionEasing);
        InnerShadow?.SetColorEase(ThemeColorPalette.CurrentInnerShadowColor, TransitionDuration, TransitionEasing);
        Highlight?.SetColorEase(ThemeColorPalette.CurrentHighlightColor, TransitionDuration, TransitionEasing);
    }
}