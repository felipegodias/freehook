#if UNITY_EDITOR
using UnityEngine;

using UnityEditor;
#endif
using UnityEngine;

[ExecuteInEditMode]
public class Line : MonoBehaviour
{

    [SerializeField]
    private Transform to;

    [SerializeField, HideInInspector]
    private new SpriteRenderer renderer;

    [SerializeField]
    private bool bold;

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (to == null)
        {
            return;
        }

        if (renderer == null)
        {
            var rendererGo = new GameObject("line_renderer");
            renderer = rendererGo.AddComponent<SpriteRenderer>();
            renderer.transform.SetParent(transform);
            renderer.transform.localPosition = Vector3.zero;
            renderer.transform.localScale = Vector3.one;
            renderer.drawMode = SpriteDrawMode.Tiled;
            Color color = Color.white;
            ColorUtility.TryParseHtmlString("#585451FF", out color);
            renderer.color = color;
        }

        Sprite sprite = null;
        if (bold)
        {
            sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/bold_line.png");
        }
        else
        {
            sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/line.png");
        }

        renderer.sprite = sprite;
#endif
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (to == null || renderer == null)
        {
            return;
        }

        Vector3 dir = (to.position - transform.position).normalized;
        float distance = Vector3.Distance(to.position, transform.position);
        dir.z = 0;
        renderer.transform.right = dir;
        renderer.size = new Vector3(distance, 1);

#endif
    }

    private void OnDestroy()
    {
#if UNITY_EDITOR
        if (renderer == null)
        {
            return;
        }

        DestroyImmediate(renderer.gameObject);
        renderer = null;
#endif
    }

}