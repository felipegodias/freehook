using DG.Tweening;
using DG.Tweening.Core;

using MGS.EventManager;

using UnityEngine;

public class UISpinner : MonoBehaviour
{

    [SerializeField]
    private float rps;

    [SerializeField]
    private CanvasGroup spinner;

    private float rotation;

    private Tweener tweener;

    private void Awake()
    {
        EventManager.AddListener<OnProcessPurchaseStart>(OnProcessPurchaseStart);
        EventManager.AddListener<OnProcessPurchaseFinish>(OnProcessPurchaseFinish);
    }

    private void Update()
    {
        rotation += rps * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, 0, rotation);
    }

    private void OnProcessPurchaseFinish(object sender, OnProcessPurchaseFinish eventargs)
    {
        Animate(0, Vector3.zero);
    }

    private void OnProcessPurchaseStart(object sender, OnProcessPurchaseStart eventargs)
    {
        Animate(1, Vector3.one);
    }

    private void Animate(float alpha, Vector3 scale)
    {
        Vector3 scaleFrom = transform.localScale;
        Vector3 scaleDif = scale - scaleFrom;
        float alphaFrom = spinner.alpha;
        float alphaDif = alpha - alphaFrom;

        if (tweener != null)
        {
            tweener.Kill();
            tweener = null;
        }

        DOGetter<float> getter = () => 0;

        DOSetter<float> setter = f =>
        {
            transform.localScale = scaleFrom + scaleDif * f;
            spinner.alpha = alphaFrom + alphaDif * f;
        };

        tweener = DOTween.To(getter, setter, 1, 0.5f);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<OnProcessPurchaseStart>(OnProcessPurchaseStart);
        EventManager.RemoveListener<OnProcessPurchaseFinish>(OnProcessPurchaseFinish);
    }

}