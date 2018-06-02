public class StraightSwitch : Switch
{

    protected override bool IsElementInsideInput(GameElement element)
    {
        if (SwitchState == SwitchState.A || SwitchState == SwitchState.C)
        {
            return A == element || C == element;
        }

        return B == element || D == element;
    }

    protected override GameElement[] GetOutput(GameElement element)
    {
        if (SwitchState == SwitchState.A || SwitchState == SwitchState.C)
        {
            if (element == A)
            {
                return new[]
                {
                    C
                };
            }

            if (element == C)
            {
                return new[]
                {
                    A
                };
            }
        }

        if (element == B)
        {
            return new[]
            {
                D
            };
        }

        if (element == D)
        {
            return new[]
            {
                B
            };
        }

        return new GameElement[0];
    }

    public override GameElement[] GetAllOutputsFor(GameElement element)
    {
        if (element == A)
        {
            return new[]
            {
                C
            };
        }

        if (element == B)
        {
            return new[]
            {
                D
            };
        }

        if (element == C)
        {
            return new[]
            {
                A
            };
        }

        if (element == D)
        {
            return new[]
            {
                B
            };
        }

        return new GameElement[0];
    }

    public override Puller[] GetPullersFor(GameElement element)
    {
        if (element == A)
        {
            return CPullers;
        }

        if (element == B)
        {
            return DPullers;
        }

        if (element == C)
        {
            return APullers;
        }

        if (element == D)
        {
            return BPullers;
        }

        return new Puller[0];
    }

}