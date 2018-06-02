using System;

using TMPro;

using UnityEngine;

public class UISpeedRunGameOverScreen : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI timeValueText;

    [SerializeField]
    private TextMeshProUGUI bestTimeValueText;

    private void Awake()
    {
        TimeSpan lastSpeedRunTimeSpan = Player.GetLastSpeedRunTime();
        TimeSpan bestSpeedRunTimeSpan = Player.GetBestSpeedRunTime();
        timeValueText.text = lastSpeedRunTimeSpan.ToChronometerString();
        bestTimeValueText.text = bestSpeedRunTimeSpan.ToChronometerString();
    }

}