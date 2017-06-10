using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Switch : GameElement, IPointerClickHandler {

    [SerializeField]
    private SwitchState switchState;

    public SwitchState SwitchState {
        get { return this.switchState; }
    }

    public void OnPointerClick(PointerEventData eventData) {
        this.switchState++;
        if (this.switchState > SwitchState.D) {
            this.switchState = SwitchState.A;
        }
        this.transform.rotation = Quaternion.Euler(0, 0, -90 * (int) this.switchState);
    }

    public override void Pull(GameElement element) {
        if (!this.IsElementInsideInput(element)) {
            return;
        }
        this.previousElement = element;
        GameElement[] gameElements = this.GetOutput(element);
        foreach (GameElement gameElement in gameElements) {
            gameElement.Pull(this);
        }
    }

    public abstract bool IsClearForElement(GameElement element);

    protected abstract bool IsElementInsideInput(GameElement element);

    protected abstract GameElement[] GetOutput(GameElement element);

    private void OnValidate() {
        this.transform.rotation = Quaternion.Euler(0, 0, -90 * (int) this.switchState);
    }

}