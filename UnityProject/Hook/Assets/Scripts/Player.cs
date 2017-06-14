using System;
using UnityEngine;

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
        if (!Application.isPlaying) {
            return true;
        }
        int value = PlayerPrefs.GetInt("IS_LIGHTS_ON", 1);
        return value == 1;
    }

    public static void SetLightsOn(bool isLightsOn) {
        PlayerPrefs.SetInt("IS_LIGHTS_ON", isLightsOn ? 1 : 0);
    }

    public static TimeSpan GetLastSpeedRunTime() {
        string rawValue = PlayerPrefs.GetString("LAST_SPEED_RUN_TIME", long.MaxValue.ToString());
        long ticks = long.Parse(rawValue);
        TimeSpan timeSpan = new TimeSpan(ticks);
        return timeSpan;
    }

    public static void SetLastSpeedRunTime(TimeSpan timeSpan) {
        PlayerPrefs.SetString("LAST_SPEED_RUN_TIME", timeSpan.Ticks.ToString());
    }

    public static TimeSpan GetBestSpeedRunTime() {
        string rawValue = PlayerPrefs.GetString("BEST_SPEED_RUN_TIME", long.MaxValue.ToString());
        long ticks = long.Parse(rawValue);
        TimeSpan timeSpan = new TimeSpan(ticks);
        return timeSpan;
    }

    public static void SetBestSpeedRunTime(TimeSpan timeSpan) {
        PlayerPrefs.SetString("BEST_SPEED_RUN_TIME", timeSpan.Ticks.ToString());
    }

}
