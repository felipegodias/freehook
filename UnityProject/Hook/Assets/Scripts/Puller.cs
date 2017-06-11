using UnityEngine;

public class Puller : GameElement {

    private Transform content;
    private Transform puller;
    private Transform pullerEnd;

    private bool isStartedToPull;

    private bool isPullerClear;

    private bool isBreaked;

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
        Vector3 from = this.content.localPosition;
        Vector3 to = -this.pullerEnd.localPosition;
        Vector3 dif = to - from;
        float time = distance / 5;
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
        if (this.isBreaked) {
            return;
        }

        this.isBreaked = true;
        LeanTween.cancel(this.gameObject);

        if (this.isStartedToPull) {
            Vector3 from = this.content.localPosition;
            Vector3 to = -this.pullerEnd.localPosition;
            Vector3 dif = -(to - from).normalized * 0.125f;
            LeanTween.value(this.gameObject, f => {
                this.content.localPosition = from + dif * f;
            }, 0, 1, 0.2f).setOnComplete(() => {
                LeanTween.value(this.gameObject, f => {
                    this.content.localPosition = from + dif * (1 - f);
                }, 0, 1, 0.1f);
            });
        }

        this.Stage.FailStage();
    }

    private void OnTriggerEnter2D(Collider2D collider2D) {
        this.isPullerClear = true;
        this.Hide();
    }

}
