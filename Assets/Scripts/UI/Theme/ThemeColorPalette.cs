using UnityEngine;

namespace UI.Theme
{
    public static class ThemeColorPalette
    {
        public static Color DefaultColoredHighlightColor {get; private set; }= new Color(0f, 0.88f, 0.42f);
        public static Color DefaultColoredInnerShadowColor {get; private set; }= new Color(0.01f, 0.35f, 0.18f);
        public static Color DefaultColoredDiffuseColor {get; private set; }= new Color(0f, 0.66f, 0.33f);
        public static Color DefaultColoredShadowColor {get; private set; }= new(0f, 0.63f, 0.31f);

        public static Color DefaultMissingHighlightColor {get; private set; }= new Color(0.72f, 0.72f, 0.72f);
        public static Color DefaultMissingInnerShadowColor {get; private set; }= new Color(0.31f, 0.3f, 0.3f);
        public static Color DefaultMissingDiffuseColor {get; private set; }= new Color(0.49f, 0.49f, 0.49f);
        public static Color DefaultMissingShadowColor {get; private set; }= new Color(0.24f, 0.24f, 0.24f);
            
        public static Color CurrentHighlightColor = DefaultMissingHighlightColor;
        public static Color CurrentInnerShadowColor = DefaultMissingInnerShadowColor;
        public static Color CurrentDiffuseColor = DefaultMissingDiffuseColor;
        public static Color CurrentShadowColor = DefaultMissingShadowColor;

        
        
        public static Color DefaultTextColor = new(0.26f, 0.25f, 0.26f);
        public static Color HighlightedTextColor = new(1f, 1f, 1f);
    }
}