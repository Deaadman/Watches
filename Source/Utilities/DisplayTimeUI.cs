using Watches.Components;

namespace Watches.Utilities;

internal static class DisplayTimeUI
{
    internal static GameObject SetupDisplayTimesGameObject(Transform parent, bool worldPositionStays)
    {
        GameObject gameObject = new("DisplayTimes");
        gameObject.AddComponent<DisplayTime>();
        gameObject.transform.SetParent(parent, worldPositionStays);
        
        return gameObject;
    }

    internal static UILabel SetupDigitalTime(Transform parent, bool worldPositionStays)
    {
        GameObject gameObject = new("DigitalTime");
        gameObject.transform.SetParent(parent, worldPositionStays);
        gameObject.SetActive(false);
        
        var uiLabel = gameObject.AddComponent<UILabel>();
        SetupUILabel(
            uiLabel,
            string.Empty,
            FontStyle.Normal,
            UILabel.Crispness.Always,
            NGUIText.Alignment.Center,
            UILabel.Overflow.ResizeFreely,
            false,
            0,
            18,
            Color.white,
            true
        );

        return uiLabel;
    }
    
    private static void SetupUILabel(
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
        label.ambigiousFont = InterfaceManager.GetPanel<Panel_Subtitles>().m_Label_Subtitles.ambigiousFont;
        label.bitmapFont = InterfaceManager.GetPanel<Panel_Subtitles>().m_Label_Subtitles.bitmapFont;
        label.font = InterfaceManager.GetPanel<Panel_Subtitles>().m_Label_Subtitles.font;
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
}