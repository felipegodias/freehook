using DG.Tweening;

using MGS.EventManager;

using TMPro;

using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class UIAdScreen : MonoBehaviour
{

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

    public void Show()
    {
        var onShowAdsScreen = new OnShowAdsScreen();
        EventManager.Dispatch(onShowAdsScreen);
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        gameObject.SetActive(true);

        TweenCallback onCompleteCallback = () =>
        {
            canvasGroup.interactable = true;
        };

        tweener = canvasGroup.DOFade(1, 0.33f).SetDelay(1).OnComplete(onCompleteCallback);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        watchAdsButton.onClick.AddListener(OnWatchAdsButtonClick);
        removeAdsButton.onClick.AddListener(OnRemoveAdsButtonClick);
        EventManager.AddListener<OnRemoveAdsBought>(OnRemoveAdsBought);
        EventManager.AddListener<OnProcessPurchaseStart>(OnProcessPurchaseStart);
        EventManager.AddListener<OnProcessPurchaseFinish>(OnProcessPurchaseFinish);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<OnRemoveAdsBought>(OnRemoveAdsBought);
        EventManager.RemoveListener<OnProcessPurchaseStart>(OnProcessPurchaseStart);
        EventManager.RemoveListener<OnProcessPurchaseFinish>(OnProcessPurchaseFinish);
    }

    private void OnRemoveAdsBought(object sender, OnRemoveAdsBought eventargs)
    {
        Hide();
        EventManager.Dispatch(new OnWatchAdsCompleted(ShowResult.Skipped));
    }

    private void OnProcessPurchaseFinish(object sender, OnProcessPurchaseFinish eventargs)
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }

        if (tweener != null)
        {
            tweener.Kill();
            tweener = null;
        }

        Show();
    }

    private void OnProcessPurchaseStart(object sender, OnProcessPurchaseStart eventargs)
    {
        if (tweener != null)
        {
            tweener.Kill();
            tweener = null;
        }

        canvasGroup.interactable = false;

        tweener = canvasGroup.DOFade(0, 0.5f);
    }

    private void Start()
    {
        int maxHearts = GameSettings.MAX_HEARTS;
        string description = I18N.GetText("ads_screen_description", maxHearts);
        descriptionText.text = description;
    }

    private void OnWatchAdsButtonClick()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var showOptions = new ShowOptions();
            showOptions.resultCallback = result =>
            {
                int maxHearts = GameSettings.MAX_HEARTS;
                Player.SetHearts(maxHearts);
                var onHeartsCountWasChanged = new OnHeartsCountWasChanged(maxHearts, false);
                EventManager.Dispatch(onHeartsCountWasChanged);
                EventManager.Dispatch(new OnWatchAdsCompleted(result));
            };
            var onWatchAdsStarted = new OnWatchAdsStarted();
            EventManager.Dispatch(onWatchAdsStarted);
            Advertisement.Show("rewardedVideo", showOptions);
        }
        else
        {
            noMoreAdsToShowScreen.Show();
        }

        Hide();
    }

    private void OnRemoveAdsButtonClick()
    {
        EventManager.Dispatch(new OnRemoveAdsButtonClicked());
    }

}