using MGS.EventManager;

public class OnHeartsCountWasChanged : IEvent {

    private readonly int heartCount;

    public OnHeartsCountWasChanged(int heartCount) {
        this.heartCount = heartCount;
    }

    public int HeartCount {
        get { return this.heartCount; }
    }

}