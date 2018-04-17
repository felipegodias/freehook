using System.Linq;

using MGS.EventManager;

using UnityEngine;
using UnityEngine.SocialPlatforms;

[CreateAssetMenu(menuName = "Achievements/Unlock All Achievements")]
public class AllUnlockedAchievement : Achievement
{

    public override void Init()
    {
        EventManager.AddListener<OnAchievementUnlocked>(OnAchievementUnlocked);
    }

    private void OnAchievementUnlocked(object sender, OnAchievementUnlocked eventArgs)
    {
        AchievementManager achievementManager = AchievementManager.Instance;
        IAchievement[] achievements = achievementManager.Achievements;
        int completedCount = achievements.Count(achievement => achievement.completed);
        if (completedCount == achievements.Length - 1)
        {
            CompleteAchievement();
        }
    }

}