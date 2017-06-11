using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameElement : MonoBehaviour {

    
    [SerializeField]
    private GameElement[] gameElements;
    [SerializeField]
    protected Puller[] pullers;

    private Stage stage;
    private bool isHidden;

    public GameElement[] GameElements {
        get { return this.gameElements; }
    }

    public bool IsHidden {
        get { return this.isHidden; }
    }

    public Stage Stage {
        get {
            if (this.stage != null) {
                return this.stage;
            }
            this.stage = this.GetComponentInParent<Stage>();
            return this.stage;
        }
    }

    protected virtual void LateUpdate() {
        if (this.isHidden || this.pullers.Length == 0) {
            return;
        }
        foreach (Puller puller in this.pullers) {
            if (!puller.IsClear) {
                return;
            }
        }
        this.isHidden = true;
        this.Hide();
    }

    public void SetPullers() {
        this.pullers = this.GetPullers(this, null);
    }

    public Puller[] GetPullers(GameElement gameElement, GameElement previous) {
        if (gameElement is Puller) {
            return new[] {gameElement as Puller};
        }
        List<Puller> pullers = new List<Puller>();
        if (gameElement is Switch) {
            Puller[] p = (gameElement as Switch).GetPullersFor(previous);
            foreach (Puller puller in p) {
                if (!pullers.Contains(puller)) {
                    pullers.Add(puller);
                }
            }
        } else {
            foreach (GameElement e in gameElement.gameElements) {
                if (e == null) {
                    continue;
                }
                if (e is Puller) {
                    pullers.Add(e as Puller);
                } else {
                    Puller[] p = this.GetPullers(e, gameElement);
                    foreach (Puller puller in p) {
                        if (!pullers.Contains(puller)) {
                            pullers.Add(puller);
                        }
                    }
                }
            }
        }
        return pullers.ToArray();
    }

    public virtual void Pull(GameElement element) {
        if (!this.Stage.CanPull(this)) {
            return;
        }
        this.Stage.AddToPullingList(this);
        foreach (GameElement gameElement in this.gameElements) {
            gameElement.Pull(this);
        }
    }

    public virtual void Hide() {
        SpriteRenderer[] renderers = this.GetComponentsInChildren<SpriteRenderer>(true);
        LeanTween.value(this.gameObject, f => {
            Color a = ColorUtils.LineColor;
            Color b = a;
            b.a = 0;
            Color c = ColorUtils.Lerp(a, b, f);
            foreach (SpriteRenderer spriteRenderer in renderers) {
                if (spriteRenderer == null) {
                    continue;
                }
                spriteRenderer.color = c;
            }
        }, 0, 1, 0.33f).setEase(LeanTweenType.easeOutSine);
    }

}
