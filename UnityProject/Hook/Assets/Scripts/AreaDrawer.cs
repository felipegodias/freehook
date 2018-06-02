using UnityEngine;

public class AreaDrawer : MonoBehaviour
{

    [SerializeField]
    private Vector2 size;

    private void OnDrawGizmos()
    {
        var p1 = new Vector2(-size.x, size.y);
        var p2 = new Vector2(size.x, size.y);
        var p3 = new Vector2(-size.x, -size.y);
        var p4 = new Vector2(size.x, -size.y);
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