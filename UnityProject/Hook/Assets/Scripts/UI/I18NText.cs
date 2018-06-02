using TMPro;

using UnityEngine;

public class I18NText : MonoBehaviour
{

    [SerializeField]
    private string key;

    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        text.text = I18N.GetText(key);
    }

}