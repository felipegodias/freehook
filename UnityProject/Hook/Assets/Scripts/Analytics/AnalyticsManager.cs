using System.Collections;
using MGS.EventManager;
using UnityEngine;
#if UNITY_EDITOR
#endif

public class AnalyticsManager : MonoBehaviour {

    private IAnalytics[] analytics;

    private void Awake() {
        this.analytics = new IAnalytics[] {new UnityAnalytics()};
        EventManager.AddListener<OnApplicationStart>(this.OnApplicationStart);
        EventManager.AddListener<OnStageCompleted>(this.OnStageCompleted);
        EventManager.AddListener<OnStageFail>(this.OnStageFail);
    }

    private void Start() {
        this.StartCoroutine(this.FlushEvents());
    }

    private IEnumerator FlushEvents() {
        foreach (IAnalytics analytics in this.analytics) {
            analytics.FlushEvents();
        }
        yield return new WaitForSeconds(1);
    }

    private void OnApplicationStart(object sender, OnApplicationStart eventArgs) {
        foreach (IAnalytics analytics in this.analytics) {
            analytics.OnApplicationStart();
        }
    }

    private void OnStageCompleted(object sender, OnStageCompleted eventArgs) {
        int stage = eventArgs.Stage;
        foreach (IAnalytics analytics in this.analytics) {
            analytics.OnStageCompleted(stage);
        }
    }

    private void OnStageFail(object sender, OnStageFail eventArgs) {
        int stage = eventArgs.Stage;
        foreach (IAnalytics analytics in this.analytics) {
            analytics.OnStageFail(stage);
        }
    }

    private void OnDestroy() {
        EventManager.RemoveListener<OnApplicationStart>(this.OnApplicationStart);
        EventManager.RemoveListener<OnStageCompleted>(this.OnStageCompleted);
        EventManager.RemoveListener<OnStageFail>(this.OnStageFail);
    }

}