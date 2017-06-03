using System.Collections;
using MGS.EventManager;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private Stage currentStage;

    private void Awake() {
        EventManager.AddListener<OnStageCompleted>(this.OnStageCompleted);
        EventManager.AddListener<OnStageFail>(this.OnStageFail);
        EventManager.AddListener<OnWatchAdsCompleted>(this.OnWatchAdsCompleted);
    }

    private void Start() {
        EventManager.Dispatch(new OnApplicationStart());
        int lastStage = Player.GetLastPlayedStage();
        IEnumerator routine = this.LoadNewStage(lastStage);
        this.StartCoroutine(routine);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Keypad1)) {
            EventManager.Dispatch(new OnStageCompleted(0));
        } else if (Input.GetKeyDown(KeyCode.Keypad2)) {
            EventManager.Dispatch(new OnStageCompleted(1));
        } else if (Input.GetKeyDown(KeyCode.Keypad3)) {
            EventManager.Dispatch(new OnStageCompleted(2));
        } else if (Input.GetKeyDown(KeyCode.Keypad4)) {
            EventManager.Dispatch(new OnStageCompleted(3));
        } else if (Input.GetKeyDown(KeyCode.Keypad5)) {
            EventManager.Dispatch(new OnStageCompleted(4));
        } else if (Input.GetKeyDown(KeyCode.Keypad6)) {
            EventManager.Dispatch(new OnStageCompleted(5));
        } else if (Input.GetKeyDown(KeyCode.Keypad7)) {
            EventManager.Dispatch(new OnStageCompleted(6));
        } else if (Input.GetKeyDown(KeyCode.Keypad8)) {
            EventManager.Dispatch(new OnStageCompleted(7));
        } else if (Input.GetKeyDown(KeyCode.Keypad9)) {
            EventManager.Dispatch(new OnStageCompleted(8));
        }
    }

    private void OnStageCompleted(object sender, OnStageCompleted onStageCompleted) {
        int stageToLoad = onStageCompleted.Stage + 1;
        IEnumerator routine = this.LoadNewStage(stageToLoad);
        this.StartCoroutine(routine);
    }

    private void OnWatchAdsCompleted(object sender, OnWatchAdsCompleted onWatchAdsCompleted) {
        int lastStage = Player.GetLastPlayedStage();
        IEnumerator routine = this.LoadNewStage(lastStage);
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
            IEnumerator routine = this.LoadNewStage(stageToLoad);
            this.StartCoroutine(routine);
        }
    }

    private IEnumerator LoadNewStage(int stageToLoad) {
        yield return new WaitForSeconds(1);
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