using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ApplyColorFromPalette : MonoBehaviour
{
    public ColorPalette palette;
    public ColorType colorType;

    public enum ColorType
    {
        BackgroundColor,
        PrimaryTextColor,
        AccentColor
    }

    void Awake()
    {
        ApplyColor();
    }

    void ApplyColor()
    {
        if (palette == null) return;

        Color colorToApply = Color.white;
        switch (colorType)
        {
            case ColorType.BackgroundColor:
                colorToApply = palette.backgroundColor;
                break;
            case ColorType.PrimaryTextColor:
                colorToApply = palette.primaryTextColor;
                break;
            case ColorType.AccentColor:
                colorToApply = palette.accentColor;
                break;
        }

        // Tenta aplicar a cor a uma Imagem
        Image image = GetComponent<Image>();
        if (image != null)
        {
            image.color = colorToApply;
        }

        // Tenta aplicar a cor a um Texto
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        if (text != null)
        {
            text.color = colorToApply;
        }
    }
}