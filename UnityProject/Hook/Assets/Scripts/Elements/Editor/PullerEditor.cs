using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Puller))]
public class PullerEditor : Editor {

    private new Puller target;

    private void OnEnable() {
        this.target = base.target as Puller;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        bool applyMaskButtonClicked = GUILayout.Button("Apply Mask");
        if (applyMaskButtonClicked) {

            
            Transform content = this.target.transform.Find("content");
            SpriteRenderer[] renderers = content.GetComponentsInChildren<SpriteRenderer>();
            Bounds bounds = renderers[0].bounds;
            renderers[0].maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            for (int i = 1; i < renderers.Length; i++) {
                renderers[i].maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                bounds.Encapsulate(renderers[i].bounds);
            }

            Transform mask = this.target.transform.Find("mask");
            mask.position = bounds.center;
            mask.localScale = bounds.size;
        }
    }

}
