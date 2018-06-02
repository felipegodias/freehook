using MGS.EventManager;

using UnityEngine;

[CreateAssetMenu(menuName = "Achievements/StageCompleted")]
public class StageCompletedAchievement : Achievement
{

    [SerializeField]
    private int m_value;

    public override void Init()
    {
        EventManager.AddListener<OnStageCompleted>(OnStageCompleted);
    }

    private void OnStageCompleted(object sender, OnStageCompleted eventArgs)
    {
        if (eventArgs.Stage < m_value)

        {
            return;
        }

        CompleteAchievement();
    }

}