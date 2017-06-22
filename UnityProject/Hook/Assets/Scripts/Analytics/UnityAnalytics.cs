using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Analytics;
#if UNITY_EDITOR
using UnityEditor.Analytics;

#endif

public class UnityAnalytics : IAnalytics {

    private const string kOnFirstInteraction = "hook.first_interaction";

    private const string kOnWatchAdsStarted = "hook.ads.started";

    private const string kOnWatchAdsCompleted = "hook.ads.completed";

    private const string kOnApplicationStart = "hook.aplication.start";

    private const string kOnStageComplete = "hook.stage.complete";

    private const string kOnStageFail = "hook.stage.fail";

    private const string kOnSpeedRunEnd = "hook.speed_run.end";

    private const string kOnRemoveAdsButtonClicked = "hook.remove_ads.button_click";

    private const string kStage = "stage";

    private const string kResult = "result";

    private const string kTime = "time";

    private const string kType = "type";

    public UnityAnalytics() {
        Analytics.enabled = true;
        string deviceId = Player.GetDeviceId();
        Analytics.SetUserId(deviceId);
    }

    public void OnFirstInteraction(FirstInteraction firstInteraction) {
        IDictionary<string, object> eventArgs = new Dictionary<string, object>();
        eventArgs.Add(kType, firstInteraction.ToString().ToLower());
        Analytics.CustomEvent(kOnFirstInteraction, eventArgs);
    }

    public void OnWatchAdsStart() {
        IDictionary<string, object> eventArgs = new Dictionary<string, object>();
        Analytics.CustomEvent(kOnWatchAdsStarted, eventArgs);
    }

    public void OnWatchAdsComplete(ShowResult result) {
        IDictionary<string, object> eventArgs = new Dictionary<string, object>();
        eventArgs.Add(kResult, result.ToString().ToLower());
        Analytics.CustomEvent(kOnWatchAdsCompleted, eventArgs);
    }

    public void OnApplicationStart() {
        IDictionary<string, object> eventArgs = new Dictionary<string, object>();
        Analytics.CustomEvent(kOnApplicationStart, eventArgs);
    }

    public void OnStageCompleted(int stage) {
        IDictionary<string, object> eventArgs = new Dictionary<string, object>();
        eventArgs.Add(kStage, stage);
        Analytics.CustomEvent(kOnStageComplete, eventArgs);
    }

    public void OnStageFail(int stage) {
        IDictionary<string, object> eventArgs = new Dictionary<string, object>();
        eventArgs.Add(kStage, stage);
        Analytics.CustomEvent(kOnStageFail, eventArgs);
    }

    public void OnSpeedRunEnd(TimeSpan timeSpan) {
        IDictionary<string, object> eventArgs = new Dictionary<string, object>();
        eventArgs.Add(kTime, timeSpan);
        Analytics.CustomEvent(kOnSpeedRunEnd, eventArgs);
    }

    public void OnRemoveAdsButtonClicked() {
        IDictionary<string, object> eventArgs = new Dictionary<string, object>();
        Analytics.CustomEvent(kOnRemoveAdsButtonClicked, eventArgs);
    }

    public void FlushEvents() {
        Analytics.FlushEvents();
    }



}