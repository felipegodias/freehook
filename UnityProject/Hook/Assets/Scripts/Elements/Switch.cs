using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Switch : GameElement, IPointerClickHandler {

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


    public SwitchState SwitchState {
        get { return this.switchState; }
    }

    public GameElement A {
        get { return this.a; }
    }

    public GameElement B {
        get { return this.b; }
    }

    public GameElement C {
        get { return this.c; }
    }

    public GameElement D {
        get { return this.d; }
    }

    public Puller[] APullers {
        get { return this.aPullers; }
    }

    public Puller[] BPullers {
        get { return this.bPullers; }
    }

    public Puller[] CPullers {
        get { return this.cPullers; }
    }

    public Puller[] DPullers {
        get { return this.dPullers; }
    }

    public void LookForPullers() {
        List<Puller> pullers = new List<Puller>();
        if (this.A != null) {
            foreach (GameElement e in this.A.GameElements) {
                if (e == this) {
                    continue;
                }
                if (e is Puller) {
                    pullers.AddIfNotContains(e as Puller);
                } else {
                    pullers.AddRangeIfNotContains(e.GetPullers());
                }
            }
        }
        aPullers = pullers.ToArray();
        pullers.Clear();
        if (this.B != null) {
            foreach (GameElement e in this.B.GameElements) {
                if (e == this) {
                    continue;
                }
                if (e is Puller) {
                    pullers.AddIfNotContains(e as Puller);
                } else {
                    pullers.AddRangeIfNotContains(e.GetPullers());
                }
            }
        }
        bPullers = pullers.ToArray();
        pullers.Clear();
        if (this.C != null) {
            foreach (GameElement e in this.C.GameElements) {
                if (e == this) {
                    continue;
                }
                if (e is Puller) {
                    pullers.AddIfNotContains(e as Puller);
                } else {
                    pullers.AddRangeIfNotContains(e.GetPullers());
                }
            }
        }
        cPullers = pullers.ToArray();
        pullers.Clear();
        if (this.D != null) {
            foreach (GameElement e in this.D.GameElements) {
                if (e == this) {
                    continue;
                }
                if (e is Puller) {
                    pullers.AddIfNotContains(e as Puller);
                } else {
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

    public void OnPointerClick(PointerEventData eventData) {
        this.switchState++;
        if (this.switchState > SwitchState.D) {
            this.switchState = SwitchState.A;
        }
        this.transform.rotation = Quaternion.Euler(0, 0, -90 * (int) this.switchState);
    }

    public override void Pull(GameElement element) {
        if (!this.IsElementInsideInput(element)) {
            return;
        }
        GameElement[] gameElements = this.GetOutput(element);
        foreach (GameElement gameElement in gameElements) {
            if (gameElement != null) {
                gameElement.Pull(this);
            }
        }
    }

    protected abstract bool IsElementInsideInput(GameElement element);
    protected abstract GameElement[] GetOutput(GameElement element);
    public abstract GameElement[] GetAllOutputsFor(GameElement element);

    public abstract Puller[] GetPullersFor(GameElement element);

    private void OnValidate() {
        this.transform.rotation = Quaternion.Euler(0, 0, -90 * (int) this.switchState);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        Vector3 a = this.transform.position + Vector3.left / 2;
        Handles.Label(a, "A", EditorStyles.centeredGreyMiniLabel);
        Vector3 b = this.transform.position + Vector3.up / 2;
        Handles.Label(b, "B", EditorStyles.centeredGreyMiniLabel);
        Vector3 c = this.transform.position + Vector3.right / 2;
        Handles.Label(c, "C", EditorStyles.centeredGreyMiniLabel);
        Vector3 d = this.transform.position + Vector3.down / 2;
        Handles.Label(d, "D", EditorStyles.centeredGreyMiniLabel);
    }
#endif

}