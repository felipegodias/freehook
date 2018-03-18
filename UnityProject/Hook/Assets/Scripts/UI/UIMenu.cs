using DG.Tweening;

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

        this.canvasGroup.DOFade(0, 0.5f);
        this.canvasGroup.transform.DOLocalMove(Vector3.down * 25, 0.5f);
    }

    private void OnDestroy() {
        EventManager.RemoveListener<OnSpeedRunStart>(this.OnSpeedRunStart);
    }
}
