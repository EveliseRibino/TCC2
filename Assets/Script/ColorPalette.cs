using UnityEngine;

[CreateAssetMenu(fileName = "ColorPalette", menuName = "Theme/Color Palette")]
public class ColorPalette : ScriptableObject
{
    public Color backgroundColor;
    public Color primaryTextColor;
    public Color accentColor;
}