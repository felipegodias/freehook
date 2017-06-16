using MGS.EventManager;

public class UISpeedRunButton : UIButton {

    private void Awake() {
        int lastStage = Player.GetLastStage();
        if (lastStage < 24) {
            this.gameObject.SetActive(false);
        }
    }

    protected override void OnClick() {
        EventManager.Dispatch(new OnSpeedRunStart());
    }

}
