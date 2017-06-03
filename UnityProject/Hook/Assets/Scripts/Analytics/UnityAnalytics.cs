using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
#if UNITY_EDITOR
using UnityEditor.Analytics;
#endif

public class UnityAnalytics : IAnalytics {

    private const string ON_APPLICATION_START_EVENT_NAME = "hook.aplication.start";

    private const string ON_STAGE_COMPLETE_EVENT_NAME = "hook.stage.complete";

    private const string ON_STAGE_FAIL_EVENT_NAME = "hook.stage.fail";

    private const string STAGE_ARGS_NAME = "stage";

    public UnityAnalytics() {
#if UNITY_EDITOR
        AnalyticsSettings.enabled = true;
        AnalyticsSettings.testMode = true;
#endif
        Analytics.enabled = true;
        Analytics.SetUserId(SystemInfo.deviceUniqueIdentifier);
    }

    public void OnApplicationStart() {
        IDictionary<string, object> eventArgs = new Dictionary<string, object>();
        Analytics.CustomEvent(ON_APPLICATION_START_EVENT_NAME, eventArgs);
    }

    public void OnStageCompleted(int stage) {
        IDictionary<string, object> eventArgs = new Dictionary<string, object>();
        eventArgs.Add(STAGE_ARGS_NAME, stage);
        Analytics.CustomEvent(ON_STAGE_COMPLETE_EVENT_NAME, eventArgs);
    }

    public void OnStageFail(int stage) {
        IDictionary<string, object> eventArgs = new Dictionary<string, object>();
        eventArgs.Add(STAGE_ARGS_NAME, stage);
        Analytics.CustomEvent(ON_STAGE_FAIL_EVENT_NAME, eventArgs);
    }

    public void FlushEvents() {
        Analytics.FlushEvents();
    }

}