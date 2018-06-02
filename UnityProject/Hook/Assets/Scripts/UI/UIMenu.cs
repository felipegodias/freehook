using DG.Tweening;

using MGS.EventManager;

using UnityEngine;

public class UIMenu : MonoBehaviour
{

    [SerializeField]
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        EventManager.AddListener<OnSpeedRunStart>(OnSpeedRunStart);
    }

    private void OnSpeedRunStart(object sender, OnSpeedRunStart eventArgs)
    {
        canvasGroup.interactable = false;

        canvasGroup.DOFade(0, 0.5f);
        canvasGroup.transform.DOLocalMove(Vector3.down * 25, 0.5f);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<OnSpeedRunStart>(OnSpeedRunStart);
    }

}