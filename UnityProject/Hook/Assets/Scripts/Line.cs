#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[ExecuteInEditMode]
public class Line : MonoBehaviour {

    [SerializeField]
    private Transform to;

    [SerializeField, HideInInspector]
    private new SpriteRenderer renderer;

    [SerializeField]
    private bool bold;

    private void OnValidate() {
#if UNITY_EDITOR
        if (this.to == null) {
            return;
        }

        if (this.renderer == null) {
            GameObject rendererGo = new GameObject("line_renderer");
            this.renderer = rendererGo.AddComponent<SpriteRenderer>();
            this.renderer.transform.SetParent(this.transform);
            this.renderer.transform.localPosition = Vector3.zero;
            this.renderer.transform.localScale = Vector3.one;
            this.renderer.drawMode = SpriteDrawMode.Tiled;
            Color color = Color.white;
            ColorUtility.TryParseHtmlString("#585451FF", out color);
            this.renderer.color = color;
        }

        Sprite sprite = null;
        if (this.bold) {
            sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/bold_line.png");
        } else {
            sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/line.png");
        }
        this.renderer.sprite = sprite;
#endif
    }

    
    private void Update() {
#if UNITY_EDITOR
        if (this.to == null || this.renderer == null) {
            return;
        }
        Vector3 dir = (this.to.position - this.transform.position).normalized;
        float distance = Vector3.Distance(this.to.position, this.transform.position);
        dir.z = 0;
        this.renderer.transform.right = dir;
        this.renderer.size = new Vector3(distance, 1);
        
#endif
    }

}
