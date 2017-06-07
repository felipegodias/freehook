using UnityEngine;

public class ElementCollider : MonoBehaviour {

    private Puller puller;

    private void Awake() {
        this.puller = this.GetComponentInParent<Puller>();
    }

    private void OnTriggerEnter2D(Collider2D collider2D) {
        this.puller.BreakPull();
    }

}
