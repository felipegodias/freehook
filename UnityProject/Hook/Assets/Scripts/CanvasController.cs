using MGS.EventManager;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

    [SerializeField]
    private Image fade;

    [SerializeField]
    private Text stageNumberText;

    [SerializeField]
    private UIAdScreen adsScreen;

    private void Awake() {
        EventManager.AddListener<OnStageCompleted>(this.OnStageCompleted);
        EventManager.AddListener<OnStageFail>(this.OnStageFail);
        EventManager.AddListener<OnStageLoaded>(this.OnStageLoaded);
        EventManager.AddListener<OnHeartsCountWasChanged>(this.OnHeartsCountWasChanged);

    }

    private void OnStageCompleted(object sender, OnStageCompleted onStageCompleted) {
        this.ShowFade();
    }

    private void OnStageFail(object sender, OnStageFail onStageFail) {
        this.ShowFade();
    }

    private void OnStageLoaded(object sender, OnStageLoaded onStageLoaded) {
        this.fade.raycastTarget = true;
        if (onStageLoaded.Stage > 0) {
            int stage = onStageLoaded.Stage + 1;
            this.stageNumberText.text = stage.ToString();
            LeanTween.value(this.gameObject, f => {
                Color color = this.stageNumberText.color;
                color.a = f;
                this.stageNumberText.color = color;
            }, 0, 1, 0.5f).setDelay(0).setEase(LeanTweenType.easeOutSine);
            LeanTween.value(this.gameObject, f => {
                Color color = this.stageNumberText.color;
                color.a = 1 - f;
                this.stageNumberText.color = color;
            }, 0, 1, 0.5f).setDelay(1.5f).setEase(LeanTweenType.easeOutSine);
            LeanTween.value(this.gameObject, f => {
                Color color = this.fade.color;
                color.a = 1 - f;
                this.fade.color = color;
            }, 0, 1, 0.5f).setDelay(2).setEase(LeanTweenType.easeOutSine).setOnComplete(() => {
                this.fade.raycastTarget = false;
            });
        } else {
            LeanTween.value(this.gameObject, f => {
                Color color = this.fade.color;
                color.a = 1 - f;
                this.fade.color = color;
            }, 0, 1, 0.5f).setDelay(0.5f).setEase(LeanTweenType.easeOutSine).setOnComplete(() => {
                this.fade.raycastTarget = false;
            });
        }
    }

    private void OnHeartsCountWasChanged(object sender, OnHeartsCountWasChanged onHeartsCountWasChanged) {
        if (onHeartsCountWasChanged.HeartCount == 0) {
            this.adsScreen.Show();
        }
    }

    private void ShowFade() {
        LeanTween.value(this.gameObject, f => {
            Color color = this.fade.color;
            color.a = f;
            this.fade.color = color;
        }, 0, 1, 0.5f).setDelay(0.5f).setEase(LeanTweenType.easeOutSine);
    }

    private void OnDestroy() {
        EventManager.RemoveListener<OnStageCompleted>(this.OnStageCompleted);
        EventManager.RemoveListener<OnStageFail>(this.OnStageFail);
    }

}
