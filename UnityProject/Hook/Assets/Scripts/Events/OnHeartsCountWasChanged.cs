using MGS.EventManager;

public class OnHeartsCountWasChanged : IEvent
{

    private readonly int heartCount;

    private readonly bool isSpeedRunModeOn;

    public OnHeartsCountWasChanged(int heartCount, bool isSpeedRunModeOn)
    {
        this.heartCount = heartCount;
        this.isSpeedRunModeOn = isSpeedRunModeOn;
    }

    public int HeartCount
    {
        get
        {
            return heartCount;
        }
    }

    public bool IsSpeedRunModeOn
    {
        get
        {
            return isSpeedRunModeOn;
        }
    }

}