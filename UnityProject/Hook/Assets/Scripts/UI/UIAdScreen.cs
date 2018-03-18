using DG.Tweening;

using MGS.EventManager;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class UIAdScreen : MonoBehaviour {

    [SerializeField]
    private Button watchAdsButton;
    [SerializeField]
    private Button removeAdsButton;
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private TextMeshProUGUI descriptionText;
    [SerializeField]
    private UINoMoreAdsToShowScreen noMoreAdsToShowScreen;

    private Tweener tweener;

    public void Show() {
        OnShowAdsScreen onShowAdsScreen = new OnShowAdsScreen();
        EventManager.Dispatch(onShowAdsScreen);
        this.canvasGroup.alpha = 0;
        this.canvasGroup.interactable = false;
        this.gameObject.SetActive(true);

        TweenCallback onCompleteCallback = () => {
            this.canvasGroup.interactable = true;
        };

        this.tweener = this.canvasGroup.DOFade(1, 0.33f).SetDelay(1).OnComplete(onCompleteCallback);
    }

    public void Hide() {
        this.gameObject.SetActive(false);
    }

    private void Awake() {
        this.watchAdsButton.onClick.AddListener(this.OnWatchAdsButtonClick);
        this.removeAdsButton.onClick.AddListener(this.OnRemoveAdsButtonClick);
        EventManager.AddListener<OnRemoveAdsBought>(this.OnRemoveAdsBought);
        EventManager.AddListener<OnProcessPurchaseStart>(this.OnProcessPurchaseStart);
        EventManager.AddListener<OnProcessPurchaseFinish>(this.OnProcessPurchaseFinish);
    }

    private void OnDestroy() {
        EventManager.RemoveListener<OnRemoveAdsBought>(this.OnRemoveAdsBought);
        EventManager.RemoveListener<OnProcessPurchaseStart>(this.OnProcessPurchaseStart);
        EventManager.RemoveListener<OnProcessPurchaseFinish>(this.OnProcessPurchaseFinish);
    }

    private void OnRemoveAdsBought(object sender, OnRemoveAdsBought eventargs) {
        this.Hide();
        EventManager.Dispatch(new OnWatchAdsCompleted(ShowResult.Skipped));
    }

    private void OnProcessPurchaseFinish(object sender, OnProcessPurchaseFinish eventargs) {
        if (!this.gameObject.activeInHierarchy) {
            return;
        }

        if (this.tweener != null) {
            this.tweener.Kill();
            this.tweener = null;
        }
        this.Show();
    }

    private void OnProcessPurchaseStart(object sender, OnProcessPurchaseStart eventargs) {
        if (this.tweener != null)
        {
            this.tweener.Kill();
            this.tweener = null;
        }

        this.canvasGroup.interactable = false;

        this.tweener = this.canvasGroup.DOFade(0, 0.5f);
    }

    private void Start() {
        int maxHearts = GameSettings.MAX_HEARTS;
        string description = I18N.GetText("ads_screen_description", maxHearts);
        this.descriptionText.text = description;
    }

    private void OnWatchAdsButtonClick() {
        if (Advertisement.IsReady("rewardedVideo")) {
            ShowOptions showOptions = new ShowOptions();
            showOptions.resultCallback = result => {
                int maxHearts = GameSettings.MAX_HEARTS;
                Player.SetHearts(maxHearts);
                OnHeartsCountWasChanged onHeartsCountWasChanged = new OnHeartsCountWasChanged(maxHearts, false);
                EventManager.Dispatch(onHeartsCountWasChanged);
                EventManager.Dispatch(new OnWatchAdsCompleted(result));
            };
            OnWatchAdsStarted onWatchAdsStarted = new OnWatchAdsStarted();
            EventManager.Dispatch(onWatchAdsStarted);
            Advertisement.Show("rewardedVideo", showOptions);
        } else {
            this.noMoreAdsToShowScreen.Show();
        }
        this.Hide();
    }

    private void OnRemoveAdsButtonClick() {
        EventManager.Dispatch(new OnRemoveAdsButtonClicked());
    }

}
