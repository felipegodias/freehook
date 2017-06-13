using MGS.EventManager;
using UnityEngine;

public class UIInGameElements : MonoBehaviour {

    [SerializeField]
    private CanvasGroup defaultElementsCanvasGroup;
    [SerializeField]
    private CanvasGroup speedRunElementsCanvasGroup;

    private void Awake() {
        EventManager.AddListener<OnSpeedRunStart>(this.OnSpeedRunStart);
    }

    private void OnSpeedRunStart(object sender, OnSpeedRunStart eventArgs) {
        this.defaultElementsCanvasGroup.interactable = false;
        this.speedRunElementsCanvasGroup.interactable = true;
        LeanTween.value(this.gameObject, f => {
            this.defaultElementsCanvasGroup.alpha = 1 - f;
            this.speedRunElementsCanvasGroup.alpha = f;
            this.defaultElementsCanvasGroup.transform.localPosition = Vector3.up * 25 * f;
            this.speedRunElementsCanvasGroup.transform.localPosition = Vector3.up * 25 * (1 - f);
        }, 0, 1, 0.5f).setEaseOutSine();
    }

    private void OnDestroy() {
        EventManager.RemoveListener<OnSpeedRunStart>(this.OnSpeedRunStart);
    }

}
