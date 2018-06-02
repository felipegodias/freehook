using System.Collections.Generic;

public class CurveSwitch : Switch
{

    protected override bool IsElementInsideInput(GameElement element)
    {
        switch (SwitchState)
        {
            case SwitchState.A:
                return A == element || element == B;
            case SwitchState.B:
                return B == element || element == C;
            case SwitchState.C:
                return C == element || element == D;
            case SwitchState.D:
                return D == element || element == A;
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
                        B
                    };
                case SwitchState.D:
                    return new[]
                    {
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
                        A
                    };
                case SwitchState.B:
                    return new[]
                    {
                        C
                    };
            }
        }
        else if (element == C)
        {
            switch (SwitchState)
            {
                case SwitchState.B:
                    return new[]
                    {
                        B
                    };
                case SwitchState.C:
                    return new[]
                    {
                        D
                    };
            }
        }
        else if (element == D)
        {
            switch (SwitchState)
            {
                case SwitchState.C:
                    return new[]
                    {
                        C
                    };
                case SwitchState.D:
                    return new[]
                    {
                        A
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
                D
            };
        }

        if (element == B)
        {
            return new[]
            {
                A,
                C
            };
        }

        if (element == C)
        {
            return new[]
            {
                B,
                D
            };
        }

        if (element == D)
        {
            return new[]
            {
                C,
                A
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
            pullers.AddRange(DPullers);
            return pullers.ToArray();
        }

        if (element == B)
        {
            var pullers = new List<Puller>();
            pullers.AddRange(APullers);
            pullers.AddRange(CPullers);
            return pullers.ToArray();
        }

        if (element == C)
        {
            var pullers = new List<Puller>();
            pullers.AddRange(BPullers);
            pullers.AddRange(DPullers);
            return pullers.ToArray();
        }

        if (element == D)
        {
            var pullers = new List<Puller>();
            pullers.AddRange(CPullers);
            pullers.AddRange(APullers);
            return pullers.ToArray();
        }

        return new Puller[0];
    }

}