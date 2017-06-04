﻿using UnityEngine;

public class Puller : GameElement {

    private Transform content;
    private Transform puller;
    private Transform pullerEnd;

    private Stage stage;

    private bool isStartedToPull;

    private bool isClear;

    public Transform Content {
        get { return this.content; }
    }

    public override bool IsClear {
        get { return this.isClear; }
    }

    private void Awake() {
        this.stage = this.GetComponentInParent<Stage>();
        this.content = this.transform.Find("content");
        this.puller = this.transform.Find("puller");
        this.pullerEnd = this.content.Find("puller_end");
    }

    private void Start() {
        this.stage.RegisterNewPuller(this);
    }

    public override void Pull(GameElement element) {
        if (this.isStartedToPull) {
            return;
        }

        this.previousElement = element;
        this.isStartedToPull = true;
        float distance = Vector3.Distance(this.content.localPosition, -this.pullerEnd.localPosition);
        Vector3 from = this.content.localPosition;
        Vector3 to = -this.pullerEnd.localPosition;
        Vector3 dif = to - from;
        float time = distance / 6;
        LeanTween.value(this.gameObject, f => {
            this.content.localPosition = from + (dif * f);
        }, 0, 1, time).setOnComplete(() => {
            Destroy(this.content.gameObject);
        });
        LeanTween.value(this.gameObject, f => {
            this.puller.transform.localScale = new Vector3(1, 1 + f, 1);
        }, 0, 1, 0.25f).setEase(LeanTweenType.easeOutSine);
        LeanTween.value(this.gameObject, f => {
            this.puller.transform.localScale = new Vector3(1, 1 + (1 * (1 - f)), 1);
        }, 0, 1, 0.25f).setEase(LeanTweenType.easeOutSine).setDelay(time);
    }

    public void BreakPull() {
        LeanTween.cancel(this.gameObject);
        this.stage.FailStage();
    }

    private void OnTriggerEnter2D(Collider2D collider2D) {
        this.isClear = true;
        this.Hide();
        GameElement interator = this.previousElement;
        while (interator != null) {
            if (!interator.IsClear) {
                break;
            }
            interator.Hide();
            interator = interator.PreviousElement;
        }
    }

}
