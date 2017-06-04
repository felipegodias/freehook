using MGS.EventManager;
using UnityEngine;

public class UIHearts : MonoBehaviour {

    private UIHeart[] uiHearts;

    private void Awake() {
        UIHeart uiHeart = this.GetComponentInChildren<UIHeart>();
        int maxHearts = GameSettings.MAX_HEARTS;
        for (int i = 0; i < maxHearts - 1; i++) {
            Instantiate(uiHeart, this.transform, false);
        }

        this.uiHearts = this.GetComponentsInChildren<UIHeart>();
        EventManager.AddListener<OnHeartsCountWasChanged>(this.OnHeartsCountWasChanged);
    }

    private void Start() {
        this.UpdateHeartCount(Player.GetHearts());
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
        this.UpdateHeartCount(onHeartsCountWasChanged.HeartCount);
    }

    private void OnDestroy() {
        EventManager.RemoveListener<OnHeartsCountWasChanged>(this.OnHeartsCountWasChanged);
    }

}
