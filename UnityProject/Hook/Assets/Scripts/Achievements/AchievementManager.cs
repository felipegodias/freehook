using System.Linq;

using UnityEngine;
using UnityEngine.SocialPlatforms;

public class AchievementManager : MonoBehaviour
{

    private static AchievementManager m_instance;

    private IAchievement[] m_achievements;

    public static AchievementManager Instance
    {
        get
        {
            return m_instance;
        }
    }

    public IAchievement[] Achievements
    {
        get
        {
            return m_achievements;
        }
    }

    private void Awake()
    {
        m_instance = this;
    }

    public IAchievement GetAchievement(string key)
    {
        return m_achievements.First(achievement => achievement.id == key);
    }

}