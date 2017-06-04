using System.Collections;
using MGS.EventManager;
using UnityEngine;
using UnityEngine.Advertisements;

public class AnalyticsManager : MonoBehaviour {

    private IAnalytics[] analytics;

    private void Awake() {
        this.analytics = new IAnalytics[] {new UnityAnalytics()};
        EventManager.AddListener<OnWatchAdsCompleted>(this.OnWatchAdsCompleted);
        EventManager.AddListener<OnApplicationStart>(this.OnApplicationStart);
        EventManager.AddListener<OnStageCompleted>(this.OnStageCompleted);
        EventManager.AddListener<OnStageFail>(this.OnStageFail);
    }

    private void Start() {
        this.StartCoroutine(this.FlushEvents());
    }

    private IEnumerator FlushEvents() {
        while (true) {
            foreach (IAnalytics analytics in this.analytics) {
                analytics.FlushEvents();
            }
            yield return new WaitForSeconds(5);
        }
    }

    private void OnWatchAdsCompleted(object sender, OnWatchAdsCompleted eventArgs) {
        ShowResult result = eventArgs.Result;
        foreach (IAnalytics analytics in this.analytics) {
            analytics.OnWatchAdsComplete(result);
        }
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