using UnityEngine;
using UnityEngine.UI;

public class UIHeart : MonoBehaviour {

    [SerializeField]
    private Text empty;
    [SerializeField]
    private Text fill;

    private bool isFill;

    private void Awake() {
        this.isFill = true;
    }

    public void SetFill() {
        if (this.isFill) {
            return;
        }
        this.isFill = true;
        LeanTween.value(this.gameObject, f => {
            this.fill.transform.localScale = Vector3.one + Vector3.one * (1 - f);
            Color fillColor = this.fill.color;
            fillColor.a = f;
            this.fill.color = fillColor;
        }, 0, 1, 0.5f).setEase(LeanTweenType.easeOutSine);
    }

    public void SetEmpty() {
        if (!this.isFill) {
            return;
        }
        this.isFill = false;
        LeanTween.value(this.gameObject, f => {
            this.fill.transform.localScale = Vector3.one + Vector3.one * f;
            Color fillColor = this.fill.color;
            fillColor.a = 1 - f;
            this.fill.color = fillColor;
        }, 0, 1, 0.5f).setEase(LeanTweenType.easeOutSine);
    }

}
