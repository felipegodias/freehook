using MGS.EventManager;
using UnityEngine;

public class OnHeartsCountWasChanged : IEvent {

    private readonly int heartCount;

    public OnHeartsCountWasChanged(int heartCount) {
        Debug.Log(heartCount);
        this.heartCount = heartCount;
    }

    public int HeartCount {
        get { return this.heartCount; }
    }

}