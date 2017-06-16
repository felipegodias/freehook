using MGS.EventManager;
using UnityEngine;

public class UIMenu : MonoBehaviour {

    [SerializeField]
    private CanvasGroup canvasGroup;

    private void Awake() {
        EventManager.AddListener<OnSpeedRunStart>(this.OnSpeedRunStart);
    }

    private void OnSpeedRunStart(object sender, OnSpeedRunStart eventArgs) {
        this.canvasGroup.interactable = false;
        LeanTween.value(this.gameObject, f => {
            this.canvasGroup.alpha = 1 - f;
            this.canvasGroup.transform.localPosition = Vector3.down * 25 * f;
        }, 0, 1, 0.5f).setEaseOutSine();
    }

    private void OnDestroy() {
        EventManager.RemoveListener<OnSpeedRunStart>(this.OnSpeedRunStart);
    }
}
