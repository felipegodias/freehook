﻿using UnityEngine;

public class Player {

    public static int GetHearts() {
        int defaultMaxHearts = GameSettings.MAX_HEARTS;
        return PlayerPrefs.GetInt("HEARTS", defaultMaxHearts);
    }

    public static void SetHearts(int hearts) {
        PlayerPrefs.SetInt("HEARTS", hearts);
    }

    public static int GetLastStage() {
        return PlayerPrefs.GetInt("LAST_STAGE", 0);
    }

    public static void SetLastStage(int lastStage) {
        int stage = GetLastStage();
        if (lastStage > stage) {
            PlayerPrefs.SetInt("LAST_STAGE", lastStage);
        }
    }

    public static int GetLastPlayedStage() {
        return PlayerPrefs.GetInt("LAST_PLAYED_STAGE", 0);
    }

    public static void SetLastPlayedStage(int stage) {
        PlayerPrefs.SetInt("LAST_PLAYED_STAGE", stage);
    }

    public static bool IsLightSOn() {
        int value = PlayerPrefs.GetInt("IS_LIGHTS_ON", 1);
        return value == 1;
    }

    public static void SetLightsOn(bool isLightsOn) {
        PlayerPrefs.SetInt("IS_LIGHTS_ON", isLightsOn ? 1 : 0);
    }

}
