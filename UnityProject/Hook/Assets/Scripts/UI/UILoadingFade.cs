using DG.Tweening;

using UnityEngine;

public class UILoadingFade : MonoBehaviour
{

    private void Start()
    {
        TweenCallback onCompleteCallback = () =>
        {
            Destroy(gameObject);
        };

        var canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.DOFade(0, 1).SetDelay(0.25f).OnComplete(onCompleteCallback);
    }

}