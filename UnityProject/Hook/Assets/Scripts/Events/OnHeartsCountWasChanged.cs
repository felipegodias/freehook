using MGS.EventManager;
using UnityEngine;

public class OnHeartsCountWasChanged : IEvent {

    private readonly int heartCount;

    private readonly bool isSpeedRunModeOn;

    public OnHeartsCountWasChanged(int heartCount, bool isSpeedRunModeOn) {
        this.heartCount = heartCount;
        this.isSpeedRunModeOn = isSpeedRunModeOn;
    }

    public int HeartCount {
        get { return this.heartCount; }
    }

    public bool IsSpeedRunModeOn {
        get { return this.isSpeedRunModeOn; }
    }

}