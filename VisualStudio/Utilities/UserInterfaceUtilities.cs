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

    internal static void SetupUISprite(UISprite sprite, string spriteName)
    {
        UIAtlas baseAtlas = InterfaceManager.GetPanel<Panel_HUD>().m_AltFireGamepadButtonSprite.atlas;
        UISpriteData spriteData = baseAtlas.GetSprite(spriteName);

        sprite.atlas = baseAtlas;
        sprite.spriteName = spriteName;
        sprite.mSprite = spriteData;
        sprite.mSpriteSet = true;
        //sprite.alpha                = 1f;
        //sprite.color                = Color.white;
        //sprite.MakePixelPerfect();
        //sprite.enabled              = true;
    }
}