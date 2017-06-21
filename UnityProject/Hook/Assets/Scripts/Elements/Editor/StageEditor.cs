using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Stage))]
public class StageEditor : Editor {

    private new Stage target;

    private void OnEnable() {
        this.target = base.target as Stage;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        bool isUpdateElementCollidersButtonClicked = GUILayout.Button("Update Element Colliders");
        if (isUpdateElementCollidersButtonClicked) {

            ElementCollider[] elementColliders = this.target.GetComponentsInChildren<ElementCollider>(true);
            foreach (ElementCollider elementCollider in elementColliders) {
                ElementColliderEditor.UpdateColliders(elementCollider);
            }

        }

        bool isUpdatePullersButtonClicked = GUILayout.Button("Update Pullers/End Pullers");
        if (isUpdatePullersButtonClicked) {
            Puller[] pullers = this.target.GetComponentsInChildren<Puller>(true);
            foreach (Puller puller in pullers) {
                PullerEditor.UpdatePullers(puller);
            }
        }

        bool isApplyMasksButtonClicked = GUILayout.Button("Apply Masks");
        if (isApplyMasksButtonClicked) {
            Puller[] pullers = this.target.GetComponentsInChildren<Puller>(true);
            foreach (Puller puller in pullers) {
                PullerEditor.ApplyMask(puller);
            }
        }

        bool isSetPullersButtonClicked = GUILayout.Button("Set Pullers");
        if (isSetPullersButtonClicked) {
            Switch[] switches = this.target.GetComponentsInChildren<Switch>(true);
            for (int i = 0; i < 10; i++) {
                foreach (Switch swt in switches) {
                    swt.LookForPullers();
                }
            }

            GameElement[] gameElements = this.target.GetComponentsInChildren<GameElement>(true);
            foreach (GameElement gameElement in gameElements) {
                if (!(gameElement is Switch)) {
                    gameElement.SetPullers();
                }
            }
        }

        bool isAutoLinkButtonClicked = GUILayout.Button("Link Switches");
        if (isAutoLinkButtonClicked) {
            Switch[] switches = this.target.GetComponentsInChildren<Switch>(true);
            foreach (Switch swt in switches) {
                SwitchEditor.LinkElements(swt);
            }
        }

        bool isDoubleLinkButtonClicked = GUILayout.Button("Double Link Connectors");
        if (isDoubleLinkButtonClicked) {
            Connector[] connectors = this.target.GetComponentsInChildren<Connector>(true);
            foreach (Connector connector in connectors) {
                Debug.Log(connector);
                foreach (GameElement ge in connector.GameElements) {
                    if (!(ge is Connector)) {
                        continue;
                    }
                    bool alreadyAdded = false;
                    foreach (GameElement gameElement in ge.GameElements) {
                        if (gameElement != connector) {
                            continue;
                        }
                        alreadyAdded = true;
                        break;
                    }
                    if (alreadyAdded) {
                        continue;
                    }
                    SerializedObject serializedObject = new SerializedObject(ge);
                    SerializedProperty geProperty = serializedObject.FindProperty("gameElements");
                    geProperty.InsertArrayElementAtIndex(geProperty.arraySize);
                    SerializedProperty element = geProperty.GetArrayElementAtIndex(geProperty.arraySize - 1);
                    element.objectReferenceValue = connector;
                    serializedObject.ApplyModifiedProperties();
                    Editor connectorEditor = ConnectorEditor.CreateEditor(ge);
                    connectorEditor.OnInspectorGUI();
                }
            }
        }
    }

}