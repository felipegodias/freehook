using MGS.EventManager;

public class OnRateGame : IEvent
{

    private readonly int stars;

    public OnRateGame(int stars)
    {
        this.stars = stars;
    }

    public int Stars
    {
        get
        {
            return stars;
        }
    }

}