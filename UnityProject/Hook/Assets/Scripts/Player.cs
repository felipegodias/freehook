using System;

using CodeStage.AntiCheat.ObscuredTypes;

using UnityEngine;

public class Player
{

    private const string kDeviceId = "device_id";
    private const string kFirstInteraction = "first_interaction";
    private const string kHeartCount = "heart_count";
    private const string kLastStage = "last_stage";
    private const string kLastPlayedStage = "last_played_stage";
    private const string kIsLightsOn = "is_lights_on";
    private const string kLastSpeedRunTime = "last_speed_run_time";
    private const string kBestSpeedRunTime = "best_speed_run_time";
    private const string kIsAdsEnabled = "is_ads_enabled";
    private const string kAdsCompletedCount = "ads_completed_count";
    private const string k_StageTutorialKey = "tutorial_{0}";

    public static string GetDeviceId()
    {
        if (ObscuredPrefs.HasKey(kDeviceId))
        {
            return ObscuredPrefs.GetString(kDeviceId);
        }

        string deviceId = SystemInfo.deviceUniqueIdentifier;
        ObscuredPrefs.SetString(kDeviceId, deviceId);
        return deviceId;
    }

    public static bool HasFirstInteraction()
    {
        return ObscuredPrefs.GetBool(kFirstInteraction, false);
    }

    public static void SetFirstInteraction(bool value)
    {
        ObscuredPrefs.SetBool(kFirstInteraction, value);
    }

    public static int GetHearts()
    {
        if (!IsAdsEnabled())
        {
            return GameSettings.MAX_HEARTS;
        }

        int defaultMaxHearts = GameSettings.MAX_HEARTS;
        return ObscuredPrefs.GetInt(kHeartCount, defaultMaxHearts);
    }

    public static void SetHearts(int hearts)
    {
        ObscuredPrefs.SetInt(kHeartCount, hearts);
    }

    public static int GetLastStage()
    {
        return ObscuredPrefs.GetInt(kLastStage, 0);
    }

    public static void SetLastStage(int lastStage)
    {
        int stage = GetLastStage();
        if (lastStage > stage)
        {
            ObscuredPrefs.SetInt(kLastStage, lastStage);
        }
    }

    public static int GetLastPlayedStage()
    {
        return ObscuredPrefs.GetInt(kLastPlayedStage, 0);
    }

    public static void SetLastPlayedStage(int stage)
    {
        ObscuredPrefs.SetInt(kLastPlayedStage, stage);
    }

    public static bool IsLightSOn()
    {
        if (!Application.isPlaying)
        {
            return true;
        }

        return ObscuredPrefs.GetBool(kIsLightsOn, true);
    }

    public static void SetLightsOn(bool isLightsOn)
    {
        ObscuredPrefs.SetBool(kIsLightsOn, isLightsOn);
    }

    public static TimeSpan GetLastSpeedRunTime()
    {
        long ticks = ObscuredPrefs.GetLong(kLastSpeedRunTime, long.MaxValue);
        var timeSpan = new TimeSpan(ticks);
        return timeSpan;
    }

    public static void SetLastSpeedRunTime(TimeSpan timeSpan)
    {
        ObscuredPrefs.SetLong(kLastSpeedRunTime, timeSpan.Ticks);
    }

    public static TimeSpan GetBestSpeedRunTime()
    {
        long ticks = ObscuredPrefs.GetLong(kBestSpeedRunTime, long.MaxValue);
        var timeSpan = new TimeSpan(ticks);
        return timeSpan;
    }

    public static void SetBestSpeedRunTime(TimeSpan timeSpan)
    {
        ObscuredPrefs.SetLong(kBestSpeedRunTime, timeSpan.Ticks);
    }

    public static bool IsAdsEnabled()
    {
        return ObscuredPrefs.GetBool(kIsAdsEnabled, true);
    }

    public static void DisableAds()
    {
        ObscuredPrefs.SetBool(kIsAdsEnabled, false);
    }

    public static int GetAdsCompletedCount()
    {
        return ObscuredPrefs.GetInt(kAdsCompletedCount, 0);
    }

    public static void SetAdsCompletedCount(int count)
    {
        ObscuredPrefs.SetInt(kAdsCompletedCount, count);
    }

    public static bool IsTutorialDone(int stageNum)
    {
        string key = string.Format(k_StageTutorialKey, stageNum);
        bool isTutorialDone = ObscuredPrefs.GetBool(key, false);
        return isTutorialDone;
    }

    public static void SetTutorialAsDone(int stageNum)
    {
        string key = string.Format(k_StageTutorialKey, stageNum);
        ObscuredPrefs.SetBool(key, true);
    }

}