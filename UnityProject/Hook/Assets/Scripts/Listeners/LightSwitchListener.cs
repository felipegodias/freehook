using MGS.EventManager;
using UnityEngine;

public abstract class LightSwitchListener : MonoBehaviour {

    protected virtual void Awake() {
        EventManager.AddListener<OnLightSwitch>(this.OnLightSwitch);
    }

    private void Start() {
        this.UpdateColors();
    }

    private void OnDestroy() {
        EventManager.RemoveListener<OnLightSwitch>(this.OnLightSwitch);
    }

    private void OnLightSwitch(object sender, OnLightSwitch eventargs) {
        this.UpdateColors();
    }

    protected abstract void UpdateColors();

}
