using UI.Theme;
using UnityEngine;
using UnityEngine.Events;

public class ThemeManager : MonoBehaviour
{
    #region Singleton

    public static ThemeManager? Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    #endregion

    #region Events

    public UnityEvent<bool> OnThemeUpdated;

    #endregion

    public void UpdateThemePalette(Color? newColor, bool ease = true)
    {
        if (newColor is not null)
        {
            ThemeColorPalette.CurrentHighlightColor =
                RotateHue(ThemeColorPalette.DefaultColoredHighlightColor, (Color)newColor);
            ThemeColorPalette.CurrentInnerShadowColor =
                RotateHue(ThemeColorPalette.DefaultColoredInnerShadowColor, (Color)newColor);
            ThemeColorPalette.CurrentDiffuseColor =
                RotateHue(ThemeColorPalette.DefaultColoredDiffuseColor, (Color)newColor);
            ThemeColorPalette.CurrentShadowColor =
                RotateHue(ThemeColorPalette.DefaultColoredShadowColor, (Color)newColor);
        }
        else
        {
            ThemeColorPalette.CurrentHighlightColor = ThemeColorPalette.DefaultMissingHighlightColor;
            ThemeColorPalette.CurrentInnerShadowColor = ThemeColorPalette.DefaultMissingInnerShadowColor;
            ThemeColorPalette.CurrentDiffuseColor = ThemeColorPalette.DefaultMissingDiffuseColor;
            ThemeColorPalette.CurrentShadowColor = ThemeColorPalette.DefaultMissingShadowColor;
        }

        OnThemeUpdated.Invoke(ease);
    }

    private Color RotateHue(Color originalHueColor, Color newHueColor)
    {
        Color.RGBToHSV(originalHueColor, out float _, out float originalSaturation, out float originalValue);
        Color.RGBToHSV(newHueColor, out float newHue, out float _, out float _);

        return Color.HSVToRGB(newHue, originalSaturation, originalValue);
    }
}