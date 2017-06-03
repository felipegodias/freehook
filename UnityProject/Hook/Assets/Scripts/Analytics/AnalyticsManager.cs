using System.Collections.Generic;
using MGS.EventManager;
#if UNITY_EDITOR
using UnityEditor.Analytics;
#endif
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsManager : MonoBehaviour {

    private const string ON_STAGE_COMPLETE_EVENT_NAME = "stage.complete";

    private const string ON_STAGE_FAIL_EVENT_NAME = "stage.fail";

    private const string STAGE_ARGS_NAME = "stage";

    private void Awake() {
#if UNITY_EDITOR
        AnalyticsSettings.enabled = true;
        AnalyticsSettings.testMode = true;
#endif
        EventManager.AddListener<OnApplicationStart>(this.OnApplicationStart);
        EventManager.AddListener<OnStageCompleted>(this.OnStageCompleted);
        EventManager.AddListener<OnStageFail>(this.OnStageFail);
        Analytics.enabled = true;
        Analytics.SetUserId(SystemInfo.deviceUniqueIdentifier);
    }

    private void OnApplicationStart(object sender, OnApplicationStart eventArgs) {
    }

    private void OnStageCompleted(object sender, OnStageCompleted onStageCompleted) {
        Debug.Log("OnStageCompleted");
        IDictionary<string, object> eventArgs = new Dictionary<string, object>();
        eventArgs.Add(STAGE_ARGS_NAME, onStageCompleted.Stage);
        Analytics.CustomEvent(ON_STAGE_COMPLETE_EVENT_NAME, eventArgs);
    }

    private void OnStageFail(object sender, OnStageFail onStageFail) {
        Debug.Log("OnStageFail");
        IDictionary<string, object> eventArgs = new Dictionary<string, object>();
        eventArgs.Add(STAGE_ARGS_NAME, onStageFail.Stage);
        Analytics.CustomEvent(ON_STAGE_FAIL_EVENT_NAME, eventArgs);
    }

    private void OnDestroy() {
        EventManager.RemoveListener<OnStageCompleted>(this.OnStageCompleted);
        EventManager.RemoveListener<OnStageFail>(this.OnStageFail);
    }

}