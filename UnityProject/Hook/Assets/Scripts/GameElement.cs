using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameElement : MonoBehaviour {

    
    [SerializeField]
    private GameElement[] gameElements;
    [SerializeField]
    protected Puller[] pullers;

    private Stage stage;
    protected bool isHidden;

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
        if (!Application.isPlaying) {
            return;
        }
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
        this.pullers = this.GetPullers();
    }

    public Puller[] GetPullers() {
        List<GameElement> closeList = new List<GameElement>();
        List<Puller> pullers = new List<Puller>();
        closeList.Add(this);
        Queue<GameElement> elementsToInterate = new Queue<GameElement>();
        foreach (GameElement gameElement in this.GameElements) {
            if (gameElement is Puller) {
                pullers.Add(gameElement as Puller);
                closeList.Add(gameElement);
            }
            if (gameElement is Switch) {
                Switch swt = gameElement as Switch;
                Puller[] p = swt.GetPullersFor(this);
                pullers.AddRange(p);
            } else {
                elementsToInterate.Enqueue(gameElement);
            }
        }
        while (elementsToInterate.Count > 0) {
            GameElement gameElement = elementsToInterate.Dequeue();
            closeList.Add(gameElement);
            foreach (GameElement ge in gameElement.GameElements) {
                if (closeList.Contains(ge)) {
                    continue;
                }
                if (ge is Puller) {
                    pullers.Add(ge as Puller);
                    closeList.Add(ge);
                }
                if (ge is Switch) {
                    Switch swt = ge as Switch;
                    Puller[] p = swt.GetPullersFor(gameElement);
                    pullers.AddRange(p);
                } else {
                    elementsToInterate.Enqueue(ge);
                }
            }
        }

        // Removes duplicated elements.
        List<Puller> result = new List<Puller>();
        foreach (Puller puller in pullers) {
            if (!result.Contains(puller)) {
                result.Add(puller);
            }
        }
        return result.ToArray();
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
