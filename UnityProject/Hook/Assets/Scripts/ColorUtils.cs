using UnityEngine;

public static class ColorUtils {

    public static Color BackgroundColor {
        get {
            Color color;
            if (Player.IsLightSOn()) {
                ColorUtility.TryParseHtmlString("#F0F0E6FF", out color);
            } else {
                ColorUtility.TryParseHtmlString("#0f0f19FF", out color);
            }
            return color;
        }
    }

    public static Color LineColor {
        get {
            Color color;
            if (Player.IsLightSOn()) {
                ColorUtility.TryParseHtmlString("#585451FF", out color);
            } else {
                ColorUtility.TryParseHtmlString("#a7abaeFF", out color);
            }
            return color;
        }
    }

    public static Color Lerp(Color a, Color b, float t) {
        float rdif = b.r - a.r;
        float gdif = b.g - a.g;
        float bdif = b.b - a.b;
        float adif = b.a - a.a;
        Color result = new Color(a.r + rdif * t, a.g + gdif * t, a.b + bdif * t, a.a + adif * t);
        return result;
    }

}
