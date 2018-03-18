using DG.Tweening;

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHeart : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI empty;
    [SerializeField]
    private TextMeshProUGUI fill;

    private bool isFill;

    private void Awake() {
        this.isFill = true;
    }

    public void SetFill() {
        if (this.isFill) {
            return;
        }
        this.isFill = true;

        this.fill.transform.localScale = Vector3.one * 2;
        this.fill.transform.DOScale(Vector3.one, 0.5f);

        Color colorTo = this.fill.color;
        colorTo.a = 1;
        this.fill.DOColor(colorTo, 0.5f);
    }

    public void SetEmpty() {
        if (!this.isFill) {
            return;
        }
        this.isFill = false;

        this.fill.transform.localScale = Vector3.one;
        this.fill.transform.DOScale(Vector3.one * 2, 0.5f);

        Color colorTo = this.fill.color;
        colorTo.a = 0;
        this.fill.DOColor(colorTo, 0.5f);
    }

}
