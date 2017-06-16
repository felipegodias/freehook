using System;
using MGS.EventManager;

public class OnSpeedRunEnd : IEvent {

    private readonly TimeSpan timeSpan;

    public OnSpeedRunEnd(TimeSpan timeSpan) {
        this.timeSpan = timeSpan;
    }

    public TimeSpan TimeSpan {
        get { return this.timeSpan; }
    }

}
