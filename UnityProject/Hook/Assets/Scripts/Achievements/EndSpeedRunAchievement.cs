using MGS.EventManager;

using UnityEngine;

[CreateAssetMenu(menuName = "Achievements/End Speed Run")]
public class EndSpeedRunAchievement : Achievement
{

    [SerializeField]
    private long m_seconds;

    public override void Init()
    {
        EventManager.AddListener<OnSpeedRunEnd>(OnSpeedRunEnd);
    }

    private void OnSpeedRunEnd(object sender, OnSpeedRunEnd eventArgs)
    {
        if (eventArgs.TimeSpan.TotalSeconds >= m_seconds)
        {
            return;
        }

        CompleteAchievement();
    }

}