﻿using System;
using System.Collections;
using MGS.EventManager;
using UnityEngine;
using UnityEngine.Advertisements;

public class AnalyticsManager : MonoBehaviour {

    private IAnalytics[] analytics;

    private void Awake() {
#if UNITY_EDITOR
        this.analytics = new IAnalytics[0];
#else
        this.analytics = new IAnalytics[] {new UnityAnalytics()};
#endif

        if (Debug.isDebugBuild) {
            this.analytics = new IAnalytics[0];
        }

        EventManager.AddListener<OnFirstInteraction>(this.OnFirstInteraction);
        EventManager.AddListener<OnWatchAdsCompleted>(this.OnWatchAdsCompleted);
        EventManager.AddListener<OnWatchAdsStarted>(this.OnWatchAdsStarted);
        EventManager.AddListener<OnApplicationStart>(this.OnApplicationStart);
        EventManager.AddListener<OnStageCompleted>(this.OnStageCompleted);
        EventManager.AddListener<OnStageFail>(this.OnStageFail);
        EventManager.AddListener<OnSpeedRunEnd>(this.OnSpeedRunEnd);
        EventManager.AddListener<OnRemoveAdsButtonClicked>(this.OnRemoveAdsButtonClicked);
        EventManager.AddListener<OnShowAdsScreen>(this.OnShowAdsScreen);
    }

    private void Start() {
        this.StartCoroutine(this.FlushEvents());
    }

    private IEnumerator FlushEvents() {
        while (true) {
            foreach (IAnalytics analytics in this.analytics) {
                analytics.FlushEvents();
            }
            yield return new WaitForSeconds(20);
        }
    }

    private void OnFirstInteraction(object sender, OnFirstInteraction eventArgs) {
        foreach (IAnalytics analytics in this.analytics) {
            analytics.OnFirstInteraction(eventArgs.FirstInteraction);
        }
    }
    private void OnWatchAdsStarted(object sender, OnWatchAdsStarted eventArgs) {
        foreach (IAnalytics analytics in this.analytics) {
            analytics.OnWatchAdsStart();
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
        if (stage < Player.GetLastStage()) {
            return;
        }
        foreach (IAnalytics analytics in this.analytics) {
            analytics.OnStageCompleted(stage);
        }
    }

    private void OnStageFail(object sender, OnStageFail eventArgs) {
        int stage = eventArgs.Stage;
        if (stage < Player.GetLastStage()) {
            return;
        }
        foreach (IAnalytics analytics in this.analytics) {
            analytics.OnStageFail(stage);
        }
    }

    private void OnSpeedRunEnd(object sender, OnSpeedRunEnd eventargs) {
        foreach (IAnalytics analytics in this.analytics) {
            analytics.OnSpeedRunEnd(eventargs.TimeSpan);
        }
    }

    private void OnRemoveAdsButtonClicked(object sender, OnRemoveAdsButtonClicked eventargs) {
        foreach (IAnalytics analytics in this.analytics) {
            analytics.OnRemoveAdsButtonClicked();
        }
    }

    private void OnShowAdsScreen(object sender, OnShowAdsScreen eventargs) {
        foreach (IAnalytics analytics in this.analytics) {
            analytics.OnShowAdsScreen();
        }
    }

    private void OnDestroy() {
        EventManager.RemoveListener<OnFirstInteraction>(this.OnFirstInteraction);
        EventManager.RemoveListener<OnWatchAdsStarted>(this.OnWatchAdsStarted);
        EventManager.RemoveListener<OnWatchAdsCompleted>(this.OnWatchAdsCompleted);
        EventManager.RemoveListener<OnApplicationStart>(this.OnApplicationStart);
        EventManager.RemoveListener<OnStageCompleted>(this.OnStageCompleted);
        EventManager.RemoveListener<OnStageFail>(this.OnStageFail);
        EventManager.RemoveListener<OnSpeedRunEnd>(this.OnSpeedRunEnd);
        EventManager.RemoveListener<OnRemoveAdsButtonClicked>(this.OnRemoveAdsButtonClicked);
        EventManager.RemoveListener<OnShowAdsScreen>(this.OnShowAdsScreen);
    }

}