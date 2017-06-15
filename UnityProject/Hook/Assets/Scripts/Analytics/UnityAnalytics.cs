using System;
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

    private const string ON_SPEED_RUN_END_EVENT_NAME = "hook.speed_run.end";

    private const string ON_REMOVE_ADS_BUTTON_CLICKED_EVENT_NAME = "hook.remove_ads.button_click";

    private const string STAGE_ARGS_NAME = "stage";

    private const string ADS_RESULT_ARGS_NAME = "result";

    private const string SPEED_RUN_TIME_ARGS_NAME = "time";

    public UnityAnalytics() {
        Analytics.enabled = true;
        Analytics.SetUserId(SystemInfo.deviceUniqueIdentifier);
    }

    public void OnWatchAdsComplete(ShowResult result) {
        IDictionary<string, object> eventArgs = new Dictionary<string, object>();
        eventArgs.Add(ADS_RESULT_ARGS_NAME, result.ToString().ToLower());
        AnalyticsResult analyticsResult = Analytics.CustomEvent(ON_WATCH_ADS_COMPLETED, eventArgs);
    }

    public void OnApplicationStart() {
        IDictionary<string, object> eventArgs = new Dictionary<string, object>();
        AnalyticsResult analyticsResult = Analytics.CustomEvent(ON_APPLICATION_START_EVENT_NAME, eventArgs);
    }

    public void OnStageCompleted(int stage) {
        IDictionary<string, object> eventArgs = new Dictionary<string, object>();
        eventArgs.Add(STAGE_ARGS_NAME, stage);
        AnalyticsResult analyticsResult = Analytics.CustomEvent(ON_STAGE_COMPLETE_EVENT_NAME, eventArgs);
    }

    public void OnStageFail(int stage) {
        IDictionary<string, object> eventArgs = new Dictionary<string, object>();
        eventArgs.Add(STAGE_ARGS_NAME, stage);
        AnalyticsResult analyticsResult = Analytics.CustomEvent(ON_STAGE_FAIL_EVENT_NAME, eventArgs);
    }

    public void OnSpeedRunEnd(TimeSpan timeSpan) {
        IDictionary<string, object> eventArgs = new Dictionary<string, object>();
        eventArgs.Add(SPEED_RUN_TIME_ARGS_NAME, timeSpan);
        AnalyticsResult analyticsResult = Analytics.CustomEvent(ON_SPEED_RUN_END_EVENT_NAME, eventArgs);
    }

    public void OnRemoveAdsButtonClicked() {
        IDictionary<string, object> eventArgs = new Dictionary<string, object>();
        AnalyticsResult analyticsResult = Analytics.CustomEvent(ON_REMOVE_ADS_BUTTON_CLICKED_EVENT_NAME, eventArgs);
    }

    public void FlushEvents() {
        AnalyticsResult analyticsResult = Analytics.FlushEvents();
    }



}