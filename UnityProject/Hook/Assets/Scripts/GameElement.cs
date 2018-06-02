using System.Collections.Generic;

using DG.Tweening;
using DG.Tweening.Core;

using UnityEngine;

public class GameElement : MonoBehaviour
{

    [SerializeField]
    private GameElement[] gameElements;

    [SerializeField]
    protected Puller[] pullers;

    private Stage stage;
    protected bool isHidden;

    public GameElement[] GameElements
    {
        get
        {
            return gameElements;
        }
    }

    public bool IsHidden
    {
        get
        {
            return isHidden;
        }
    }

    public Stage Stage
    {
        get
        {
            if (stage != null)
            {
                return stage;
            }

            stage = GetComponentInParent<Stage>();
            return stage;
        }
    }

    protected virtual void LateUpdate()
    {
        if (!Application.isPlaying)
        {
            return;
        }

        if (isHidden || pullers.Length == 0)
        {
            return;
        }

        foreach (Puller puller in pullers)
        {
            if (!puller.IsClear)
            {
                return;
            }
        }

        isHidden = true;
        Hide();
    }

    public void SetPullers()
    {
        pullers = GetPullers();
    }

    public Puller[] GetPullers()
    {
        var closeList = new List<GameElement>();
        var pullers = new List<Puller>();
        closeList.Add(this);
        var elementsToInterate = new Queue<GameElement>();
        foreach (GameElement gameElement in GameElements)
        {
            if (gameElement is Puller)
            {
                pullers.Add(gameElement as Puller);
                closeList.Add(gameElement);
            }

            if (gameElement is Switch)
            {
                var swt = gameElement as Switch;
                Puller[] p = swt.GetPullersFor(this);
                pullers.AddRange(p);
            }
            else
            {
                elementsToInterate.Enqueue(gameElement);
            }
        }

        while (elementsToInterate.Count > 0)
        {
            GameElement gameElement = elementsToInterate.Dequeue();
            closeList.Add(gameElement);
            foreach (GameElement ge in gameElement.GameElements)
            {
                if (closeList.Contains(ge))
                {
                    continue;
                }

                if (ge is Puller)
                {
                    pullers.Add(ge as Puller);
                    closeList.Add(ge);
                }

                if (ge is Switch)
                {
                    var swt = ge as Switch;
                    Puller[] p = swt.GetPullersFor(gameElement);
                    pullers.AddRange(p);
                }
                else
                {
                    elementsToInterate.Enqueue(ge);
                }
            }
        }

        // Removes duplicated elements.
        var result = new List<Puller>();
        foreach (Puller puller in pullers)
        {
            if (!result.Contains(puller))
            {
                result.Add(puller);
            }
        }

        return result.ToArray();
    }

    public virtual void Pull(GameElement element)
    {
        if (!Stage.CanPull(this))
        {
            return;
        }

        Stage.AddToPullingList(this);
        foreach (GameElement gameElement in gameElements)
        {
            gameElement.Pull(this);
        }
    }

    public virtual void Hide()
    {
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>(true);
        foreach (SpriteRenderer spriteRenderer in renderers)
        {
            spriteRenderer.sortingOrder = RenderUtils.GetDrawOrder();
        }

        DOGetter<float> getter = () => 0;

        DOSetter<float> setter = f =>
        {
            Color a = ColorUtils.LineColor;
            Color b = ColorUtils.BackgroundColor;
            Color c = ColorUtils.Lerp(a, b, f);
            foreach (SpriteRenderer spriteRenderer in renderers)
            {
                if (spriteRenderer == null)
                {
                    continue;
                }

                spriteRenderer.color = c;
            }
        };

        DOTween.To(getter, setter, 1, 0.33f);
    }

}