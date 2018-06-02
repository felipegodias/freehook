using MGS.EventManager;

public class OnStageLoaded : IEvent
{

    private readonly int stage;

    public OnStageLoaded(int stage)
    {
        this.stage = stage;
    }

    public int Stage
    {
        get
        {
            return stage;
        }
    }

}