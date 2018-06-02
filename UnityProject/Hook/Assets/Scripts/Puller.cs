using System.Collections.Generic;

using DG.Tweening;

using UnityEngine;

public class Puller : GameElement
{

    private Transform content;
    private Transform puller;
    private Transform pullerEnd;

    private bool isStartedToPull;

    private bool isPullerClear;

    private bool isBreaked;

    private LinkedList<Tweener> pullTweeners;

    public Transform Content
    {
        get
        {
            return content;
        }
    }

    public bool IsClear
    {
        get
        {
            return isPullerClear;
        }
    }

    private void Awake()
    {
        content = transform.Find("content");
        puller = transform.Find("puller");
        pullerEnd = content.Find("puller_end");
        pullTweeners = new LinkedList<Tweener>();
    }

    private void Start()
    {
        Stage.RegisterNewPuller(this);
    }

    public override void Pull(GameElement element)
    {
        if (isStartedToPull)
        {
            return;
        }

        isStartedToPull = true;
        float distance = Vector3.Distance(content.localPosition, -pullerEnd.localPosition);

        Vector3 to = -pullerEnd.localPosition;
        float time = distance / 5;

        TweenCallback onCompleteCallback = () =>
        {
            isHidden = true;
            Destroy(content.gameObject);
        };

        Tweener pull = content.DOLocalMove(to, time).OnComplete(onCompleteCallback).SetEase(Ease.Linear);

        Tweener scaleUp = puller.transform.DOScale(Vector3.one + Vector3.up, 0.25f);
        Tweener scaleDown = puller.transform.DOScale(Vector3.one, 0.25f).SetDelay(time);

        pullTweeners.AddLast(pull);
        pullTweeners.AddLast(scaleUp);
        pullTweeners.AddLast(scaleDown);
    }

    public void BreakPull()
    {
        if (isBreaked)
        {
            return;
        }

        isBreaked = true;

        foreach (Tweener pullTweener in pullTweeners)
        {
            pullTweener.Kill();
        }

        pullTweeners.Clear();

        if (isStartedToPull)
        {
            Vector3 from = content.localPosition;
            Vector3 to = -pullerEnd.localPosition;
            Vector3 dif = -(to - from).normalized * 0.125f;
            to = from + dif;

            TweenCallback onCompleteCallback = () =>
            {
                content.DOLocalMove(from, 0.1f);
            };

            content.DOLocalMove(to, 0.2f).OnComplete(onCompleteCallback);
        }

        Stage.FailStage();
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        isPullerClear = true;
        Hide();
    }

}