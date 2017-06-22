using System;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine;

public class Player {

    private const string kDeviceId = "device_id";
    private const string kHeartCount = "heart_count";
    private const string kLastStage = "last_stage";
    private const string kLastPlayedStage = "last_played_stage";
    private const string kIsLightsOn = "is_lights_on";
    private const string kLastSpeedRunTime = "last_speed_run_time";
    private const string kBestSpeedRunTime = "best_speed_run_time";
    private const string kIsAdsEnabled = "is_ads_enabled";

    public static string GetDeviceId() {
        if (ObscuredPrefs.HasKey(kDeviceId)) {
            return ObscuredPrefs.GetString(kDeviceId);
        }
        string deviceId = SystemInfo.deviceUniqueIdentifier;
        ObscuredPrefs.SetString(kDeviceId, deviceId);
        return deviceId;
    }

    public static int GetHearts() {
        if (!IsAdsEnabled()) {
            return GameSettings.MAX_HEARTS;
        }
        int defaultMaxHearts = GameSettings.MAX_HEARTS;
        return ObscuredPrefs.GetInt(kHeartCount, defaultMaxHearts);
    }

    public static void SetHearts(int hearts) {
        ObscuredPrefs.SetInt(kHeartCount, hearts);
    }

    public static int GetLastStage() {
        return ObscuredPrefs.GetInt(kLastStage, 0);
    }

    public static void SetLastStage(int lastStage) {
        int stage = GetLastStage();
        if (lastStage > stage) {
            ObscuredPrefs.SetInt(kLastStage, lastStage);
        }
    }

    public static int GetLastPlayedStage() {
        return ObscuredPrefs.GetInt(kLastPlayedStage, 0);
    }

    public static void SetLastPlayedStage(int stage) {
        ObscuredPrefs.SetInt(kLastPlayedStage, stage);
    }

    public static bool IsLightSOn() {
        if (!Application.isPlaying) {
            return true;
        }
        return ObscuredPrefs.GetBool(kIsLightsOn, true);
    }

    public static void SetLightsOn(bool isLightsOn) {
        ObscuredPrefs.SetBool(kIsLightsOn, isLightsOn);
    }

    public static TimeSpan GetLastSpeedRunTime() {
        long ticks = ObscuredPrefs.GetLong(kLastSpeedRunTime, long.MaxValue);
        TimeSpan timeSpan = new TimeSpan(ticks);
        return timeSpan;
    }

    public static void SetLastSpeedRunTime(TimeSpan timeSpan) {
        ObscuredPrefs.SetLong(kLastSpeedRunTime, timeSpan.Ticks);
    }

    public static TimeSpan GetBestSpeedRunTime() {
        long ticks = ObscuredPrefs.GetLong(kBestSpeedRunTime, long.MaxValue);
        TimeSpan timeSpan = new TimeSpan(ticks);
        return timeSpan;
    }

    public static void SetBestSpeedRunTime(TimeSpan timeSpan) {
        ObscuredPrefs.SetLong(kBestSpeedRunTime, timeSpan.Ticks);
    }

    public static bool IsAdsEnabled() {
        return ObscuredPrefs.GetBool(kIsAdsEnabled, true);
    }

    public static void DisableAds() {
        ObscuredPrefs.SetBool(kIsAdsEnabled, false);
    }

}
