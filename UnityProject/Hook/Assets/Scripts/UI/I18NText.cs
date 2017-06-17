using TMPro;
using UnityEngine;

public class I18NText : MonoBehaviour {

    [SerializeField]
    private string key;

    private TextMeshProUGUI text;

    private void Awake() {
        this.text = this.GetComponent<TextMeshProUGUI>();
    }

    private void Start() {
        this.text.text = I18N.GetText(this.key);
    }

}