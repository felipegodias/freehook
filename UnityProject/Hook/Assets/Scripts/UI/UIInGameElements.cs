using MGS.EventManager;
using UnityEngine;

public class UIInGameElements : MonoBehaviour {

    [SerializeField]
    private CanvasGroup defaultElementsCanvasGroup;
    [SerializeField]
    private CanvasGroup speedRunElementsCanvasGroup;

    private void Awake() {
        EventManager.AddListener<OnSpeedRunStart>(this.OnSpeedRunStart);
        EventManager.AddListener<OnSpeedRunEnd>(this.OnSpeedRunEnd);
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

    private void OnSpeedRunEnd(object sender, OnSpeedRunEnd eventargs) {
        this.defaultElementsCanvasGroup.interactable = true;
        this.speedRunElementsCanvasGroup.interactable = false;
        LeanTween.value(this.gameObject, f => {
            this.defaultElementsCanvasGroup.alpha = f;
            this.speedRunElementsCanvasGroup.alpha = 1 - f;
            this.defaultElementsCanvasGroup.transform.localPosition = Vector3.up * 25 * (1 - f);
            this.speedRunElementsCanvasGroup.transform.localPosition = Vector3.up * 25 * f;
        }, 0, 1, 0.5f).setEaseOutSine();
    }

    private void OnDestroy() {
        EventManager.RemoveListener<OnSpeedRunStart>(this.OnSpeedRunStart);
        EventManager.RemoveListener<OnSpeedRunEnd>(this.OnSpeedRunEnd);
    }

}
