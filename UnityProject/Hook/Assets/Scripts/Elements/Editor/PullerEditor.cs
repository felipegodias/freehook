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
            ApplyMask(this.target);
        }
        bool isUpdatePullersButtonClicked = GUILayout.Button("Update Puller/End Puller");
        if (isUpdatePullersButtonClicked) {
            UpdatePullers(this.target);
        }
    }

    public static void ApplyMask(Puller target) {
        Transform parent = target.transform.parent;
        Match match = Regex.Match(parent.name, @"(\d+)");
        string layer = match.Value;
        int layerId = SortingLayer.NameToID(layer);
        Transform content = target.transform.Find("content");
        Transform pullerEnd = content.transform.Find("puller_end");

        Vector3 direction = pullerEnd.localPosition;
        direction = direction.normalized;
        direction.z = 0;

        float distance = Vector3.Distance(content.transform.localPosition, pullerEnd.localPosition);


        SpriteRenderer[] renderers = content.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < renderers.Length; i++) {
            renderers[i].maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            renderers[i].sortingLayerID = layerId;
        }

        Transform mask = target.transform.Find("mask");

        mask.right = direction;
        mask.localPosition = direction * (distance / 2);
        mask.localScale = new Vector3(distance, mask.localScale.y, 1);

        SpriteMask spriteMask = mask.GetComponent<SpriteMask>();
        spriteMask.frontSortingLayerID = layerId;
        spriteMask.frontSortingOrder = 0;
        spriteMask.backSortingLayerID = layerId;
        spriteMask.backSortingOrder = -1;
    }

    public static void UpdatePullers(Puller target) {
        target.gameObject.layer = LayerMask.NameToLayer("Puller");

        Collider2D collider2D = target.GetComponent<Collider2D>();
        Rigidbody2D rigidbody2D = target.GetComponent<Rigidbody2D>();

        if (collider2D != null) {
            DestroyImmediate(collider2D);
        }

        if (rigidbody2D != null) {
            DestroyImmediate(rigidbody2D);
        }

        BoxCollider2D boxCollider2D = target.gameObject.AddComponent<BoxCollider2D>();
        boxCollider2D.isTrigger = true;
        boxCollider2D.size = new Vector2(0.2f, 0.2f);
        rigidbody2D = target.gameObject.AddComponent<Rigidbody2D>();
        rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        Transform content = target.transform.Find("content");
        Transform pullerEnd = content.transform.Find("puller_end");

        collider2D = pullerEnd.GetComponent<Collider2D>();
        rigidbody2D = pullerEnd.GetComponent<Rigidbody2D>();

        if (collider2D != null) {
            DestroyImmediate(collider2D);
        }

        if (rigidbody2D != null) {
            DestroyImmediate(rigidbody2D);
        }

        boxCollider2D = pullerEnd.gameObject.AddComponent<BoxCollider2D>();
        boxCollider2D.isTrigger = true;
        boxCollider2D.size = new Vector2(0.2f, 0.2f);
        rigidbody2D = pullerEnd.gameObject.AddComponent<Rigidbody2D>();
        rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        pullerEnd.gameObject.layer = LayerMask.NameToLayer("Puller");
    }

}
