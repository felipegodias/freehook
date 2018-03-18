using DG.Tweening;

using UnityEngine;
using UnityEngine.EventSystems;

public abstract class UIButton : MonoBehaviour, IPointerClickHandler {

    public void OnPointerClick(PointerEventData eventData)
    {
        this.transform.transform.localScale = Vector3.one;
        this.transform.DOPunchScale(Vector3.one * 0.15f, 0.5f, 6);
        this.OnClick();
    }

    protected abstract void OnClick();

}