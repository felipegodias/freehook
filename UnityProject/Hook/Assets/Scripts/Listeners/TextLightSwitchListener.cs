using TMPro;

using UnityEngine;

public class TextLightSwitchListener : LightSwitchListener
{

    private TextMeshPro[] texts;

    [SerializeField]
    private bool useMainColor = true;

    protected override void Awake()
    {
        texts = GetComponentsInChildren<TextMeshPro>(true);
        base.Awake();
    }

    protected override void UpdateColors()
    {
        foreach (TextMeshPro text in texts)
        {
            Color oldColor = text.color;
            Color newColor = useMainColor ? ColorUtils.LineColor : ColorUtils.BackgroundColor;
            newColor.a = oldColor.a;
            text.color = newColor;
        }
    }

}