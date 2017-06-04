using MGS.EventManager;
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

    private void OnWatchAdsButtonClick() {
        if (Advertisement.IsReady()) {
            ShowOptions showOptions = new ShowOptions();
            showOptions.resultCallback = result => {
                Player.SetHearts(5);
                OnHeartsCountWasChanged onHeartsCountWasChanged = new OnHeartsCountWasChanged(5);
                EventManager.Dispatch(onHeartsCountWasChanged);
                EventManager.Dispatch(new OnWatchAdsCompleted(result));
            };
            Advertisement.Show(showOptions);
        } else {
            Player.SetHearts(5);
            OnHeartsCountWasChanged onHeartsCountWasChanged = new OnHeartsCountWasChanged(5);
            EventManager.Dispatch(onHeartsCountWasChanged);
            EventManager.Dispatch(new OnWatchAdsCompleted(ShowResult.Failed));
        }
        this.Hide();
    }

    private void OnRemoveAdsButtonClick() {

    }

}
