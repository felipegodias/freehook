using DG.Tweening;

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

        if (!Player.IsAdsEnabled()) {
            return;
        }

        int hearts = Player.GetHearts();
        if (hearts <= 0) {
            this.adsScreen.Show();
        }
    }

    private void OnNextStageButtonClicked() {
        if (this.isSwitchStageButtonBlocked) {
            return;
        }

        Vector3 to = Vector3.one * 0.15f;
        this.nextStageButton.transform.localScale = Vector3.one;
        this.nextStageButton.transform.DOPunchScale(to, 0.5f, 6);

        EventManager.Dispatch(new OnStageSwitch(1));
    }

    private void OnPreviousStageButtonClicked() {
        if (this.isSwitchStageButtonBlocked) {
            return;
        }

        Vector3 to = Vector3.one * 0.15f;
        this.previousStageButton.transform.localScale = Vector3.one;
        this.previousStageButton.transform.DOPunchScale(to, 0.5f, 6);

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

        this.previousStageButton.gameObject.SetActive(onStageLoaded.Stage > 0);
        this.nextStageButton.gameObject.SetActive(onStageLoaded.Stage != Player.GetLastStage());

        if (onStageLoaded.Stage > 0) {
            int stage = onStageLoaded.Stage + 1;
            this.stageNumberText.text = stage.ToString();

            Color to = this.stageNumberText.color;
            to.a = 1;
            this.stageNumberText.DOColor(to, 0.2f);

            to.a = 0;
            this.stageNumberText.DOColor(to, 0.2f).SetDelay(1.3f);

            to = fade.color;
            to.a = 0;

            TweenCallback onCompleteCallback = () =>
            {
                this.fade.raycastTarget = false;
                this.isSwitchStageButtonBlocked = false;
            };

            fade.DOColor(to, 0.25f).SetDelay(1.5f).OnComplete(onCompleteCallback);
        } else {
            Color to = fade.color;
            to.a = 0;

            TweenCallback onCompleteCallback = () =>
            {
                this.fade.raycastTarget = false;
                this.isSwitchStageButtonBlocked = false;
            };

            fade.DOColor(to, 0.5f).SetDelay(0.5f).OnComplete(onCompleteCallback);
        }
    }

    private void OnHeartsCountWasChanged(object sender, OnHeartsCountWasChanged onHeartsCountWasChanged) {
        if (onHeartsCountWasChanged.IsSpeedRunModeOn) {
            return;
        }
        if (onHeartsCountWasChanged.HeartCount <= 0) {
            this.nextStageButton.gameObject.SetActive(false);
            this.previousStageButton.gameObject.SetActive(false);
            this.adsScreen.Show();
        }
    }

    private void ShowFade() {
        Color to = fade.color;
        to.a = 1;

        fade.DOColor(to, 0.25f).SetDelay(0.25f);
    }

    private void OnDestroy() {
        EventManager.RemoveListener<OnStageCompleted>(this.OnStageCompleted);
        EventManager.RemoveListener<OnStageFail>(this.OnStageFail);
        EventManager.RemoveListener<OnStageLoaded>(this.OnStageLoaded);
        EventManager.RemoveListener<OnHeartsCountWasChanged>(this.OnHeartsCountWasChanged);
        EventManager.RemoveListener<OnStageSwitch>(this.OnStageSwitch);
    }

}
