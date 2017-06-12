using MGS.EventManager;

public class UILightButton : UIButton {

    protected override void OnClick() {
        bool isLightsOn = Player.IsLightSOn();
        Player.SetLightsOn(!isLightsOn);
        OnLightSwitch onLightSwitch = new OnLightSwitch();
        EventManager.Dispatch(onLightSwitch);
    }

}
