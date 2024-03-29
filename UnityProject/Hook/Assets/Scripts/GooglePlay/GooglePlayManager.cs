﻿using GooglePlayGames;
using GooglePlayGames.BasicApi;
using MGS.EventManager;
using UnityEngine;

public class GooglePlayManager : MonoBehaviour {

    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
        EventManager.AddListener<OnSpeedRunEnd>(this.OnSpeedRunEnd);
    }

    private void OnSpeedRunEnd(object sender, OnSpeedRunEnd eventArgs) {
        Social.ReportScore((long)eventArgs.TimeSpan.TotalMilliseconds, GPGSIds.leaderboard_speedrun, null);
    }

    private void Start() {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        // requests a server auth code be generated so it can be passed to an
        //  associated back end server application and exchanged for an OAuth token.
        //.RequestServerAuthCode(false)
        // requests an ID token be generated.  This OAuth token can be used to
        //  identify the player to other services such as Firebase.
        //.RequestIdToken()
        .Build();

        PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        //PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();

        Social.localUser.Authenticate((bool success) => {
            // handle success or failure
        });
    }

}
