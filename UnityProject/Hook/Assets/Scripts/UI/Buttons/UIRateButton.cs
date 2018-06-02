using UnityEngine;

public class UIRateButton : UIButton
{

    private void Awake()
    {
        int lastStage = Player.GetLastStage();
        if (lastStage < 24)
        {
            gameObject.SetActive(false);
        }
    }

    protected override void OnClick()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.robinhoodlab.freehook");
    }

}