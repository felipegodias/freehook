using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Switch), true)]
public class SwitchEditor : Editor {

    private new Switch target;

    private void OnEnable() {
        this.target = base.target as Switch;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        bool isAutoLinkButtonClicked = GUILayout.Button("Auto Link Elements");
        if (isAutoLinkButtonClicked) {
            LinkElements(this.target);
        }
    }

    public static void LinkElements(Switch switchElement) {
        SerializedObject serializedObject = new SerializedObject(switchElement);
        SerializedProperty a = serializedObject.FindProperty("a");
        SerializedProperty b = serializedObject.FindProperty("b");
        SerializedProperty c = serializedObject.FindProperty("c");
        SerializedProperty d = serializedObject.FindProperty("d");

        Vector3 position = switchElement.transform.position;
        Vector3 ap = position + Vector3.left / 2;
        Vector3 bp = position + Vector3.up / 2;
        Vector3 cp = position + Vector3.right / 2;
        Vector3 dp = position + Vector3.down / 2;

        Stage stage = switchElement.GetComponentInParent<Stage>();
        GameElement[] gameElements = stage.GetComponentsInChildren<GameElement>(true);
        foreach (GameElement gameElement in gameElements) {
            float aDistance = Vector3.Distance(gameElement.transform.position, ap);
            if (aDistance < Mathf.Epsilon) {
                a.objectReferenceValue = gameElement;
            }
            float bDistance = Vector3.Distance(gameElement.transform.position, bp);
            if (bDistance < Mathf.Epsilon) {
                b.objectReferenceValue = gameElement;
            }
            float cDistance = Vector3.Distance(gameElement.transform.position, cp);
            if (cDistance < Mathf.Epsilon) {
                c.objectReferenceValue = gameElement;
            }
            float dDistance = Vector3.Distance(gameElement.transform.position, dp);
            if (dDistance < Mathf.Epsilon) {
                d.objectReferenceValue = gameElement;
            }
        }
        serializedObject.ApplyModifiedProperties();
    }

}
