﻿using System.Collections;
using MGS.EventManager;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private Stage currentStage;

    private string stageToLoad;

    private void Awake() {
        EventManager.AddListener<OnStageCompleted>(this.OnStageCompleted);
        EventManager.AddListener<OnStageFail>(this.OnStageFail);
        EventManager.AddListener<OnWatchAdsCompleted>(this.OnWatchAdsCompleted);
        EventManager.AddListener<OnStageSwitch>(this.OnStageSwitch);
    }

    private void Start() {
        int hearts = Player.GetHearts();
        if (hearts > 0) {
            EventManager.Dispatch(new OnApplicationStart());
            int lastStage = Player.GetLastPlayedStage();
            IEnumerator routine = this.LoadNewStage(lastStage, false);
            this.StartCoroutine(routine);
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Keypad0)) {
            this.stageToLoad += "0";
        } else if (Input.GetKeyDown(KeyCode.Keypad1)) {
            this.stageToLoad += "1";
        } else if (Input.GetKeyDown(KeyCode.Keypad2)) {
            this.stageToLoad += "2";
        } else if (Input.GetKeyDown(KeyCode.Keypad3)) {
            this.stageToLoad += "3";
        } else if (Input.GetKeyDown(KeyCode.Keypad4)) {
            this.stageToLoad += "4";
        } else if (Input.GetKeyDown(KeyCode.Keypad5)) {
            this.stageToLoad += "5";
        } else if (Input.GetKeyDown(KeyCode.Keypad6)) {
            this.stageToLoad += "6";
        } else if (Input.GetKeyDown(KeyCode.Keypad7)) {
            this.stageToLoad += "7";
        } else if (Input.GetKeyDown(KeyCode.Keypad8)) {
            this.stageToLoad += "8";
        } else if (Input.GetKeyDown(KeyCode.Keypad9)) {
            this.stageToLoad += "9";
        } else if (Input.GetKeyDown(KeyCode.KeypadPeriod)) {
            this.stageToLoad = "";
        } else if (Input.GetKeyDown(KeyCode.KeypadEnter)) {
            if (this.stageToLoad != "") {
                int stage = int.Parse(this.stageToLoad) - 1;
                this.stageToLoad = "";
                int increment = stage - this.currentStage.StageNum;
                EventManager.Dispatch(new OnStageSwitch(increment));
                Debug.Log(stage);
            }
        }
    }

    private void OnStageSwitch(object sender, OnStageSwitch eventArgs) {
        int stageToLoad = this.currentStage.StageNum + eventArgs.Increment;
        IEnumerator routine = this.LoadNewStage(stageToLoad, true);
        this.StartCoroutine(routine);
    }

    private void OnStageCompleted(object sender, OnStageCompleted onStageCompleted) {
        int stageToLoad = onStageCompleted.Stage + 1;
        IEnumerator routine = this.LoadNewStage(stageToLoad, false);
        this.StartCoroutine(routine);
    }

    private void OnWatchAdsCompleted(object sender, OnWatchAdsCompleted onWatchAdsCompleted) {
        int lastStage = Player.GetLastPlayedStage();
        IEnumerator routine = this.LoadNewStage(lastStage, false);
        this.StartCoroutine(routine);
    }

    private void OnStageFail(object sender, OnStageFail onStageFail) {
        int heartCount = Player.GetHearts();
        heartCount--;
        Player.SetHearts(heartCount);
        OnHeartsCountWasChanged onHeartsCountWasChanged = new OnHeartsCountWasChanged(heartCount);
        EventManager.Dispatch(onHeartsCountWasChanged);

        if (heartCount > 0) {
            int stageToLoad = onStageFail.Stage;
            IEnumerator routine = this.LoadNewStage(stageToLoad, false);
            this.StartCoroutine(routine);
        }
    }

    private IEnumerator LoadNewStage(int stageToLoad, bool imediatly) {
        if (!imediatly) {
            yield return new WaitForSeconds(0.5f);
        }

        if (this.currentStage != null) {
            Destroy(this.currentStage.gameObject);
        }
        string stagePath = string.Format("Prefabs/Stage ({0})", stageToLoad);
        Stage stage = Resources.Load<Stage>(stagePath);
        if (stage == null) {
            stageToLoad = -1;
            stagePath = string.Format("Prefabs/Stage ({0})", stageToLoad);
            stage = Resources.Load<Stage>(stagePath);
        }
        Player.SetLastStage(stageToLoad);
        Player.SetLastPlayedStage(stageToLoad);
        this.currentStage = Instantiate(stage);
        EventManager.Dispatch(new OnStageLoaded(stageToLoad));
    }

    private void OnDestroy() {
        EventManager.RemoveListener<OnStageCompleted>(this.OnStageCompleted);
        EventManager.RemoveListener<OnStageFail>(this.OnStageFail);
        EventManager.RemoveListener<OnWatchAdsCompleted>(this.OnWatchAdsCompleted);
    }

}