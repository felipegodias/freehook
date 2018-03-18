using DG.Tweening;

using MGS.EventManager;
using UnityEngine;
using UnityEngine.EventSystems;

public class Trigger : GameElement, IPointerClickHandler {

    public void OnPointerClick(PointerEventData eventData) {
        this.Stage.ClearPullingList();
        EventManager.Dispatch(new OnTriggerClick());

        this.transform.transform.localScale = Vector3.one;
        this.transform.DOPunchScale(Vector3.one * 0.15f, 0.5f, 6);

        foreach (GameElement gameElement in this.GameElements) {
            gameElement.Pull(this);
        }
    }

}
