// AAAAAA || BBBBBB || CCCCCC || DDDDDDD||
//--------------------------------------||
//   BB   ||   BB   ||   XX   ||   BB   ||
//   BB   ||   BB   ||   XX   ||   BB   ||
// AA  CC || XX  CC || AA  CC || AA  XX ||
// AA  CC || XX  CC || AA  CC || AA  XX ||
//   XX   ||   DD   ||   DD   ||   DD   ||
//   XX   ||   DD   ||   DD   ||   DD   ||
//--------------------------------------||

using System.Collections.Generic;

public class TSwitch : Switch {

    protected override bool IsElementInsideInput(GameElement element) {
        switch (this.SwitchState) {
            case SwitchState.A:
                return this.A == element || element == this.B || element == this.C;
            case SwitchState.B:
                return this.B == element || element == this.C || element == this.D;
            case SwitchState.C:
                return this.A == element || element == this.C || element == this.D;
            case SwitchState.D:
                return this.A == element || element == this.B || element == this.D;
        }
        return false;
    }

    protected override GameElement[] GetOutput(GameElement element) {
        if (element == this.A) {
            switch (this.SwitchState) {
                case SwitchState.A:
                    return new[] { this.B, this.C };
                case SwitchState.C:
                    return new[] { this.C, this.D };
                case SwitchState.D:
                    return new[] { this.B, this.D };
            }
        } else if (element == this.B) {
            switch (this.SwitchState) {
                case SwitchState.A:
                    return new[] { this.A, this.C };
                case SwitchState.B:
                    return new[] { this.C, this.D };
                case SwitchState.D:
                    return new[] { this.A, this.D };
            }
        } else if (element == this.C) {
            switch (this.SwitchState) {
                case SwitchState.A:
                    return new[] { this.A, this.B };
                case SwitchState.B:
                    return new[] { this.B, this.D };
                case SwitchState.C:
                    return new[] { this.A, this.D };
            }
        } else if (element == this.D) {
            switch (this.SwitchState) {
                case SwitchState.B:
                    return new[] { this.B, this.C };
                case SwitchState.C:
                    return new[] { this.A, this.C };
                case SwitchState.D:
                    return new[] { this.A, this.B };
            }
        }
        return new GameElement[0];
    }

    public override GameElement[] GetAllOutputsFor(GameElement element) {
        if (element == this.A) {
            return new[] { this.B, this.C, this.D };
        }
        if (element == this.B) {
            return new[] { this.A, this.C, this.D };
        }
        if (element == this.C) {
            return new[] { this.A, this.B, this.D };
        }
        if (element == this.D) {
            return new[] { this.A, this.B, this.C };
        }
        return new GameElement[0];
    }

    public override Puller[] GetPullersFor(GameElement element) {
        if (element == this.A) {
            List<Puller> pullers = new List<Puller>();
            pullers.AddRange(this.BPullers);
            pullers.AddRange(this.CPullers);
            pullers.AddRange(this.DPullers);
            return pullers.ToArray();
        }
        if (element == this.B) {
            List<Puller> pullers = new List<Puller>();
            pullers.AddRange(this.APullers);
            pullers.AddRange(this.CPullers);
            pullers.AddRange(this.DPullers);
            return pullers.ToArray();
        }
        if (element == this.C) {
            List<Puller> pullers = new List<Puller>();
            pullers.AddRange(this.APullers);
            pullers.AddRange(this.BPullers);
            pullers.AddRange(this.DPullers);
            return pullers.ToArray();
        }
        if (element == this.D) {
            List<Puller> pullers = new List<Puller>();
            pullers.AddRange(this.APullers);
            pullers.AddRange(this.BPullers);
            pullers.AddRange(this.CPullers);
            return pullers.ToArray();
        }
        return new Puller[0];
    }

}
