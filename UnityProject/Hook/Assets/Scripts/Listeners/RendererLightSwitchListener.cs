using UnityEngine;

public class RendererLightSwitchListener : LightSwitchListener
{

    private SpriteRenderer[] spriteRenderers;

    protected override void Awake()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
        base.Awake();
    }

    protected override void UpdateColors()
    {
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            Color oldColor = spriteRenderer.color;
            Color newColor = ColorUtils.LineColor;
            newColor.a = oldColor.a;
            spriteRenderer.color = newColor;
        }
    }

}