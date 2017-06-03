using UnityEngine;

public static class ColorUtils {

    public static Color BackgroundColor {
        get {
            Color color = Color.white;
            ColorUtility.TryParseHtmlString("#585451FF", out color);
            return color;
        }
    }

    public static Color LineColor {
        get {
            Color color = Color.white;
            ColorUtility.TryParseHtmlString("#585451FF", out color);
            return color;
        }
    }
}
