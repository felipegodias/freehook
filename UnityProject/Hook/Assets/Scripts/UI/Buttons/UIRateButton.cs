public class UIRateButton : UIButton {

    private void Awake() {
        int lastStage = Player.GetLastStage();
        if (lastStage < 24) {
            this.gameObject.SetActive(false);
        }
    }

    protected override void OnClick() {}

}
