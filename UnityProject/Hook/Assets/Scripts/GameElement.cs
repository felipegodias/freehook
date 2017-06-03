using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameElement : MonoBehaviour {

    [SerializeField]
    private GameElement[] gameElements;

    protected GameElement previousElement;

    public GameElement PreviousElement {
        get { return this.previousElement; }
    }

    public GameElement[] GameElements {
        get { return this.gameElements; }
    }

    public virtual bool IsClear {
        get {
            foreach (GameElement gameElement in this.gameElements) {
                if (!gameElement.IsClear) {
                    return false;
                }
            }
            return true;
        }
    }

    public virtual void Pull(GameElement element) {
        this.previousElement = element;
        foreach (GameElement gameElement in this.gameElements) {
            gameElement.Pull(this);
        }
    }

    public virtual void Hide() {
        SpriteRenderer[] renderers = this.GetComponentsInChildren<SpriteRenderer>(true);
        LeanTween.value(this.gameObject, f => {
            foreach (SpriteRenderer spriteRenderer in renderers) {
                Color color = spriteRenderer.color;
                color.a = 1 - f;
                spriteRenderer.color = color;
            }
        }, 0, 1, 0.2f).setEase(LeanTweenType.easeOutSine);
    }

}
