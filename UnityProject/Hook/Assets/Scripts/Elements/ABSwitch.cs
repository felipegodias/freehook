using UnityEngine;

public class ABSwitch : Switch {

    public override bool IsClear {
        get { return true; }
    }

    protected override bool IsElementInsideInput(GameElement element) {
        if (this.SwitchState == SwitchState.A || this.SwitchState == SwitchState.C) {
            return this.A == element || this.C == element;
        }
        return this.B == element || this.D == element;
    }

    protected override GameElement[] GetOutput(GameElement element) {
        if (this.SwitchState == SwitchState.A || this.SwitchState == SwitchState.C) {
            if (element == this.A) {
                return new[] { this.C };
            }
            if (element == this.C) {
                return new[] { this.A };
            }
        }
        if (element == this.B) {
            return new[] { this.D };
        }

        if (element == this.D) {
            return new[] { this.B };
        }

        return new GameElement[0];
    }

    public override bool IsClearForElement(GameElement element) {
        if (element == this.A) {
            return this.C.IsClear;
        }
        if (element == this.B) {
            return this.D.IsClear;
        }
        if (element == this.C) {
            return this.A.IsClear;
        }
        if (element == this.D) {
            return this.B.IsClear;
        }
        return false;
    }

}