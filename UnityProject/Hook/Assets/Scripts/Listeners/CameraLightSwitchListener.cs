using UnityEngine;

public class CameraLightSwitchListener : LightSwitchListener
{

    private new Camera camera;

    protected override void Awake()
    {
        camera = GetComponent<Camera>();
        base.Awake();
    }

    protected override void UpdateColors()
    {
        camera.backgroundColor = ColorUtils.BackgroundColor;
    }

}