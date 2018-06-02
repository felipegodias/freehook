using DG.Tweening;

using MGS.EventManager;

using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{

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

    private void Awake()
    {
        EventManager.AddListener<OnStageCompleted>(OnStageCompleted);
        EventManager.AddListener<OnStageFail>(OnStageFail);
        EventManager.AddListener<OnStageLoaded>(OnStageLoaded);
        EventManager.AddListener<OnHeartsCountWasChanged>(OnHeartsCountWasChanged);
        EventManager.AddListener<OnStageSwitch>(OnStageSwitch);

        nextStageButton.onClick.AddListener(OnNextStageButtonClicked);
        previousStageButton.onClick.AddListener(OnPreviousStageButtonClicked);
    }

    private void Start()
    {
        nextStageButton.gameObject.SetActive(false);
        previousStageButton.gameObject.SetActive(false);

        if (!Player.IsAdsEnabled())
        {
            return;
        }

        int hearts = Player.GetHearts();
        if (hearts <= 0)
        {
            adsScreen.Show();
        }
    }

    private void OnNextStageButtonClicked()
    {
        if (isSwitchStageButtonBlocked)
        {
            return;
        }

        Vector3 to = Vector3.one * 0.15f;
        nextStageButton.transform.localScale = Vector3.one;
        nextStageButton.transform.DOPunchScale(to, 0.5f, 6);

        EventManager.Dispatch(new OnStageSwitch(1));
    }

    private void OnPreviousStageButtonClicked()
    {
        if (isSwitchStageButtonBlocked)
        {
            return;
        }

        Vector3 to = Vector3.one * 0.15f;
        previousStageButton.transform.localScale = Vector3.one;
        previousStageButton.transform.DOPunchScale(to, 0.5f, 6);

        EventManager.Dispatch(new OnStageSwitch(-1));
    }

    private void OnStageSwitch(object sender, OnStageSwitch eventargs)
    {
        Color color = fade.color;
        color.a = 1;
        fade.color = color;
        fade.raycastTarget = true;
    }

    private void OnStageCompleted(object sender, OnStageCompleted onStageCompleted)
    {
        ShowFade();
    }

    private void OnStageFail(object sender, OnStageFail onStageFail)
    {
        ShowFade();
    }

    private void OnStageLoaded(object sender, OnStageLoaded onStageLoaded)
    {
        fade.raycastTarget = true;
        isSwitchStageButtonBlocked = true;

        previousStageButton.gameObject.SetActive(onStageLoaded.Stage > 0);
        nextStageButton.gameObject.SetActive(onStageLoaded.Stage != Player.GetLastStage());

        if (onStageLoaded.Stage > 0)
        {
            int stage = onStageLoaded.Stage + 1;
            stageNumberText.text = stage.ToString();

            Color to = stageNumberText.color;
            to.a = 1;
            stageNumberText.DOColor(to, 0.2f);

            to.a = 0;
            stageNumberText.DOColor(to, 0.2f).SetDelay(1.3f);

            to = fade.color;
            to.a = 0;

            TweenCallback onCompleteCallback = () =>
            {
                fade.raycastTarget = false;
                isSwitchStageButtonBlocked = false;
            };

            fade.DOColor(to, 0.25f).SetDelay(1.5f).OnComplete(onCompleteCallback);
        }
        else
        {
            Color to = fade.color;
            to.a = 0;

            TweenCallback onCompleteCallback = () =>
            {
                fade.raycastTarget = false;
                isSwitchStageButtonBlocked = false;
            };

            fade.DOColor(to, 0.5f).SetDelay(0.5f).OnComplete(onCompleteCallback);
        }
    }

    private void OnHeartsCountWasChanged(object sender, OnHeartsCountWasChanged onHeartsCountWasChanged)
    {
        if (onHeartsCountWasChanged.IsSpeedRunModeOn)
        {
            return;
        }

        if (onHeartsCountWasChanged.HeartCount <= 0)
        {
            nextStageButton.gameObject.SetActive(false);
            previousStageButton.gameObject.SetActive(false);
            adsScreen.Show();
        }
    }

    private void ShowFade()
    {
        Color to = fade.color;
        to.a = 1;

        fade.DOColor(to, 0.25f).SetDelay(0.25f);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<OnStageCompleted>(OnStageCompleted);
        EventManager.RemoveListener<OnStageFail>(OnStageFail);
        EventManager.RemoveListener<OnStageLoaded>(OnStageLoaded);
        EventManager.RemoveListener<OnHeartsCountWasChanged>(OnHeartsCountWasChanged);
        EventManager.RemoveListener<OnStageSwitch>(OnStageSwitch);
    }

}