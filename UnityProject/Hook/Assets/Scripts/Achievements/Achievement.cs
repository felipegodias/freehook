using MGS.EventManager;

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
        Debug.LogFormat("Trying to complete achievement {0}.", AchievementKey);
        IAchievement achievement = AchievementManager.Instance.GetAchievement(AchievementKey);
        if (achievement == null)
        {
            return;
        }

        if (achievement.completed)
        {
            return;
        }

        //Debug.LogFormat("Trying to complete achievement {0}.", AchievementKey);
        Social.ReportProgress(AchievementKey, 100, OnAchievementReported);
    }

    private void OnAchievementReported(bool success)
    {
        Debug.LogFormat("Achievement with id {0} was completed with the result {1}.", AchievementKey, success);
        if (!success)
        {
            return;
        }

        var onAchievementUnlocked = new OnAchievementUnlocked(m_achievement);
        EventManager.Dispatch(onAchievementUnlocked);
    }

}