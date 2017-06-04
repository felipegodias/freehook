using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Analytics;
#if UNITY_EDITOR
using UnityEditor.Analytics;

#endif

public class UnityAnalytics : IAnalytics {

    private const string ON_WATCH_ADS_COMPLETED = "hook.ads.completed";

    private const string ON_APPLICATION_START_EVENT_NAME = "hook.aplication.start";

    private const string ON_STAGE_COMPLETE_EVENT_NAME = "hook.stage.complete";

    private const string ON_STAGE_FAIL_EVENT_NAME = "hook.stage.fail";

    private const string STAGE_ARGS_NAME = "stage";

    private const string ADS_RESULT_ARGS_NAME = "result";

    public UnityAnalytics() {
#if UNITY_EDITOR
        AnalyticsSettings.enabled = true;
        AnalyticsSettings.testMode = true;
#endif
        Analytics.enabled = true;
        Analytics.SetUserId(SystemInfo.deviceUniqueIdentifier);
    }

    public void OnWatchAdsComplete(ShowResult result) {
        IDictionary<string, object> eventArgs = new Dictionary<string, object>();
        eventArgs.Add(ADS_RESULT_ARGS_NAME, result.ToString().ToLower());
        AnalyticsResult analyticsResult = Analytics.CustomEvent(ON_WATCH_ADS_COMPLETED, eventArgs);
        Debug.Log("OnWatchAdsComplete -> " + analyticsResult);
    }

    public void OnApplicationStart() {
        IDictionary<string, object> eventArgs = new Dictionary<string, object>();
        AnalyticsResult analyticsResult = Analytics.CustomEvent(ON_APPLICATION_START_EVENT_NAME, eventArgs);
        Debug.Log("OnApplicationStart -> " + analyticsResult);
    }

    public void OnStageCompleted(int stage) {
        IDictionary<string, object> eventArgs = new Dictionary<string, object>();
        eventArgs.Add(STAGE_ARGS_NAME, stage);
        AnalyticsResult analyticsResult = Analytics.CustomEvent(ON_STAGE_COMPLETE_EVENT_NAME, eventArgs);
        Debug.Log("OnStageCompleted -> " + analyticsResult);
    }

    public void OnStageFail(int stage) {
        IDictionary<string, object> eventArgs = new Dictionary<string, object>();
        eventArgs.Add(STAGE_ARGS_NAME, stage);
        AnalyticsResult analyticsResult = Analytics.CustomEvent(ON_STAGE_FAIL_EVENT_NAME, eventArgs);
        Debug.Log("OnStageCompleted -> " + analyticsResult);
    }

    public void FlushEvents() {
        AnalyticsResult analyticsResult = Analytics.FlushEvents();
        Debug.Log("FlushEvents -> " + analyticsResult);
    }

}