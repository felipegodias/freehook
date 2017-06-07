using UnityEngine;

public class AreaDrawer : MonoBehaviour {

    [SerializeField]
    private Vector2 size;

    private void OnDrawGizmos() {
        Vector2 p1 = new Vector2(-this.size.x, this.size.y);
        Vector2 p2 = new Vector2(this.size.x, this.size.y);
        Vector2 p3 = new Vector2(-this.size.x, -this.size.y);
        Vector2 p4 = new Vector2(this.size.x, -this.size.y);
        Gizmos.DrawSphere(p1, 0.1f);
        Gizmos.DrawSphere(p2, 0.1f);
        Gizmos.DrawSphere(p3, 0.1f);
        Gizmos.DrawSphere(p4, 0.1f);
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p1, p3);
        Gizmos.DrawLine(p2, p4);
        Gizmos.DrawLine(p3, p4);
    }

}
