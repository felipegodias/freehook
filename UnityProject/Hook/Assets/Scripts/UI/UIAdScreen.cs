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

    public void Show() {
        this.canvasGroup.alpha = 0;
        this.canvasGroup.interactable = false;
        this.gameObject.SetActive(true);
        LeanTween.value(this.gameObject, f => {
            this.canvasGroup.alpha = f;
            this.canvasGroup.interactable = true;
        }, 0, 1, 0.33f).setDelay(1).setEase(LeanTweenType.easeOutSine);
    }

    public void Hide() {
        this.gameObject.SetActive(false);
    }

    private void Awake() {
        this.watchAdsButton.onClick.AddListener(this.OnWatchAdsButtonClick);
        this.removeAdsButton.onClick.AddListener(this.OnRemoveAdsButtonClick);
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
            Advertisement.Show("rewardedVideo", showOptions);
        } else {
            int maxHearts = GameSettings.MAX_HEARTS;
            Player.SetHearts(maxHearts);
            OnHeartsCountWasChanged onHeartsCountWasChanged = new OnHeartsCountWasChanged(maxHearts, false);
            EventManager.Dispatch(onHeartsCountWasChanged);
            EventManager.Dispatch(new OnWatchAdsCompleted(ShowResult.Failed));
        }
        this.Hide();
    }

    private void OnRemoveAdsButtonClick() {

    }

}
