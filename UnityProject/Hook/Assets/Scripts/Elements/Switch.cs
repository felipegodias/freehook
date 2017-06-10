using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Switch : GameElement, IPointerClickHandler {

    [SerializeField]
    private SwitchState switchState;

    [SerializeField]
    private GameElement a;

    [SerializeField]
    private GameElement b;

    [SerializeField]
    private GameElement c;

    [SerializeField]
    private GameElement d;

    private bool isHidden;

    public SwitchState SwitchState {
        get { return this.switchState; }
    }

    public GameElement A {
        get { return this.a; }
    }

    public GameElement B {
        get { return this.b; }
    }

    public GameElement C {
        get { return this.c; }
    }

    public GameElement D {
        get { return this.d; }
    }

    public override void Hide() {
        bool canHide = true;

        if (this.A != null) {
            canHide = this.A.IsClear;
        }

        if (this.B != null) {
            canHide = this.B.IsClear && canHide;
        }

        if (this.C != null) {
            canHide = this.C.IsClear && canHide;
        }

        if (this.D != null) {
            canHide = this.D.IsClear && canHide;
        }

        if (canHide) {
            this.isHidden = true;
            base.Hide();
        }
    }

    private void Update() {
        if (!this.isHidden) {
            this.Hide();
        }
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

    private void OnDrawGizmos() {
        Vector3 a = this.transform.position + Vector3.left / 2;
        Handles.Label(a, "A", EditorStyles.centeredGreyMiniLabel);
        Vector3 b = this.transform.position + Vector3.up / 2;
        Handles.Label(b, "B", EditorStyles.centeredGreyMiniLabel);
        Vector3 c = this.transform.position + Vector3.right / 2;
        Handles.Label(c, "C", EditorStyles.centeredGreyMiniLabel);
        Vector3 d = this.transform.position + Vector3.down / 2;
        Handles.Label(d, "D", EditorStyles.centeredGreyMiniLabel);
    }

}