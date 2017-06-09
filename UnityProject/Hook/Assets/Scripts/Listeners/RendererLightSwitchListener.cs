using UnityEngine;
using UnityEngine.UI;

public class RendererLightSwitchListener : LightSwitchListener {

    private SpriteRenderer[] spriteRenderers;

    protected override void Awake() {
        this.spriteRenderers = this.GetComponentsInChildren<SpriteRenderer>(true);
        base.Awake();
    }

    protected override void UpdateColors() {
        foreach (SpriteRenderer spriteRenderer in this.spriteRenderers) {
            Color oldColor = spriteRenderer.color;
            Color newColor = ColorUtils.LineColor;
            newColor.a = oldColor.a;
            spriteRenderer.color = newColor;
        }
    }

}
