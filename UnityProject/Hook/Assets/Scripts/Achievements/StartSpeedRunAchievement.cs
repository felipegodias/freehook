using MGS.EventManager;

using UnityEngine;

[CreateAssetMenu(menuName = "Achievements/Start Speed Run")]
public class StartSpeedRunAchievement : Achievement
{

    public override void Init()
    {
        EventManager.AddListener<OnSpeedRunStart>(OnSpeedRunStart);
    }

    private void OnSpeedRunStart(object sender, OnSpeedRunStart eventArgs)
    {
        CompleteAchievement();
    }

}