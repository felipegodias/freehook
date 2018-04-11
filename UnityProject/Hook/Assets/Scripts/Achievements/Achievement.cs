using UnityEngine;
using UnityEngine.SocialPlatforms;

public abstract class Achievement : ScriptableObject
{

    [SerializeField]
    private string m_achievement;

    public string AchievementKey
    {
        get
        {
            return m_achievement;
        }
    }

    public abstract void Init();

    protected void CompleteAchievement()
    {
        IAchievement achievement = AchievementManager.Instance.GetAchievement(AchievementKey);
        if (achievement.completed)
        {
            return;
        }

        Social.ReportProgress(AchievementKey, 100, OnAchievementReported);
    }

    private void OnAchievementReported(bool success)
    {
        Debug.LogFormat("Achievement with id {0} was completed with the result {1}.", AchievementKey, success);
    }

}