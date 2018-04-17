using MGS.EventManager;

public class OnAchievementUnlocked : IEvent
{

    private readonly string achievementKey;

    public OnAchievementUnlocked(string achievementKey)
    {
        this.achievementKey = achievementKey;
    }

    public string AchievementKey
    {
        get
        {
            return achievementKey;
        }
    }

}