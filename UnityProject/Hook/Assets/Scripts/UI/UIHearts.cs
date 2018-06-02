using DG.Tweening;

using MGS.EventManager;

using UnityEngine;

public class UIHearts : MonoBehaviour
{

    [SerializeField]
    private CanvasGroup canvasGroup;

    private UIHeart[] uiHearts;

    private void Awake()
    {
        if (!Player.IsAdsEnabled())
        {
            gameObject.SetActive(false);
            return;
        }

        var uiHeart = GetComponentInChildren<UIHeart>();
        int maxHearts = GameSettings.MAX_HEARTS;
        for (int i = 0; i < maxHearts - 1; i++)
        {
            Instantiate(uiHeart, transform, false);
        }

        uiHearts = GetComponentsInChildren<UIHeart>();
        EventManager.AddListener<OnHeartsCountWasChanged>(OnHeartsCountWasChanged);
        EventManager.AddListener<OnRemoveAdsBought>(OnRemoveAdsBought);
    }

    private void Start()
    {
        if (Player.IsAdsEnabled())
        {
            UpdateHeartCount(Player.GetHearts());
        }
    }

    private void UpdateHeartCount(int heartCount)
    {
        int maxHearts = GameSettings.MAX_HEARTS;
        int hearts = maxHearts - heartCount;
        for (int i = 0; i < uiHearts.Length; i++)
        {
            if (i >= hearts)
            {
                uiHearts[i].SetFill();
            }
            else
            {
                uiHearts[i].SetEmpty();
            }
        }
    }

    private void OnHeartsCountWasChanged(object sender, OnHeartsCountWasChanged onHeartsCountWasChanged)
    {
        if (Player.IsAdsEnabled())
        {
            UpdateHeartCount(onHeartsCountWasChanged.HeartCount);
        }
    }

    private void OnRemoveAdsBought(object sender, OnRemoveAdsBought eventargs)
    {
        Vector3 from = canvasGroup.transform.localPosition;
        Vector3 to = from + Vector3.up * 25;

        TweenCallback onCompleteCallback = () =>
        {
            gameObject.SetActive(false);
        };

        canvasGroup.DOFade(0, 0.5f);
        canvasGroup.transform.DOLocalMove(to, 0.5f).OnComplete(onCompleteCallback);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<OnHeartsCountWasChanged>(OnHeartsCountWasChanged);
        EventManager.RemoveListener<OnRemoveAdsBought>(OnRemoveAdsBought);
    }

}