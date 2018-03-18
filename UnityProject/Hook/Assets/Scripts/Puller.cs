using System.Collections.Generic;

using DG.Tweening;

using UnityEngine;

public class Puller : GameElement {

    private Transform content;
    private Transform puller;
    private Transform pullerEnd;

    private bool isStartedToPull;

    private bool isPullerClear;

    private bool isBreaked;

    private LinkedList<Tweener> pullTweeners;

    public Transform Content {
        get { return this.content; }
    }

    public bool IsClear {
        get { return this.isPullerClear; }
    }

    private void Awake() {
        this.content = this.transform.Find("content");
        this.puller = this.transform.Find("puller");
        this.pullerEnd = this.content.Find("puller_end");
        this.pullTweeners = new LinkedList<Tweener>();
    }

    private void Start() {
        this.Stage.RegisterNewPuller(this);
    }

    public override void Pull(GameElement element) {
        if (this.isStartedToPull) {
            return;
        }
        this.isStartedToPull = true;
        float distance = Vector3.Distance(this.content.localPosition, -this.pullerEnd.localPosition);

        Vector3 to = -this.pullerEnd.localPosition;
        float time = distance / 5;

        TweenCallback onCompleteCallback = () =>
        {
            this.isHidden = true;
            Destroy(this.content.gameObject);
        };

        Tweener pull = this.content.DOLocalMove(to, time).OnComplete(onCompleteCallback).SetEase(Ease.Linear);

        Tweener scaleUp = this.puller.transform.DOScale(Vector3.one + Vector3.up, 0.25f);
        Tweener scaleDown = this.puller.transform.DOScale(Vector3.one, 0.25f).SetDelay(time);

        pullTweeners.AddLast(pull);
        pullTweeners.AddLast(scaleUp);
        pullTweeners.AddLast(scaleDown);
    }

    public void BreakPull() {
        if (this.isBreaked) {
            return;
        }

        this.isBreaked = true;

        foreach (Tweener pullTweener in pullTweeners)
        {
            pullTweener.Kill();
        }
        pullTweeners.Clear();

        if (this.isStartedToPull) {
            Vector3 from = this.content.localPosition;
            Vector3 to = -this.pullerEnd.localPosition;
            Vector3 dif = -(to - from).normalized * 0.125f;
            to = from + dif;

            TweenCallback onCompleteCallback = () =>
            {
                this.content.DOLocalMove(from, 0.1f);
            };

            this.content.DOLocalMove(to, 0.2f).OnComplete(onCompleteCallback);
        }

        this.Stage.FailStage();
    }

    private void OnTriggerEnter2D(Collider2D collider2D) {
        this.isPullerClear = true;
        this.Hide();
    }

}
