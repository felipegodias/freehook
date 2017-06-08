using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Switch : GameElement, IPointerClickHandler {

    [SerializeField]
    private SwitchState switchState;

    public SwitchState SwitchState {
        get { return this.switchState; }
    }

    private void OnValidate() {
        this.transform.rotation = Quaternion.Euler(0, 0, 90 * (int)this.switchState);
    }

    public override void Pull(GameElement element) {
        this.previousElement = element;
        if (!this.IsElementInsideInput(element)) {
            return;
        }
        GameElement[] gameElements = this.GetOutput();
        foreach (GameElement gameElement in gameElements) {
            gameElement.Pull(this);
        }
    }

    protected abstract bool IsElementInsideInput(GameElement element);

    protected abstract GameElement[] GetOutput();

    public void OnPointerClick(PointerEventData eventData) {
        this.switchState++;
        if (this.switchState > SwitchState.D) {
            this.switchState = SwitchState.A;
        }
        this.transform.rotation = Quaternion.Euler(0, 0, 90 * (int)this.switchState);
    }

}