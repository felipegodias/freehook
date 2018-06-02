using UnityEngine;

public class ElementCollider : MonoBehaviour
{

    private Puller puller;

    private void Awake()
    {
        puller = GetComponentInParent<Puller>();
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        puller.BreakPull();
    }

}