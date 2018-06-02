using DG.Tweening;
using DG.Tweening.Core;

using MGS.EventManager;

using UnityEngine;

public class UIInGameElements : MonoBehaviour
{

    [SerializeField]
    private CanvasGroup defaultElementsCanvasGroup;

    [SerializeField]
    private CanvasGroup speedRunElementsCanvasGroup;

    private void Awake()
    {
        EventManager.AddListener<OnSpeedRunStart>(OnSpeedRunStart);
        EventManager.AddListener<OnSpeedRunEnd>(OnSpeedRunEnd);
    }

    private void OnSpeedRunStart(object sender, OnSpeedRunStart eventArgs)
    {
        defaultElementsCanvasGroup.interactable = false;
        speedRunElementsCanvasGroup.interactable = true;

        DOGetter<float> getter = () => 0;

        DOSetter<float> setter = f =>
        {
            defaultElementsCanvasGroup.alpha = 1 - f;
            speedRunElementsCanvasGroup.alpha = f;
            defaultElementsCanvasGroup.transform.localPosition = Vector3.up * 25 * f;
            speedRunElementsCanvasGroup.transform.localPosition = Vector3.up * 25 * (1 - f);
        };

        DOTween.To(getter, setter, 1, 0.5f).SetDelay(0.75f);
    }

    private void OnSpeedRunEnd(object sender, OnSpeedRunEnd eventargs)
    {
        defaultElementsCanvasGroup.interactable = true;
        speedRunElementsCanvasGroup.interactable = false;

        DOGetter<float> getter = () => 0;

        DOSetter<float> setter = f =>
        {
            defaultElementsCanvasGroup.alpha = f;
            speedRunElementsCanvasGroup.alpha = 1 - f;
            defaultElementsCanvasGroup.transform.localPosition = Vector3.up * 25 * (1 - f);
            speedRunElementsCanvasGroup.transform.localPosition = Vector3.up * 25 * f;
        };

        DOTween.To(getter, setter, 1, 0.5f).SetDelay(0.75f);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<OnSpeedRunStart>(OnSpeedRunStart);
        EventManager.RemoveListener<OnSpeedRunEnd>(OnSpeedRunEnd);
    }

}