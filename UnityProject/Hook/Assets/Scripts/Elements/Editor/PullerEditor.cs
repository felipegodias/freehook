using System.Text.RegularExpressions;
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


            Transform parent = this.target.transform.parent;
            Match match = Regex.Match(parent.name, @"(\d+)");
            string layer = match.Value;
            int layerId = SortingLayer.NameToID(layer);
            Transform content = this.target.transform.Find("content");
            Transform pullerEnd = content.transform.Find("puller_end");

            Vector3 direction = pullerEnd.localPosition - content.transform.localPosition;
            direction = direction.normalized;
            direction.z = 0;

            float distance = Vector3.Distance(content.transform.localPosition, pullerEnd.localPosition);

            
            SpriteRenderer[] renderers = content.GetComponentsInChildren<SpriteRenderer>();
            for (int i = 0; i < renderers.Length; i++) {
                renderers[i].maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                renderers[i].sortingLayerID = layerId;
            }

            Transform mask = this.target.transform.Find("mask");

            mask.right = direction;
            mask.localPosition = direction * (distance / 2);
            mask.localScale = new Vector3(distance, 1, 1);

            SpriteMask spriteMask = mask.GetComponent<SpriteMask>();
            spriteMask.frontSortingLayerID = layerId;
            spriteMask.frontSortingOrder = 0;
            spriteMask.backSortingLayerID = layerId;
            spriteMask.backSortingOrder = -1;
        }
    }

}
