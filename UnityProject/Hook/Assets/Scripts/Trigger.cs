using UnityEngine;
using UnityEngine.EventSystems;

public class Trigger : GameElement, IPointerClickHandler {

    public void OnPointerClick(PointerEventData eventData) {
        LeanTween.value(this.gameObject, f => {
            this.transform.localScale = Vector3.one + (Vector3.one * f) * 0.1f;
        }, 0, 1, 0.5f).setEase(LeanTweenType.punch);
        foreach (GameElement gameElement in this.GameElements) {
            gameElement.Pull(this);
        }
    }

}
