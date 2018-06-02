using UnityEngine;
using UnityEngine.UI;

public class GraphicLightSwitchListener : LightSwitchListener
{

    [SerializeField]
    private bool useMainColor = true;

    private Graphic[] graphics;

    protected override void Awake()
    {
        graphics = GetComponentsInChildren<Graphic>(true);
        base.Awake();
    }

    protected override void UpdateColors()
    {
        foreach (Graphic graphic in graphics)
        {
            Color oldColor = graphic.color;
            Color newColor = useMainColor ? ColorUtils.LineColor : ColorUtils.BackgroundColor;
            newColor.a = oldColor.a;
            graphic.color = newColor;
        }
    }

}