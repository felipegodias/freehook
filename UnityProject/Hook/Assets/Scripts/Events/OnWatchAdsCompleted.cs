using MGS.EventManager;
using UnityEngine.Advertisements;

public class OnWatchAdsCompleted : IEvent {

    private readonly ShowResult result;

    public OnWatchAdsCompleted(ShowResult result) {
        this.result = result;
    }

    public ShowResult Result {
        get { return this.result; }
    }

}
