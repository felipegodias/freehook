using MGS.EventManager;

public class UISpeedRunButton : UIButton {

    protected override void OnClick() {
        EventManager.Dispatch(new OnSpeedRunStart());
    }

}
