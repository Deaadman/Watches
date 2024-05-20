namespace Watches.Utilities;

internal class UserInterfaceUtilities
{
    internal static UILabel SetupGameObjectUILabel(string gameObjectName, Transform parent, bool worldPositionStays, float posX, float posY, float posZ)
    {
        GameObject newLabelObject = new(gameObjectName);
        UILabel label = newLabelObject.AddComponent<UILabel>();

        if (parent != null)
        {
            newLabelObject.transform.SetParent(parent, worldPositionStays);
        }
        newLabelObject.transform.localPosition = new Vector3(posX, posY, posZ);

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

    internal static void UISpriteAlphas(Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppArrayBase<UISprite> sprites, bool alphaVisibility)
    {
        foreach (var sprite in sprites)
        {
            if (alphaVisibility)
            {
                switch (sprite.gameObject.name)
                {
                    case "horizon":
                        sprite.alpha = 0.7843f;
                        break;
                    case "arrows":
                        sprite.alpha = 0f;
                        break;
                    case "sun":
                        sprite.alpha = 1f;
                        break;
                    case "moon":
                        sprite.alpha = 1f;
                        break;
                    case "bg":
                        sprite.alpha = 0.116f;
                        break;
                    case "glow":
                        sprite.alpha = 0.078f;
                        break;
                }
            }
            else
            {
                sprite.alpha = 0f;
            }
        }
    }

    internal static bool CheckUISpriteAlphas(Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppArrayBase<UISprite> sprites)
    {
        foreach (var sprite in sprites)
        {
            switch (sprite.gameObject.name)
            {
                case "sun":
                    if (sprite.alpha == 1f)
                    {
                        return true;
                    }
                return false;
            }
        }

        return false;
    }
}