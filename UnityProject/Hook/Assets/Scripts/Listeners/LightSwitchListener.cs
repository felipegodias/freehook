using MGS.EventManager;

using UnityEngine;

public abstract class LightSwitchListener : MonoBehaviour
{

    protected virtual void Awake()
    {
        EventManager.AddListener<OnLightSwitch>(OnLightSwitch);
    }

    private void Start()
    {
        UpdateColors();
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<OnLightSwitch>(OnLightSwitch);
    }

    private void OnLightSwitch(object sender, OnLightSwitch eventargs)
    {
        UpdateColors();
    }

    protected abstract void UpdateColors();

}