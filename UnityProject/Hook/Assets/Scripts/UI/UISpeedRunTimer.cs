using TMPro;
using UnityEngine;

public class UISpeedRunTimer : MonoBehaviour {

    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private TextMeshProUGUI text;

    private void Update() {
        this.text.text = this.gameManager.SpeedRunTimeSpan.ToString();
    }

}
