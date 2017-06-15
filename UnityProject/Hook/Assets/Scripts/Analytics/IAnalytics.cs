using System;
using UnityEngine.Advertisements;

public interface IAnalytics {

    void OnWatchAdsComplete(ShowResult result);

    void OnApplicationStart();

    void OnStageCompleted(int stage);

    void OnStageFail(int stage);

    void OnSpeedRunEnd(TimeSpan timeSpan);

    void OnRemoveAdsButtonClicked();

    void FlushEvents();
    

}
