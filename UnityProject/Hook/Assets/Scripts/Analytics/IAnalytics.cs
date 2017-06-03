public interface IAnalytics {

    void OnApplicationStart();

    void OnStageCompleted(int stage);

    void OnStageFail(int stage);

    void FlushEvents();

}
