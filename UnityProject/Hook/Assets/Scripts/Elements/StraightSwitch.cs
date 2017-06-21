using UnityEngine;

public class StraightSwitch : Switch {

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

    public override GameElement[] GetAllOutputsFor(GameElement element) {
        if (element == this.A) {
            return new[] { this.C };
        }
        if (element == this.B) {
            return new[] { this.D };
        }
        if (element == this.C) {
            return new[] { this.A };
        }
        if (element == this.D) {
            return new[] { this.B };
        }
        return new GameElement[0];
    }

    public override Puller[] GetPullersFor(GameElement element) {
        if (element == this.A) {
            return this.CPullers;
        }
        if (element == this.B) {
            return this.DPullers;
        }
        if (element == this.C) {
            return this.APullers;
        }
        if (element == this.D) {
            return this.BPullers;
        }
        return new Puller[0];
    }

}