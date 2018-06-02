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

public class TSwitch : Switch
{

    protected override bool IsElementInsideInput(GameElement element)
    {
        switch (SwitchState)
        {
            case SwitchState.A:
                return A == element || element == B || element == C;
            case SwitchState.B:
                return B == element || element == C || element == D;
            case SwitchState.C:
                return A == element || element == C || element == D;
            case SwitchState.D:
                return A == element || element == B || element == D;
        }

        return false;
    }

    protected override GameElement[] GetOutput(GameElement element)
    {
        if (element == A)
        {
            switch (SwitchState)
            {
                case SwitchState.A:
                    return new[]
                    {
                        B,
                        C
                    };
                case SwitchState.C:
                    return new[]
                    {
                        C,
                        D
                    };
                case SwitchState.D:
                    return new[]
                    {
                        B,
                        D
                    };
            }
        }
        else if (element == B)
        {
            switch (SwitchState)
            {
                case SwitchState.A:
                    return new[]
                    {
                        A,
                        C
                    };
                case SwitchState.B:
                    return new[]
                    {
                        C,
                        D
                    };
                case SwitchState.D:
                    return new[]
                    {
                        A,
                        D
                    };
            }
        }
        else if (element == C)
        {
            switch (SwitchState)
            {
                case SwitchState.A:
                    return new[]
                    {
                        A,
                        B
                    };
                case SwitchState.B:
                    return new[]
                    {
                        B,
                        D
                    };
                case SwitchState.C:
                    return new[]
                    {
                        A,
                        D
                    };
            }
        }
        else if (element == D)
        {
            switch (SwitchState)
            {
                case SwitchState.B:
                    return new[]
                    {
                        B,
                        C
                    };
                case SwitchState.C:
                    return new[]
                    {
                        A,
                        C
                    };
                case SwitchState.D:
                    return new[]
                    {
                        A,
                        B
                    };
            }
        }

        return new GameElement[0];
    }

    public override GameElement[] GetAllOutputsFor(GameElement element)
    {
        if (element == A)
        {
            return new[]
            {
                B,
                C,
                D
            };
        }

        if (element == B)
        {
            return new[]
            {
                A,
                C,
                D
            };
        }

        if (element == C)
        {
            return new[]
            {
                A,
                B,
                D
            };
        }

        if (element == D)
        {
            return new[]
            {
                A,
                B,
                C
            };
        }

        return new GameElement[0];
    }

    public override Puller[] GetPullersFor(GameElement element)
    {
        if (element == A)
        {
            var pullers = new List<Puller>();
            pullers.AddRange(BPullers);
            pullers.AddRange(CPullers);
            pullers.AddRange(DPullers);
            return pullers.ToArray();
        }

        if (element == B)
        {
            var pullers = new List<Puller>();
            pullers.AddRange(APullers);
            pullers.AddRange(CPullers);
            pullers.AddRange(DPullers);
            return pullers.ToArray();
        }

        if (element == C)
        {
            var pullers = new List<Puller>();
            pullers.AddRange(APullers);
            pullers.AddRange(BPullers);
            pullers.AddRange(DPullers);
            return pullers.ToArray();
        }

        if (element == D)
        {
            var pullers = new List<Puller>();
            pullers.AddRange(APullers);
            pullers.AddRange(BPullers);
            pullers.AddRange(CPullers);
            return pullers.ToArray();
        }

        return new Puller[0];
    }

}