using System.Collections.Generic;
using MGS.EventManager;
using UnityEngine;

public class Stage : MonoBehaviour {

    [SerializeField]
    private int stage;

    private IList<Puller> pullers = new List<Puller>();
    [SerializeField]
    private List<GameElement> pullingList = new List<GameElement>();
    private bool stageFail;
    private bool stageClear;

    public int StageNum {
        get { return this.stage; }
    }

    public void RegisterNewPuller(Puller puller) {
        this.pullers.Add(puller);
    }

    public bool CanPull(GameElement gameElement) {
        return !this.pullingList.Contains(gameElement);
    }

    public void AddToPullingList(GameElement gameElement) {
        this.pullingList.Add(gameElement);
    }

    public void ClearPullingList() {
        this.pullingList.Clear();
    }

    private void Update() {

        if (this.stage == -1) {
            return;
        }
        if (this.stageClear || this.stageFail) {
            return;
        }
        foreach (Puller puller in this.pullers) {
            if (!puller.IsClear) {
                return;
            }
        }
        this.stageClear = true;
        EventManager.Dispatch(new OnStageCompleted(this.stage));

    }

    public void FailStage() {
        if (this.stageClear || this.stageFail) {
            return;
        }
        this.stageFail = true;
        EventManager.Dispatch(new OnStageFail(this.stage));
    }

}
