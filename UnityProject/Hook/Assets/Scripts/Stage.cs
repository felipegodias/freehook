using System.Collections.Generic;

using MGS.EventManager;

using UnityEngine;

public class Stage : MonoBehaviour
{

    [SerializeField]
    private int stage;

    private readonly IList<Puller> pullers = new List<Puller>();

    [SerializeField]
    private List<GameElement> pullingList = new List<GameElement>();

    private bool stageFail;
    private bool stageClear;

    public int StageNum
    {
        get
        {
            return stage;
        }
    }

    public void Awake()
    {
        RenderUtils.ClearDrawOrder();
    }

    public void RegisterNewPuller(Puller puller)
    {
        pullers.Add(puller);
    }

    public bool CanPull(GameElement gameElement)
    {
        return !pullingList.Contains(gameElement);
    }

    public void AddToPullingList(GameElement gameElement)
    {
        pullingList.Add(gameElement);
    }

    public void ClearPullingList()
    {
        pullingList.Clear();
    }

    private void Update()
    {
        if (stage == -1)
        {
            return;
        }

        if (stageClear || stageFail)
        {
            return;
        }

        foreach (Puller puller in pullers)
        {
            if (!puller.IsClear)
            {
                return;
            }
        }

        stageClear = true;
        EventManager.Dispatch(new OnStageCompleted(stage));
    }

    public void FailStage()
    {
        if (stageClear || stageFail)
        {
            return;
        }

        stageFail = true;
        EventManager.Dispatch(new OnStageFail(stage));
    }

}