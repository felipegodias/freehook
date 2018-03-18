using System;

using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;

public class UINoMoreAdsToShowScreen : MonoBehaviour {

    [SerializeField]
    private Button button;
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private UIAdScreen uiAdScreen;

    private void Awake() {
        this.button.onClick.AddListener(this.OnOkButtonClick);
    }

    public void Show() {
        this.gameObject.SetActive(true);
        this.Animate(1, () => {
            this.canvasGroup.interactable = true;
        });
    }

    public void Hide() {
        this.canvasGroup.interactable = false;
        this.Animate(0, () => {
            this.gameObject.SetActive(false);
        });
    }

    private void Animate(float alphaTo, Action callback) {
        float alphaFrom = this.canvasGroup.alpha;
        float alphaDif = alphaTo - alphaFrom;

        TweenCallback onCompleteCallback = () =>
        {
            if (callback != null)
            {
                callback.Invoke();
            }
        };

        this.canvasGroup.DOFade(alphaTo, 0.33f).OnComplete(onCompleteCallback);
    }

    private void OnOkButtonClick() {
        this.Hide();
        this.uiAdScreen.Show();
    }

}
