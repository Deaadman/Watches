namespace Watches.Utilities;

internal static class UIUtilities
{
    internal static bool CheckTimeWidgetSpriteAlphas(Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppArrayBase<UISprite> sprites)
    {
        foreach (var sprite in sprites)
        {
            switch (sprite.gameObject.name)
            {
                case "sun":
                    return Mathf.Approximately(sprite.alpha, 1f);
            }
        }

        return false;
    }
    
    internal static UILabel SetupGameObjectUILabel(string gameObjectName, Transform parent, bool worldPositionStays, float posX, float posY, float posZ)
    {
        GameObject gameObject = new(gameObjectName);
        var label = gameObject.AddComponent<UILabel>();
        gameObject.transform.SetParent(parent, worldPositionStays);
        gameObject.transform.localPosition = new Vector3(posX, posY, posZ);

        return label;
    }

    internal static void SetupUILabel(
            UILabel label,
            string text,
            FontStyle fontStyle,
            UILabel.Crispness crispness,
            NGUIText.Alignment alignment,
            UILabel.Overflow overflow,
            bool mulitLine,
            int depth,
            int fontSize,
            Color color,
            bool capsLock)
    {
        label.text = text;
        label.ambigiousFont = GameManager.GetFontManager().GetUIFontForCharacterSet(FontManager.m_CurrentCharacterSet);
        label.bitmapFont = GameManager.GetFontManager().GetUIFontForCharacterSet(FontManager.m_CurrentCharacterSet);
        label.font = GameManager.GetFontManager().GetUIFontForCharacterSet(FontManager.m_CurrentCharacterSet);

        label.fontStyle = fontStyle;
        label.keepCrispWhenShrunk = crispness;
        label.alignment = alignment;
        label.overflowMethod = overflow;
        label.multiLine = mulitLine;
        label.depth = depth;
        label.fontSize = fontSize;
        label.color = color;
        label.capsLock = capsLock;
    }

    internal static void TimeWidgetSpriteAlphas(Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppArrayBase<UISprite> sprites, bool alphaVisibility)
    {
        foreach (var sprite in sprites)
        {
            if (alphaVisibility)
            {
                sprite.alpha = sprite.gameObject.name switch
                {
                    "horizon" => 0.7843f,
                    "arrows" => 0f,
                    "sun" => 1f,
                    "moon" => 1f,
                    "bg" => 0.116f,
                    "glow" => 0.078f,
                    _ => sprite.alpha
                };
            }
            else
            {
                sprite.alpha = 0f;
            }
        }
    }
}