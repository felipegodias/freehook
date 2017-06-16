using MGS.EventManager;
using UnityEngine;

public class UIHearts : MonoBehaviour {

    [SerializeField]
    private CanvasGroup canvasGroup;
    private UIHeart[] uiHearts;

    private void Awake() {
        if (!Player.IsAdsEnabled()) {
            this.gameObject.SetActive(false);
            return;
        }

        UIHeart uiHeart = this.GetComponentInChildren<UIHeart>();
        int maxHearts = GameSettings.MAX_HEARTS;
        for (int i = 0; i < maxHearts - 1; i++) {
            Instantiate(uiHeart, this.transform, false);
        }

        this.uiHearts = this.GetComponentsInChildren<UIHeart>();
        EventManager.AddListener<OnHeartsCountWasChanged>(this.OnHeartsCountWasChanged);
        EventManager.AddListener<OnRemoveAdsBought>(this.OnRemoveAdsBought);
    }



    private void Start() {
        if (Player.IsAdsEnabled()) {
            this.UpdateHeartCount(Player.GetHearts());
        }
    }

    private void UpdateHeartCount(int heartCount) {
        int maxHearts = GameSettings.MAX_HEARTS;
        int hearts = maxHearts - heartCount;
        for (int i = 0; i < this.uiHearts.Length; i++) {
            if (i >= hearts) {
                this.uiHearts[i].SetFill();
            } else {
                this.uiHearts[i].SetEmpty();
            }
        }
    }

    private void OnHeartsCountWasChanged(object sender, OnHeartsCountWasChanged onHeartsCountWasChanged) {
        if (Player.IsAdsEnabled()) {
            this.UpdateHeartCount(onHeartsCountWasChanged.HeartCount);
        }
    }

    private void OnRemoveAdsBought(object sender, OnRemoveAdsBought eventargs) {
        Vector3 from = this.canvasGroup.transform.localPosition;
        Vector3 to = from + Vector3.up * 25;
        Vector3 dif = to - from;
        LeanTween.value(this.gameObject, f => {
            this.canvasGroup.alpha = 1 - f;
            this.canvasGroup.transform.localPosition = from + dif * f;
        }, 0, 1, 0.5f).setOnComplete(() => {
            this.gameObject.SetActive(false);
        }).setEaseOutSine();
    }

    private void OnDestroy() {
        EventManager.RemoveListener<OnHeartsCountWasChanged>(this.OnHeartsCountWasChanged);
        EventManager.RemoveListener<OnRemoveAdsBought>(this.OnRemoveAdsBought);
    }

}
