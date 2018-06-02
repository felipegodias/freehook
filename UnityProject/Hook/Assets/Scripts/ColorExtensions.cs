using UnityEngine;

public static class ColorExtensions
{

    public static Color SetAlpha(this Color color, float aplha)
    {
        color.a = aplha;
        return color;
    }

}