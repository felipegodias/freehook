using DG.Tweening;

using UnityEngine;

public class ClickMe : MonoBehaviour
{

    [SerializeField]
    private GameObject m_Container;

    [SerializeField]
    private float m_Force = 0.33f;

    [SerializeField]
    private int m_Vibrato = 5;

    [SerializeField]
    private float m_Elasticity = 0.25f;

    [SerializeField]
    private float m_Duration = 1;

    [SerializeField]
    private float m_Delay = 1;

    private Tweener m_Tweener;

    private float m_CurrentTime;

    private void Update()
    {
        m_CurrentTime -= Time.deltaTime;
        if (m_CurrentTime > 0)
        {
            return;
        }

        m_Tweener = m_Container.transform.DOPunchScale(Vector3.one * m_Force, m_Duration, m_Vibrato, m_Elasticity);
        m_CurrentTime = m_Duration + m_Delay;
    }

    private void OnEnable()
    {
        m_CurrentTime = 0;
    }

    private void OnDisable()
    {
        if (m_Tweener != null)
        {
            m_Tweener.Kill();
        }
    }

}