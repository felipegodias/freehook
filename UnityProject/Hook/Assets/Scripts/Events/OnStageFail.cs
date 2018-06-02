using MGS.EventManager;

public class OnStageFail : IEvent
{

    private readonly int stage;

    public OnStageFail(int stage)
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