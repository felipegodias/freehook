using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Connector))]
public class ConnectorEditor : Editor {

    private new Connector target;

    private SerializedProperty spriteRenderer;

    private SerializedProperty gameElements;
    private SerializedProperty pullers;

    private SerializedProperty lines;

    public override void OnInspectorGUI() {
        EditorGUILayout.PropertyField(this.gameElements, true);
        EditorGUILayout.PropertyField(this.pullers, true);

        if (this.spriteRenderer.objectReferenceValue == null) {
            SpriteRenderer spriteRenderer = this.target.gameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.color = ColorUtils.LineColor;
            spriteRenderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/connector.png");
            this.spriteRenderer.objectReferenceValue = spriteRenderer;
        }

        if (this.gameElements.arraySize != this.lines.arraySize) {
            for (int i = 0; i < this.lines.arraySize; i++) {
                SerializedProperty line = this.lines.GetArrayElementAtIndex(i);
                SpriteRenderer spriteRenderer = line.objectReferenceValue as SpriteRenderer;
                if (spriteRenderer != null) {
                    DestroyImmediate(spriteRenderer.gameObject);
                }
            }

            this.lines.ClearArray();

            for (int i = 0; i < this.gameElements.arraySize; i++) {
                this.lines.InsertArrayElementAtIndex(i);
                SerializedProperty line = this.lines.GetArrayElementAtIndex(i);

                GameObject lineGameObject = new GameObject("line (" + i + ")");
                lineGameObject.transform.SetParent(this.target.transform);
                lineGameObject.transform.localPosition = Vector3.zero;
                SpriteRenderer lineSpriteRenderer = lineGameObject.AddComponent<SpriteRenderer>();
                lineSpriteRenderer.drawMode = SpriteDrawMode.Tiled;
                lineSpriteRenderer.color = ColorUtils.LineColor;
                lineSpriteRenderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/line.png");

                line.objectReferenceValue = lineSpriteRenderer;
            }
        }
        this.serializedObject.ApplyModifiedProperties();
    }

    private void OnEnable() {
        this.target = base.target as Connector;
        this.spriteRenderer = this.serializedObject.FindProperty("spriteRenderer");
        this.gameElements = this.serializedObject.FindProperty("gameElements");
        this.pullers = this.serializedObject.FindProperty("pullers");
        this.lines = this.serializedObject.FindProperty("lines");
    }

}