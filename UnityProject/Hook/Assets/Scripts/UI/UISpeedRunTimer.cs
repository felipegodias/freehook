using System;
using TMPro;
using UnityEngine;

public class UISpeedRunTimer : MonoBehaviour {

    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private TextMeshProUGUI text;

    private void Update() {
        TimeSpan timeSpan = this.gameManager.SpeedRunTimeSpan;
        int hours = timeSpan.Hours;
        int minutes = timeSpan.Minutes;
        int seconds = timeSpan.Seconds;
        int milliseconds = timeSpan.Milliseconds;
        string text = string.Format("{0:D2}:{1:D2}:{2:D2},{3}", hours, minutes, seconds, milliseconds.ToString("D2").Substring(0, 2));
        this.text.text = text;
    }

}
