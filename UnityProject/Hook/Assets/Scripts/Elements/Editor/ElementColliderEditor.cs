using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ElementCollider))]
public class ElementColliderEditor : Editor {

    private new ElementCollider target;

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        bool onCreateCollidersButtonClicked = GUILayout.Button("Update Colliders");
        if (onCreateCollidersButtonClicked) {
            UpdateColliders(this.target);
        }
    }

    public static void UpdateColliders(ElementCollider elementCollider, float radius = 0.075f) {
        elementCollider.gameObject.layer = LayerMask.NameToLayer("Game");

        Collider2D collider2D = elementCollider.GetComponent<Collider2D>();
        Rigidbody2D rigidbody2D = elementCollider.GetComponent<Rigidbody2D>();

        if (collider2D != null) {
            DestroyImmediate(collider2D);
        }

        if (rigidbody2D != null) {
            DestroyImmediate(rigidbody2D);
        }

        rigidbody2D = elementCollider.gameObject.AddComponent<Rigidbody2D>();
        rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        Transform[] children = elementCollider.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform transform in children) {
            if (transform == elementCollider.transform) {
                continue;
            }
            DestroyImmediate(transform.gameObject);
        }

        string name = elementCollider.name;
        if (name.StartsWith("line_curve")) {
            CreateLineCurveColliders(elementCollider, radius);
        } else if (name.StartsWith("cut_line")) {
            CreateCutLineColliders(elementCollider, radius);
        } else {
            CircleCollider2D circleCollider2D = elementCollider.gameObject.AddComponent<CircleCollider2D>();
            circleCollider2D.isTrigger = true;
            circleCollider2D.radius = radius;
        }
    }

    private void OnEnable() {
        this.target = base.target as ElementCollider;
    }

    private static void CreateCutLineColliders(ElementCollider elementCollider, float radius) {
        Vector2[] positions = {
            new Vector2(0.3f, 0),
            new Vector2(0.7f, 0)
        };

        for (int i = 0; i < positions.Length; i++) {
            CreateCollider(elementCollider, positions[i], i, radius);
        }
    }

    private static void CreateLineCurveColliders(ElementCollider elementCollider, float radius) {
        Vector2[] positions = {
            new Vector2(0.1f, 0),
            new Vector2(0.2f, 0.3f),
            new Vector2(0.5f, 0.45f),
            new Vector2(0.8f, 0.3f),
            new Vector2(0.9f, 0)
        };

        for (int i = 0; i < positions.Length; i++) {
            CreateCollider(elementCollider, positions[i], i, radius);
        }
    }

    private static void CreateCollider(ElementCollider elementCollider, Vector2 position, int index, float radius) {
        string name = string.Format("collider ({0})", index);
        GameObject gameObject = new GameObject(name);
        gameObject.layer = LayerMask.NameToLayer("Game");
        gameObject.transform.SetParent(elementCollider.transform);
        gameObject.transform.localPosition = position;
        gameObject.transform.localScale = Vector3.one;
        gameObject.transform.localRotation = Quaternion.identity;
        CircleCollider2D circleCollider2D = gameObject.AddComponent<CircleCollider2D>();
        circleCollider2D.radius = radius;
        circleCollider2D.isTrigger = true;
    }

}