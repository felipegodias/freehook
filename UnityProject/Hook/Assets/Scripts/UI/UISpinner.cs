using DG.Tweening;
using DG.Tweening.Core;

using MGS.EventManager;
using TMPro;
using UnityEngine;

public class UISpinner : MonoBehaviour {

    [SerializeField]
    private float rps;
    [SerializeField]
    private CanvasGroup spinner;
    private float rotation;

    private Tweener tweener;

    private void Awake() {
        EventManager.AddListener<OnProcessPurchaseStart>(this.OnProcessPurchaseStart);
        EventManager.AddListener<OnProcessPurchaseFinish>(this.OnProcessPurchaseFinish);
    }

    private void Update() {
        this.rotation += this.rps * Time.deltaTime;
        this.transform.localRotation = Quaternion.Euler(0, 0, this.rotation);
    }

    private void OnProcessPurchaseFinish(object sender, OnProcessPurchaseFinish eventargs) {
        this.Animate(0, Vector3.zero);
    }

    private void OnProcessPurchaseStart(object sender, OnProcessPurchaseStart eventargs) {
        this.Animate(1, Vector3.one);
    }

    private void Animate(float alpha, Vector3 scale) {
        Vector3 scaleFrom = this.transform.localScale;
        Vector3 scaleDif = scale - scaleFrom;
        float alphaFrom = this.spinner.alpha;
        float alphaDif = alpha - alphaFrom;

        if (this.tweener != null)
        {
            this.tweener.Kill();
            this.tweener = null;
        }

        DOGetter<float> getter = () => 0;

        DOSetter<float> setter = f =>
        {
            this.transform.localScale = scaleFrom + scaleDif * f;
            this.spinner.alpha = alphaFrom + alphaDif * f;
        };

        this.tweener = DOTween.To(getter, setter, 1, 0.5f);
    }

    private void OnDestroy() {
        EventManager.RemoveListener<OnProcessPurchaseStart>(this.OnProcessPurchaseStart);
        EventManager.RemoveListener<OnProcessPurchaseFinish>(this.OnProcessPurchaseFinish);
    }
}
