using System;

using TMPro;

using UnityEngine;

public class UISpeedRunTimer : MonoBehaviour
{

    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private TextMeshProUGUI text;

    private void Update()
    {
        TimeSpan timeSpan = gameManager.SpeedRunTimeSpan;
        text.text = timeSpan.ToChronometerString();
    }

}