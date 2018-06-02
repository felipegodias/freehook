using System;

using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;

public class UINoMoreAdsToShowScreen : MonoBehaviour
{

    [SerializeField]
    private Button button;

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private UIAdScreen uiAdScreen;

    private void Awake()
    {
        button.onClick.AddListener(OnOkButtonClick);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        Animate(
                1,
                () =>
                {
                    canvasGroup.interactable = true;
                });
    }

    public void Hide()
    {
        canvasGroup.interactable = false;
        Animate(
                0,
                () =>
                {
                    gameObject.SetActive(false);
                });
    }

    private void Animate(float alphaTo, Action callback)
    {
        float alphaFrom = canvasGroup.alpha;
        float alphaDif = alphaTo - alphaFrom;

        TweenCallback onCompleteCallback = () =>
        {
            if (callback != null)
            {
                callback.Invoke();
            }
        };

        canvasGroup.DOFade(alphaTo, 0.33f).OnComplete(onCompleteCallback);
    }

    private void OnOkButtonClick()
    {
        Hide();
        uiAdScreen.Show();
    }

}