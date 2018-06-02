using MGS.EventManager;

public class OnStageCompleted : IEvent
{

    private readonly int stage;

    public OnStageCompleted(int stage)
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