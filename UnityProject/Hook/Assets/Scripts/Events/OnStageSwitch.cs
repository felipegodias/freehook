using MGS.EventManager;

public class OnStageSwitch : IEvent
{

    private readonly int increment;

    public OnStageSwitch(int increment)
    {
        this.increment = increment;
    }

    public int Increment
    {
        get
        {
            return increment;
        }
    }

}