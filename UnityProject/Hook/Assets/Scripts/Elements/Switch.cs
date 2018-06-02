using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class Switch : GameElement, IPointerClickHandler
{

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

    [SerializeField]
    private Puller[] aPullers;

    [SerializeField]
    private Puller[] bPullers;

    [SerializeField]
    private Puller[] cPullers;

    [SerializeField]
    private Puller[] dPullers;

    public SwitchState SwitchState
    {
        get
        {
            return switchState;
        }
    }

    public GameElement A
    {
        get
        {
            return a;
        }
    }

    public GameElement B
    {
        get
        {
            return b;
        }
    }

    public GameElement C
    {
        get
        {
            return c;
        }
    }

    public GameElement D
    {
        get
        {
            return d;
        }
    }

    public Puller[] APullers
    {
        get
        {
            return aPullers;
        }
    }

    public Puller[] BPullers
    {
        get
        {
            return bPullers;
        }
    }

    public Puller[] CPullers
    {
        get
        {
            return cPullers;
        }
    }

    public Puller[] DPullers
    {
        get
        {
            return dPullers;
        }
    }

    public void LookForPullers()
    {
        var pullers = new List<Puller>();
        if (A != null)
        {
            foreach (GameElement e in A.GameElements)
            {
                if (e == this)
                {
                    continue;
                }

                if (e is Puller)
                {
                    pullers.AddIfNotContains(e as Puller);
                }
                else
                {
                    pullers.AddRangeIfNotContains(e.GetPullers());
                }
            }
        }

        aPullers = pullers.ToArray();
        pullers.Clear();
        if (B != null)
        {
            foreach (GameElement e in B.GameElements)
            {
                if (e == this)
                {
                    continue;
                }

                if (e is Puller)
                {
                    pullers.AddIfNotContains(e as Puller);
                }
                else
                {
                    pullers.AddRangeIfNotContains(e.GetPullers());
                }
            }
        }

        bPullers = pullers.ToArray();
        pullers.Clear();
        if (C != null)
        {
            foreach (GameElement e in C.GameElements)
            {
                if (e == this)
                {
                    continue;
                }

                if (e is Puller)
                {
                    pullers.AddIfNotContains(e as Puller);
                }
                else
                {
                    pullers.AddRangeIfNotContains(e.GetPullers());
                }
            }
        }

        cPullers = pullers.ToArray();
        pullers.Clear();
        if (D != null)
        {
            foreach (GameElement e in D.GameElements)
            {
                if (e == this)
                {
                    continue;
                }

                if (e is Puller)
                {
                    pullers.AddIfNotContains(e as Puller);
                }
                else
                {
                    pullers.AddRangeIfNotContains(e.GetPullers());
                }
            }
        }

        dPullers = pullers.ToArray();
        pullers.Clear();
        pullers.AddRangeIfNotContains(aPullers);
        pullers.AddRangeIfNotContains(bPullers);
        pullers.AddRangeIfNotContains(cPullers);
        pullers.AddRangeIfNotContains(dPullers);
        this.pullers = pullers.ToArray();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switchState++;
        if (switchState > SwitchState.D)
        {
            switchState = SwitchState.A;
        }

        transform.rotation = Quaternion.Euler(0, 0, -90 * (int) switchState);
    }

    public override void Pull(GameElement element)
    {
        if (!IsElementInsideInput(element))
        {
            return;
        }

        GameElement[] gameElements = GetOutput(element);
        foreach (GameElement gameElement in gameElements)
        {
            if (gameElement != null)
            {
                gameElement.Pull(this);
            }
        }
    }

    protected abstract bool IsElementInsideInput(GameElement element);
    protected abstract GameElement[] GetOutput(GameElement element);
    public abstract GameElement[] GetAllOutputsFor(GameElement element);

    public abstract Puller[] GetPullersFor(GameElement element);

    private void OnValidate()
    {
        transform.rotation = Quaternion.Euler(0, 0, -90 * (int) switchState);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Vector3 a = transform.position + Vector3.left / 2;
        Handles.Label(a, "A", EditorStyles.centeredGreyMiniLabel);
        Vector3 b = transform.position + Vector3.up / 2;
        Handles.Label(b, "B", EditorStyles.centeredGreyMiniLabel);
        Vector3 c = transform.position + Vector3.right / 2;
        Handles.Label(c, "C", EditorStyles.centeredGreyMiniLabel);
        Vector3 d = transform.position + Vector3.down / 2;
        Handles.Label(d, "D", EditorStyles.centeredGreyMiniLabel);
    }
#endif

}