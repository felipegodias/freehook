using System.Collections;

using MGS.EventManager;

using UnityEngine;
using UnityEngine.Advertisements;

public class AnalyticsManager : MonoBehaviour
{

    private IAnalytics[] analytics;

    private void Awake()
    {
#if UNITY_EDITOR
        analytics = new IAnalytics[0];
#else
        this.analytics = new IAnalytics[] {new UnityAnalytics()};
#endif

        if (Debug.isDebugBuild)
        {
            analytics = new IAnalytics[0];
        }

        EventManager.AddListener<OnFirstInteraction>(OnFirstInteraction);
        EventManager.AddListener<OnWatchAdsCompleted>(OnWatchAdsCompleted);
        EventManager.AddListener<OnWatchAdsStarted>(OnWatchAdsStarted);
        EventManager.AddListener<OnApplicationStart>(OnApplicationStart);
        EventManager.AddListener<OnStageCompleted>(OnStageCompleted);
        EventManager.AddListener<OnStageFail>(OnStageFail);
        EventManager.AddListener<OnSpeedRunEnd>(OnSpeedRunEnd);
        EventManager.AddListener<OnRemoveAdsButtonClicked>(OnRemoveAdsButtonClicked);
        EventManager.AddListener<OnShowAdsScreen>(OnShowAdsScreen);
    }

    private void Start()
    {
        StartCoroutine(FlushEvents());
    }

    private IEnumerator FlushEvents()
    {
        while (true)
        {
            foreach (IAnalytics analytics in this.analytics)
            {
                analytics.FlushEvents();
            }

            yield return new WaitForSeconds(20);
        }
    }

    private void OnFirstInteraction(object sender, OnFirstInteraction eventArgs)
    {
        foreach (IAnalytics analytics in this.analytics)
        {
            analytics.OnFirstInteraction(eventArgs.FirstInteraction);
        }
    }

    private void OnWatchAdsStarted(object sender, OnWatchAdsStarted eventArgs)
    {
        foreach (IAnalytics analytics in this.analytics)
        {
            analytics.OnWatchAdsStart();
        }
    }

    private void OnWatchAdsCompleted(object sender, OnWatchAdsCompleted eventArgs)
    {
        ShowResult result = eventArgs.Result;
        foreach (IAnalytics analytics in this.analytics)
        {
            analytics.OnWatchAdsComplete(result);
        }
    }

    private void OnApplicationStart(object sender, OnApplicationStart eventArgs)
    {
        foreach (IAnalytics analytics in this.analytics)
        {
            analytics.OnApplicationStart();
        }
    }

    private void OnStageCompleted(object sender, OnStageCompleted eventArgs)
    {
        int stage = eventArgs.Stage;
        if (stage < Player.GetLastStage())
        {
            return;
        }

        foreach (IAnalytics analytics in this.analytics)
        {
            analytics.OnStageCompleted(stage);
        }
    }

    private void OnStageFail(object sender, OnStageFail eventArgs)
    {
        int stage = eventArgs.Stage;
        if (stage < Player.GetLastStage())
        {
            return;
        }

        foreach (IAnalytics analytics in this.analytics)
        {
            analytics.OnStageFail(stage);
        }
    }

    private void OnSpeedRunEnd(object sender, OnSpeedRunEnd eventargs)
    {
        foreach (IAnalytics analytics in this.analytics)
        {
            analytics.OnSpeedRunEnd(eventargs.TimeSpan);
        }
    }

    private void OnRemoveAdsButtonClicked(object sender, OnRemoveAdsButtonClicked eventargs)
    {
        foreach (IAnalytics analytics in this.analytics)
        {
            analytics.OnRemoveAdsButtonClicked();
        }
    }

    private void OnShowAdsScreen(object sender, OnShowAdsScreen eventargs)
    {
        foreach (IAnalytics analytics in this.analytics)
        {
            analytics.OnShowAdsScreen();
        }
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<OnFirstInteraction>(OnFirstInteraction);
        EventManager.RemoveListener<OnWatchAdsStarted>(OnWatchAdsStarted);
        EventManager.RemoveListener<OnWatchAdsCompleted>(OnWatchAdsCompleted);
        EventManager.RemoveListener<OnApplicationStart>(OnApplicationStart);
        EventManager.RemoveListener<OnStageCompleted>(OnStageCompleted);
        EventManager.RemoveListener<OnStageFail>(OnStageFail);
        EventManager.RemoveListener<OnSpeedRunEnd>(OnSpeedRunEnd);
        EventManager.RemoveListener<OnRemoveAdsButtonClicked>(OnRemoveAdsButtonClicked);
        EventManager.RemoveListener<OnShowAdsScreen>(OnShowAdsScreen);
    }

}