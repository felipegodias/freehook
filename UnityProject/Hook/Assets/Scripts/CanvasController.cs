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

    [SerializeField]
    private Button nextStageButton;
    [SerializeField]
    private Button previousStageButton;

    private bool isSwitchStageButtonBlocked;

    private void Awake() {
        EventManager.AddListener<OnStageCompleted>(this.OnStageCompleted);
        EventManager.AddListener<OnStageFail>(this.OnStageFail);
        EventManager.AddListener<OnStageLoaded>(this.OnStageLoaded);
        EventManager.AddListener<OnHeartsCountWasChanged>(this.OnHeartsCountWasChanged);
        EventManager.AddListener<OnStageSwitch>(this.OnStageSwitch);

        this.nextStageButton.onClick.AddListener(this.OnNextStageButtonClicked);
        this.previousStageButton.onClick.AddListener(this.OnPreviousStageButtonClicked);
    }

    private void Start() {
        this.nextStageButton.gameObject.SetActive(false);
        this.previousStageButton.gameObject.SetActive(false);

        int hearts = Player.GetHearts();
        if (hearts <= 0) {
            this.adsScreen.Show();
        }
    }



    private void OnNextStageButtonClicked() {
        if (this.isSwitchStageButtonBlocked) {
            return;
        }

        LeanTween.value(this.nextStageButton.gameObject, f => {
            this.nextStageButton.transform.localScale = Vector3.one + (Vector3.one * f) * 0.1f;
        }, 0, 1, 0.5f).setEase(LeanTweenType.punch); ;


        EventManager.Dispatch(new OnStageSwitch(1));
    }

    private void OnPreviousStageButtonClicked() {
        if (this.isSwitchStageButtonBlocked) {
            return;
        }

        LeanTween.value(this.previousStageButton.gameObject, f => {
            this.previousStageButton.transform.localScale = Vector3.one + (Vector3.one * f) * 0.1f;
        }, 0, 1, 0.5f).setEase(LeanTweenType.punch); ;

        EventManager.Dispatch(new OnStageSwitch(-1));
    }

    private void OnStageSwitch(object sender, OnStageSwitch eventargs) {
        Color color = this.fade.color;
        color.a = 1;
        this.fade.color = color;
        this.fade.raycastTarget = true;
    }

    private void OnStageCompleted(object sender, OnStageCompleted onStageCompleted) {
        this.ShowFade();
    }

    private void OnStageFail(object sender, OnStageFail onStageFail) {
        this.ShowFade();
    }

    private void OnStageLoaded(object sender, OnStageLoaded onStageLoaded) {
        this.fade.raycastTarget = true;
        this.isSwitchStageButtonBlocked = true;

        this.previousStageButton.gameObject.SetActive(onStageLoaded.Stage != 0);
        this.nextStageButton.gameObject.SetActive(onStageLoaded.Stage != Player.GetLastStage());

        if (onStageLoaded.Stage > 0) {
            int stage = onStageLoaded.Stage + 1;
            this.stageNumberText.text = stage.ToString();
            LeanTween.value(this.gameObject, f => {
                Color color = this.stageNumberText.color;
                color.a = f;
                this.stageNumberText.color = color;
            }, 0, 1, 0.2f).setDelay(0).setEase(LeanTweenType.easeOutSine);
            LeanTween.value(this.gameObject, f => {
                Color color = this.stageNumberText.color;
                color.a = 1 - f;
                this.stageNumberText.color = color;
            }, 0, 1, 0.2f).setDelay(1.3f).setEase(LeanTweenType.easeOutSine);
            LeanTween.value(this.gameObject, f => {
                Color color = this.fade.color;
                color.a = 1 - f;
                this.fade.color = color;
            }, 0, 1, 0.25f).setDelay(1.5f).setEase(LeanTweenType.easeOutSine).setOnComplete(() => {
                this.fade.raycastTarget = false;
                this.isSwitchStageButtonBlocked = false;
            });
        } else {
            LeanTween.value(this.gameObject, f => {
                Color color = this.fade.color;
                color.a = 1 - f;
                this.fade.color = color;
            }, 0, 1, 0.5f).setDelay(0.5f).setEase(LeanTweenType.easeOutSine).setOnComplete(() => {
                this.fade.raycastTarget = false;
                this.isSwitchStageButtonBlocked = false;
            });
        }
    }

    private void OnHeartsCountWasChanged(object sender, OnHeartsCountWasChanged onHeartsCountWasChanged) {
        if (onHeartsCountWasChanged.HeartCount <= 0) {
            this.nextStageButton.gameObject.SetActive(false);
            this.previousStageButton.gameObject.SetActive(false);
            this.adsScreen.Show();
        }
    }

    private void ShowFade() {
        LeanTween.value(this.gameObject, f => {
            Color color = this.fade.color;
            color.a = f;
            this.fade.color = color;
        }, 0, 1, 0.25f).setDelay(0.25f).setEase(LeanTweenType.easeOutSine);
    }

    private void OnDestroy() {
        EventManager.RemoveListener<OnStageCompleted>(this.OnStageCompleted);
        EventManager.RemoveListener<OnStageFail>(this.OnStageFail);
        EventManager.RemoveListener<OnStageLoaded>(this.OnStageLoaded);
        EventManager.RemoveListener<OnHeartsCountWasChanged>(this.OnHeartsCountWasChanged);
        EventManager.RemoveListener<OnStageSwitch>(this.OnStageSwitch);
    }

}
