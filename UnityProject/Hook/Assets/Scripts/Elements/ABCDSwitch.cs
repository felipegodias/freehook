using UnityEditor;
using UnityEngine;

public class ABCDSwitch : Switch {

    public override bool IsClear {
        get { return true; }
    }



    protected override bool IsElementInsideInput(GameElement element) {
        switch (this.SwitchState) {
            case SwitchState.A:
                return this.A == element || element == this.B;
            case SwitchState.B:
                return this.B == element || element == this.C;
            case SwitchState.C:
                return this.C == element || element == this.D;
            case SwitchState.D:
                return this.D == element || element == this.A;
        }
        return false;
    }

    protected override GameElement[] GetOutput(GameElement element) {
        if (element == this.A) {
            switch (this.SwitchState) {
                case SwitchState.A:
                    return new[] { this.B };
                case SwitchState.D:
                    return new[] { this.D };
            }
        } else if (element == this.B) {
            switch (this.SwitchState) {
                case SwitchState.A:
                    return new[] { this.A };
                case SwitchState.B:
                    return new[] { this.C };
            }
        } else if (element == this.C) {
            switch (this.SwitchState) {
                case SwitchState.B:
                    return new[] { this.B };
                case SwitchState.C:
                    return new[] { this.D };
            }
        } else if (element == this.D) {
            switch (this.SwitchState) {
                case SwitchState.C:
                    return new[] { this.C };
                case SwitchState.D:
                    return new[] { this.A };
            }
        }
        return new GameElement[0];
    }

    public override bool IsClearForElement(GameElement element) {
        if (element == this.A) {
            bool isBClear = true;
            bool isDClear = true;
            if (this.B != null) {
                isBClear = this.B.IsClear;
            }
            if (this.D != null) {
                isDClear = this.D.IsClear;
            }
            return isBClear && isDClear;
        }
        if (element == this.B) {
            bool isAClear = true;
            bool isCClear = true;
            if (this.A != null) {
                isAClear = this.A.IsClear;
            }
            if (this.C != null) {
                isCClear = this.C.IsClear;
            }
            return isAClear && isCClear;
        }
        if (element == this.C) {
            bool isBClear = true;
            bool isDClear = true;
            if (this.B != null) {
                isBClear = this.B.IsClear;
            }
            if (this.D != null) {
                isDClear = this.D.IsClear;
            }
            return isBClear && isDClear;
        }
        if (element == this.D) {
            bool isCClear = true;
            bool isAClear = true;
            if (this.C != null) {
                isCClear = this.C.IsClear;
            }
            if (this.A != null) {
                isAClear = this.A.IsClear;
            }
            return isCClear && isAClear;
        }
        return false;
    }

}