using System;
using System.Collections;
using System.Diagnostics;

using MGS.EventManager;

using UnityEngine;
using UnityEngine.SceneManagement;

using Debug = UnityEngine.Debug;

public class GameManager : MonoBehaviour
{

    private Stage currentStage;

    private string stageToLoad;

    private bool isFirstPlay = true;

    private bool isSpeedRunMode;

    private Stopwatch speedRunStopWatch;

    public TimeSpan SpeedRunTimeSpan
    {
        get
        {
            if (speedRunStopWatch == null)
            {
                return new TimeSpan();
            }

            return speedRunStopWatch.Elapsed;
        }
    }

    private void Awake()
    {
        EventManager.AddListener<OnStageCompleted>(OnStageCompleted);
        EventManager.AddListener<OnStageFail>(OnStageFail);
        EventManager.AddListener<OnWatchAdsCompleted>(OnWatchAdsCompleted);
        EventManager.AddListener<OnStageSwitch>(OnStageSwitch);
        EventManager.AddListener<OnSpeedRunStart>(OnSpeedRunStart);
        EventManager.AddListener<OnTriggerClick>(OnTriggerClick);
        EventManager.AddListener<OnLightSwitch>(OnLightSwitch);
        EventManager.AddListener<OnOpenLeaderboard>(OnOpenLeaderboard);
    }

    private void Start()
    {
        int hearts = Player.GetHearts();
        if (hearts > 0)
        {
            EventManager.Dispatch(new OnApplicationStart());
            IEnumerator routine = LoadNewStage(0, false);
            StartCoroutine(routine);
        }
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            stageToLoad += "0";
        }
        else if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            stageToLoad += "1";
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            stageToLoad += "2";
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            stageToLoad += "3";
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            stageToLoad += "4";
        }
        else if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            stageToLoad += "5";
        }
        else if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            stageToLoad += "6";
        }
        else if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            stageToLoad += "7";
        }
        else if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            stageToLoad += "8";
        }
        else if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            stageToLoad += "9";
        }
        else if (Input.GetKeyDown(KeyCode.KeypadPeriod))
        {
            stageToLoad = "";
        }
        else if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (stageToLoad != "")
            {
                int stage = int.Parse(stageToLoad) - 1;
                stageToLoad = "";
                int increment = stage - currentStage.StageNum;
                EventManager.Dispatch(new OnStageSwitch(increment));
                Debug.Log(stage);
            }
        }
#endif
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus && isSpeedRunMode)
        {
            SceneManager.LoadScene("SplashScreen");
        }
    }

    private void OnStageSwitch(object sender, OnStageSwitch eventArgs)
    {
        int heartCount = Player.GetHearts();

        if (heartCount <= 0 && !isSpeedRunMode)
        {
            if (currentStage != null)
            {
                Destroy(currentStage.gameObject);
            }

            Player.SetLastPlayedStage(0);
            var onHeartsCountWasChanged = new OnHeartsCountWasChanged(heartCount, false);
            EventManager.Dispatch(onHeartsCountWasChanged);
            return;
        }

        isFirstPlay = false;
        int stageToLoad = currentStage.StageNum + eventArgs.Increment;
        IEnumerator routine = LoadNewStage(stageToLoad, true);
        StartCoroutine(routine);
    }

    private void OnStageCompleted(object sender, OnStageCompleted onStageCompleted)
    {
        int stageToLoad = onStageCompleted.Stage + 1;
        if (isFirstPlay && !isSpeedRunMode)
        {
            isFirstPlay = false;
            int lastPlayerStage = Player.GetLastPlayedStage();
            if (lastPlayerStage != 0)
            {
                stageToLoad = lastPlayerStage;
            }
        }

        if (isSpeedRunMode && stageToLoad >= 25)
        {
            isSpeedRunMode = false;
            speedRunStopWatch.Stop();
            TimeSpan timeSpan = speedRunStopWatch.Elapsed;
            speedRunStopWatch = null;

            Player.SetLastSpeedRunTime(timeSpan);
            TimeSpan bestSpeedRuntimeSpan = Player.GetBestSpeedRunTime();
            if (timeSpan < bestSpeedRuntimeSpan)
            {
                Player.SetBestSpeedRunTime(timeSpan);
            }

            EventManager.Dispatch(new OnSpeedRunEnd(timeSpan));
            stageToLoad = -1;
        }

        IEnumerator routine = LoadNewStage(stageToLoad, false);
        StartCoroutine(routine);
    }

    private void OnWatchAdsCompleted(object sender, OnWatchAdsCompleted onWatchAdsCompleted)
    {
        int lastStage = Player.GetLastPlayedStage();
        if (isFirstPlay)
        {
            lastStage = 0;
        }

        IEnumerator routine = LoadNewStage(lastStage, false);
        StartCoroutine(routine);
    }

    private void OnSpeedRunStart(object sender, OnSpeedRunStart eventArgs)
    {
        isSpeedRunMode = true;
        if (!Player.IsAdsEnabled())
        {
            return;
        }

        int heartCount = Player.GetHearts();
        heartCount--;
        Player.SetHearts(heartCount);
        var onHeartsCountWasChanged = new OnHeartsCountWasChanged(heartCount, true);
        EventManager.Dispatch(onHeartsCountWasChanged);
    }

    private void OnTriggerClick(object sender, OnTriggerClick eventArgs)
    {
        if (!Player.HasFirstInteraction())
        {
            Player.SetFirstInteraction(true);
            var firstInteraction = new OnFirstInteraction(FirstInteraction.Play);
            EventManager.Dispatch(firstInteraction);
        }

        if (!isSpeedRunMode || speedRunStopWatch != null)
        {
            return;
        }

        speedRunStopWatch = new Stopwatch();
        speedRunStopWatch.Start();
    }

    private void OnOpenLeaderboard(object sender, OnOpenLeaderboard eventargs)
    {
        if (Player.HasFirstInteraction())
        {
            return;
        }

        Player.SetFirstInteraction(true);
        var firstInteraction = new OnFirstInteraction(FirstInteraction.OpenLeaderboard);
        EventManager.Dispatch(firstInteraction);
    }

    private void OnLightSwitch(object sender, OnLightSwitch eventargs)
    {
        if (Player.HasFirstInteraction())
        {
            return;
        }

        Player.SetFirstInteraction(true);
        var firstInteraction = new OnFirstInteraction(FirstInteraction.SwitchLight);
        EventManager.Dispatch(firstInteraction);
    }

    private void OnStageFail(object sender, OnStageFail onStageFail)
    {
        int heartCount = 1;
        if (!isSpeedRunMode && Player.IsAdsEnabled())
        {
            heartCount = Player.GetHearts();
            heartCount--;
            Player.SetHearts(heartCount);
            var onHeartsCountWasChanged = new OnHeartsCountWasChanged(heartCount, false);
            EventManager.Dispatch(onHeartsCountWasChanged);
        }

        if (heartCount <= 0)
        {
            return;
        }

        int stageToLoad = onStageFail.Stage;
        IEnumerator routine = LoadNewStage(stageToLoad, false);
        StartCoroutine(routine);
    }

    private IEnumerator LoadNewStage(int stageToLoad, bool imediatly)
    {
        if (!imediatly)
        {
            yield return new WaitForSeconds(0.5f);
        }

        if (currentStage != null)
        {
            Destroy(currentStage.gameObject);
        }

        string stagePath = string.Format("Prefabs/Stage ({0})", stageToLoad);
        var stage = Resources.Load<Stage>(stagePath);
        if (stage == null)
        {
            stageToLoad = 0;
            stagePath = string.Format("Prefabs/Stage ({0})", stageToLoad);
            stage = Resources.Load<Stage>(stagePath);
        }

        if (!isSpeedRunMode)
        {
            Player.SetLastStage(stageToLoad);
            if (stageToLoad != 0)
            {
                Player.SetLastPlayedStage(stageToLoad);
            }
        }

        currentStage = Instantiate(stage);
        EventManager.Dispatch(new OnStageLoaded(stageToLoad));
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<OnStageCompleted>(OnStageCompleted);
        EventManager.RemoveListener<OnStageFail>(OnStageFail);
        EventManager.RemoveListener<OnWatchAdsCompleted>(OnWatchAdsCompleted);
        EventManager.RemoveListener<OnStageSwitch>(OnStageSwitch);
        EventManager.RemoveListener<OnSpeedRunStart>(OnSpeedRunStart);
        EventManager.RemoveListener<OnTriggerClick>(OnTriggerClick);
        EventManager.RemoveListener<OnLightSwitch>(OnLightSwitch);
        EventManager.RemoveListener<OnOpenLeaderboard>(OnOpenLeaderboard);
    }

}