using System;
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
        LeanTween.value(this.gameObject, f => {
            this.canvasGroup.alpha = alphaFrom + alphaDif * f;
        }, 0, 1, 0.33f).setOnComplete(() => {
            if (callback != null) {
                callback.Invoke();
            }
        }).setEase(LeanTweenType.easeOutSine);
    }

    private void OnOkButtonClick() {
        this.Hide();
        this.uiAdScreen.Show();
    }

}
