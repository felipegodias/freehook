using System;
using System.Collections;

using MGS.EventManager;

using UnityEngine;
using UnityEngine.UI;

public class UIPrivacyPolicyScreen : MonoBehaviour
{

    [SerializeField]
    private string m_link;
    [SerializeField]
    private Button m_linkButton;
    [SerializeField]
    private Button m_agreeButton;

    private Action m_callback;

    public void Show(Action callback)
    {
        gameObject.SetActive(true);
        m_callback = callback;
    }

    private void Awake()
    {
        m_linkButton.onClick.AddListener(OnLinkButtonClicked);
        m_agreeButton.onClick.AddListener(OnAgreeButtonClicked);
    }

    private void OnLinkButtonClicked()
    {
        Application.OpenURL(m_link);
    }

    private void OnAgreeButtonClicked()
    {
        Player.SetAgreePrivacyPolicy();
        m_callback();
        gameObject.SetActive(false);
    }

}