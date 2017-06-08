using UnityEngine;

public class ABSwitch : Switch {

    [SerializeField]
    private GameElement inA;
    [SerializeField]
    private GameElement outA;

    [SerializeField]
    private GameElement inB;

    [SerializeField]
    private GameElement outB;

    
    public override bool IsClear {
        get { return true; }
    }

    public override void Hide() {
        bool canHide = true;

        if (this.outA != null) {
            canHide = this.outA.IsClear;
        }

        if (this.outB != null) {
            canHide = this.outB.IsClear && canHide;
        }

        if (canHide) {
            base.Hide();
        }
    }

    protected override bool IsElementInsideInput(GameElement element) {
        if (this.SwitchState == SwitchState.A || this.SwitchState == SwitchState.C) {
            return this.inA == element;
        }
        return this.inB == element;
    }

    protected override GameElement[] GetOutput() {
        if (this.SwitchState == SwitchState.A || this.SwitchState == SwitchState.C) {
            return new []{ this.outA };
        }
        return new[] { this.outB };
    }

    private void OnDrawGizmosSelected() {
        Color oldColor = Gizmos.color;
        Gizmos.color = Color.blue;

        if (this.inA != null) {
            Gizmos.DrawLine(this.inA.transform.position, this.transform.position);
        }

        if (this.outA != null) {
            Gizmos.DrawLine(this.outA.transform.position, this.transform.position);
        }

        Gizmos.color = Color.red;

        if (this.inB != null) {
            Gizmos.DrawLine(this.inB.transform.position, this.transform.position);
        }

        if (this.outB != null) {
            Gizmos.DrawLine(this.outB.transform.position, this.transform.position);
        }
        Gizmos.color = oldColor;
    }

}