using UnityEngine;

public class TutorialGroup : MonoBehaviour
{

    [SerializeField]
    private TutorialStep m_FirstTutorialStep;

    private Stage m_Stage;

    private void Awake()
    {
        m_Stage = GetComponentInParent<Stage>();
    }

    private void Start()
    {
        bool isTutorialDone = Player.IsTutorialDone(m_Stage.StageNum);
        if (isTutorialDone)
        {
            gameObject.SetActive(false);
            return;
        }

        Begin();
    }

    private void Begin()
    {
        m_FirstTutorialStep.Begin(End);
    }

    private void End()
    {
        Player.SetTutorialAsDone(m_Stage.StageNum);
    }

}