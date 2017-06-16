using UnityEngine;

public class UILeaderboardButton : UIButton {

    protected override void OnClick() {
        if (Social.Active != null) {
            Social.ShowLeaderboardUI();
        }
    }
}
