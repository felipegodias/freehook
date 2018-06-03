using System;

using UnityEngine;

public class TutorialStep : MonoBehaviour
{

    private static TutorialStep s_Instance;

    [SerializeField]
    private GameObject m_AllowClickOn;

    [SerializeField]
    private GameObject m_Container;

    [SerializeField]
    private TutorialStep m_NextTutorialStep;

    private Action m_EndCallback;

    private void Awake()
    {
        if (m_Container != null)
        {
            m_Container.SetActive(false);
        }
    }

    public void Begin(Action endCallback)
    {
        s_Instance = this;
        m_EndCallback = endCallback;

        if (m_Container != null)
        {
            m_Container.SetActive(true);
        }
    }

    private void End()
    {
        if (m_Container != null)
        {
            m_Container.SetActive(false);
        }

        if (m_NextTutorialStep != null)
        {
            m_NextTutorialStep.Begin(m_EndCallback);
            return;
        }

        if (m_EndCallback != null)
        {
            m_EndCallback();
        }
    }

    public static bool HandleClickOn(GameObject gameObject)
    {
        if (s_Instance == null)
        {
            return true;
        }

        GameObject allowClickOn = s_Instance.m_AllowClickOn;
        if (allowClickOn != null && allowClickOn != gameObject)
        {
            return false;
        }

        TutorialStep instance = s_Instance;
        s_Instance = null;
        instance.End();
        return true;
    }

}