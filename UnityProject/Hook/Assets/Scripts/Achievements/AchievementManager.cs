using System.Linq;

using UnityEngine;
using UnityEngine.SocialPlatforms;

public class AchievementManager : MonoBehaviour
{

    private static AchievementManager m_instance;

    [SerializeField]
    private Achievement[] m_achievementsObjs;

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

    private void Start()
    {
        foreach (Achievement achievementsObj in m_achievementsObjs)
        {
            achievementsObj.Init();
        }
    }

    public IAchievement GetAchievement(string key)
    {
        return m_achievements == null ? null : m_achievements.FirstOrDefault(achievement => achievement.id == key);
    }

}