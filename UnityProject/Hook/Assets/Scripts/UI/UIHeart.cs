using DG.Tweening;

using TMPro;

using UnityEngine;

public class UIHeart : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI empty;

    [SerializeField]
    private TextMeshProUGUI fill;

    private bool isFill;

    private void Awake()
    {
        isFill = true;
    }

    public void SetFill()
    {
        if (isFill)
        {
            return;
        }

        isFill = true;

        fill.transform.localScale = Vector3.one * 2;
        fill.transform.DOScale(Vector3.one, 0.5f);

        Color colorTo = fill.color;
        colorTo.a = 1;
        fill.DOColor(colorTo, 0.5f);
    }

    public void SetEmpty()
    {
        if (!isFill)
        {
            return;
        }

        isFill = false;

        fill.transform.localScale = Vector3.one;
        fill.transform.DOScale(Vector3.one * 2, 0.5f);

        Color colorTo = fill.color;
        colorTo.a = 0;
        fill.DOColor(colorTo, 0.5f);
    }

}