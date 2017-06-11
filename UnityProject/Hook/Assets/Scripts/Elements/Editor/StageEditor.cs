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
    }

}
