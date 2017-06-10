using UnityEditor;
using UnityEngine;

public class ABCDSwitch : Switch {

    [SerializeField]
    private GameElement inA;

    [SerializeField]
    private GameElement outA;

    [SerializeField]
    private GameElement inB;

    [SerializeField]
    private GameElement outB;

    [SerializeField]
    private GameElement inC;

    [SerializeField]
    private GameElement outC;

    [SerializeField]
    private GameElement inD;

    [SerializeField]
    private GameElement outD;

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

        if (this.outC != null) {
            canHide = this.outC.IsClear && canHide;
        }

        if (this.outD != null) {
            canHide = this.outD.IsClear && canHide;
        }

        if (canHide) {
            base.Hide();
        }
    }

    protected override bool IsElementInsideInput(GameElement element) {
        switch (this.SwitchState) {
            case SwitchState.A:
                return this.inA == element || element == this.inB;
            case SwitchState.B:
                return this.inB == element || element == this.inC;
            case SwitchState.C:
                return this.inC == element || element == this.inD;
            case SwitchState.D:
                return this.inD == element || element == this.inA;
        }
        return false;
    }

    protected override GameElement[] GetOutput(GameElement element) {
        if (element == this.inA) {
            switch (this.SwitchState) {
                case SwitchState.A:
                    return new[] { this.outA };
                case SwitchState.D:
                    return new[] { this.outC };
            }
        } else if (element == this.inB) {
            switch (this.SwitchState) {
                case SwitchState.A:
                    return new[] { this.outD };
                case SwitchState.B:
                    return new[] { this.outB };
            }
        } else if (element == this.inC) {
            switch (this.SwitchState) {
                case SwitchState.B:
                    return new[] { this.outA };
                case SwitchState.C:
                    return new[] { this.outC };
            }
        } else if (element == this.inD) {
            switch (this.SwitchState) {
                case SwitchState.C:
                    return new[] { this.outB };
                case SwitchState.D:
                    return new[] { this.outD };
            }
        }
        return new GameElement[0];
    }

    public override bool IsClearForElement(GameElement element) {
        if (element == this.inA) {
            bool isOutAClear = true;
            bool isOutCClear = true;
            if (this.outA != null) {
                isOutAClear = this.outA.IsClear;
            }
            if (this.outC != null) {
                isOutCClear = this.outC.IsClear;
            }
            return isOutAClear && isOutCClear;
        }
        if (element == this.inB) {
            bool isOutDClear = true;
            bool isOutBClear = true;
            if (this.outD != null) {
                isOutDClear = this.outD.IsClear;
            }
            if (this.outB != null) {
                isOutBClear = this.outB.IsClear;
            }
            return isOutDClear && isOutBClear;
        }
        if (element == this.inC) {
            bool isOutAClear = true;
            bool isOutCClear = true;
            if (this.outA != null) {
                isOutAClear = this.outA.IsClear;
            }
            if (this.outC != null) {
                isOutCClear = this.outC.IsClear;
            }
            return isOutAClear && isOutCClear;
        }
        if (element == this.inD) {
            bool isOutBClear = true;
            bool isOutDClear = true;
            if (this.outB != null) {
                isOutBClear = this.outB.IsClear;
            }
            if (this.outD != null) {
                isOutDClear = this.outD.IsClear;
            }
            return isOutBClear && isOutDClear;
        }
        return false;
    }

    private void OnDrawGizmos() {
        Vector3 ad = this.transform.position + Vector3.left / 2;
        Handles.Label(ad, "A/D", EditorStyles.whiteLabel);
        Vector3 ba = this.transform.position + Vector3.up / 2;
        Handles.Label(ba, "B/A", EditorStyles.whiteLabel);
        Vector3 cb = this.transform.position + Vector3.right / 2;
        Handles.Label(cb, "C/B", EditorStyles.label);
        Vector3 dc = this.transform.position + Vector3.down / 2;
        Handles.Label(dc, "D/C", EditorStyles.label);
    }

}