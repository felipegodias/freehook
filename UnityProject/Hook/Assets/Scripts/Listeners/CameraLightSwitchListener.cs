using UnityEngine;

public class CameraLightSwitchListener : LightSwitchListener {

    private new Camera camera;

    protected override void Awake() {
        this.camera = this.GetComponent<Camera>();
        base.Awake();
    }

    protected override void UpdateColors() {
        this.camera.backgroundColor = ColorUtils.BackgroundColor;
    }

}
