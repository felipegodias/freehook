using MGS.EventManager;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class UIAdScreen : MonoBehaviour {

    [SerializeField]
    private Button watchAdsButton;
    [SerializeField]
    private Button removeAdsButton;

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
                EventManager.Dispatch(new OnWatchAdsCompleted());
            };
            Advertisement.Show(showOptions);
        } else {
            Player.SetHearts(5);
            OnHeartsCountWasChanged onHeartsCountWasChanged = new OnHeartsCountWasChanged(5);
            EventManager.Dispatch(onHeartsCountWasChanged);
            EventManager.Dispatch(new OnWatchAdsCompleted());
        }
        this.gameObject.SetActive(false);
    }

    private void OnRemoveAdsButtonClick() {

    }

}
