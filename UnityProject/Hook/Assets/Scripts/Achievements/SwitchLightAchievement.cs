using MGS.EventManager;

using UnityEngine;

[CreateAssetMenu(menuName = "Achievements/Switch Light")]
public class SwitchLightAchievement : Achievement
{

    public override void Init()
    {
        EventManager.AddListener<OnLightSwitch>(OnLightSwitch);
    }

    private void OnLightSwitch(object sender, OnLightSwitch eventargs)
    {
        CompleteAchievement();
    }

}