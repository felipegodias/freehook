using TMPro;

using UnityEngine;

public class I18NText : MonoBehaviour
{

    [SerializeField]
    private string key;

    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        text.text = I18N.GetText(key);
    }

}