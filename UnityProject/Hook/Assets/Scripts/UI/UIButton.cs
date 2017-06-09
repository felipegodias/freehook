using UnityEngine;
using UnityEngine.EventSystems;

public abstract class UIButton : MonoBehaviour, IPointerClickHandler {

    public void OnPointerClick(PointerEventData eventData) {
        LeanTween.value(this.gameObject, f => { this.transform.localScale = Vector3.one + Vector3.one * f * 0.1f; }, 0,
            1, 0.5f).setEase(LeanTweenType.punch);
        this.OnClick();
    }

    protected abstract void OnClick();

}